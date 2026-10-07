using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;

namespace ProductManager
{
    public class ExcelImportResult
    {
        public bool Success { get; set; }
        public List<Product> ImportedProducts { get; set; } = new List<Product>();
        public List<string> Errors { get; set; } = new List<string>();
        public List<string> Warnings { get; set; } = new List<string>();
    }

    public static class ProductExcelImporter
    {
        private static readonly string[] RequiredHeaders = new[]
        {
            "MaSP", "TenSP", "LoaiSP", "DonGia", "SoLuong", "NgayNhap", "ConKinhDoanh"
        };

        private static readonly Dictionary<string, string> AllowedProductTypes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "Điện tử", "Điện tử" },
            { "Văn phòng", "Văn phòng" },
            { "Gia dụng", "Gia dụng" },
            { "Thời trang", "Thời trang" }
        };

        public static ExcelImportResult ImportProductsFromExcel(string filePath, IEnumerable<Product> existingProducts)
        {
            var result = new ExcelImportResult();
            var existingCodeSet = new HashSet<string>(
                (existingProducts ?? Enumerable.Empty<Product>()).Select(p => p.MaSP?.Trim()),
                StringComparer.OrdinalIgnoreCase
            );
            existingCodeSet.Remove(null);

            if (!File.Exists(filePath))
            {
                result.Errors.Add("Tệp Excel không tồn tại: " + filePath);
                return result;
            }

            string fileDirectory = Path.GetDirectoryName(Path.GetFullPath(filePath));

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var doc = SpreadsheetDocument.Open(stream, false))
                {
                    WorkbookPart workbookPart = doc.WorkbookPart;
                    if (workbookPart == null)
                    {
                        result.Errors.Add("Tệp Excel không chứa WorkbookPart hợp lệ.");
                        return result;
                    }

                    // Kiểm tra hệ ngày 1904 (WorkbookProperties.Date1904)
                    bool is1904 = false;
                    var wbProps = workbookPart.Workbook?.WorkbookProperties ?? workbookPart.Workbook?.Elements<WorkbookProperties>().FirstOrDefault();
                    if (wbProps != null && wbProps.Date1904 != null && wbProps.Date1904.HasValue)
                    {
                        is1904 = wbProps.Date1904.Value;
                    }

                    Sheet firstSheet = workbookPart.Workbook?.Descendants<Sheet>().FirstOrDefault();
                    if (firstSheet == null || firstSheet.Id == null)
                    {
                        result.Errors.Add("Tệp Excel không chứa bảng tính (worksheet) nào.");
                        return result;
                    }

                    var worksheetPart = (WorksheetPart)workbookPart.GetPartById(firstSheet.Id);
                    if (worksheetPart == null || worksheetPart.Worksheet == null)
                    {
                        result.Errors.Add("Không tìm thấy dữ liệu bảng tính đầu tiên.");
                        return result;
                    }

                    // Đọc SharedStringTable nếu có
                    List<string> sharedStrings = null;
                    if (workbookPart.SharedStringTablePart != null && workbookPart.SharedStringTablePart.SharedStringTable != null)
                    {
                        sharedStrings = workbookPart.SharedStringTablePart.SharedStringTable
                            .Elements<SharedStringItem>()
                            .Select(s => s.InnerText ?? "")
                            .ToList();
                    }

                    SheetData sheetData = worksheetPart.Worksheet.Elements<SheetData>().FirstOrDefault();
                    if (sheetData == null)
                    {
                        result.Errors.Add("Bảng tính không có dữ liệu hàng nào.");
                        return result;
                    }

                    var rows = sheetData.Elements<Row>().ToList();
                    if (rows.Count == 0)
                    {
                        result.Errors.Add("Bảng tính không có dữ liệu hàng nào.");
                        return result;
                    }

                    // Hàng 1 là tiêu đề
                    Row headerRow = rows.FirstOrDefault(r => (r.RowIndex != null && r.RowIndex.Value == 1)) ?? rows[0];
                    var headerCells = headerRow.Elements<Cell>().ToList();
                    if (headerCells.Count == 0)
                    {
                        result.Errors.Add("Hàng 1 không chứa tiêu đề cột nào.");
                        return result;
                    }

                    var colToFieldMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
                    var seenHeaders = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

                    int colCounter = 1;
                    foreach (Cell hCell in headerCells)
                    {
                        string colLetter = GetColumnLetters(hCell.CellReference, colCounter++);
                        string rawHeader = GetCellText(hCell, sharedStrings).Trim();
                        if (string.IsNullOrEmpty(rawHeader)) continue;

                        if (seenHeaders.Contains(rawHeader))
                        {
                            result.Errors.Add(string.Format("Tiêu đề cột bị trùng lặp trong hàng 1: '{0}'.", rawHeader));
                        }
                        else
                        {
                            seenHeaders.Add(rawHeader);
                        }

                        // Kiểm tra ánh xạ vào các cột quy chuẩn
                        string canonical = MatchCanonicalHeader(rawHeader);
                        if (canonical != null)
                        {
                            colToFieldMap[colLetter] = canonical;
                        }
                    }

                    // Kiểm tra đủ 7 cột bắt buộc
                    var missingRequired = RequiredHeaders
                        .Where(rh => !colToFieldMap.Values.Contains(rh, StringComparer.OrdinalIgnoreCase))
                        .ToList();
                    if (missingRequired.Count > 0)
                    {
                        result.Errors.Add("Tệp Excel thiếu các cột bắt buộc: " + string.Join(", ", missingRequired) + ".");
                    }

                    if (result.Errors.Count > 0)
                    {
                        return result;
                    }

                    // Đọc các hàng dữ liệu từ sau hàng tiêu đề
                    var fileCodeTracker = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
                    var candidateProducts = new List<Product>();
                    int dataRowCount = 0;

                    foreach (Row row in rows)
                    {
                        int rowNum = (int)(row.RowIndex != null ? row.RowIndex.Value : 0);
                        if (row == headerRow || (rowNum != 0 && rowNum <= 1))
                            continue;

                        var cells = row.Elements<Cell>().ToList();
                        if (cells.Count == 0) continue;

                        // Kiểm tra xem dòng có hoàn toàn trống không
                        bool allEmpty = true;
                        var rowCellDict = new Dictionary<string, Cell>(StringComparer.OrdinalIgnoreCase);
                        int cellIndex = 1;
                        foreach (Cell c in cells)
                        {
                            string colLetter = GetColumnLetters(c.CellReference, cellIndex++);
                            rowCellDict[colLetter] = c;
                            string txt = GetCellText(c, sharedStrings);
                            if (!string.IsNullOrWhiteSpace(txt)) allEmpty = false;
                        }

                        if (allEmpty)
                        {
                            // Bỏ qua dòng hoàn toàn trống
                            continue;
                        }

                        dataRowCount++;
                        int displayRowNum = rowNum > 0 ? rowNum : dataRowCount + 1;

                        // Kiểm tra ô công thức trong các cột được nhập
                        foreach (var kvp in colToFieldMap)
                        {
                            string colLetter = kvp.Key;
                            string fieldName = kvp.Value;
                            if (rowCellDict.TryGetValue(colLetter, out Cell c))
                            {
                                if (c.CellFormula != null && !string.IsNullOrWhiteSpace(c.CellFormula.Text))
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột {1}: Ô chứa công thức (={2}). Yêu cầu chuyển sang giá trị tĩnh.", displayRowNum, fieldName, c.CellFormula.Text));
                                }
                            }
                        }

                        var product = new Product();
                        bool rowHasError = false;

                        // 1. MaSP
                        Cell maCell = GetCellByField(rowCellDict, colToFieldMap, "MaSP");
                        string rawMaSP = maCell != null ? GetCellText(maCell, sharedStrings) : "";
                        if (maCell == null || string.IsNullOrWhiteSpace(rawMaSP))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột MaSP: Mã sản phẩm không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else
                        {
                            if (IsNumericCell(maCell))
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột MaSP: Mã sản phẩm phải là ô định dạng Text (chuỗi) để giữ số 0 ở đầu.", displayRowNum));
                                rowHasError = true;
                            }
                            else
                            {
                                string maSP = rawMaSP.Trim();
                                if (existingCodeSet.Contains(maSP))
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột MaSP: Mã sản phẩm '{1}' đã tồn tại trong danh sách ứng dụng (không phân biệt hoa/thường).", displayRowNum, maSP));
                                    rowHasError = true;
                                }
                                else if (fileCodeTracker.TryGetValue(maSP, out int prevRow))
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột MaSP: Mã sản phẩm '{1}' bị trùng lặp với dòng {2} trong cùng tệp Excel.", displayRowNum, maSP, prevRow));
                                    rowHasError = true;
                                }
                                else
                                {
                                    fileCodeTracker[maSP] = displayRowNum;
                                    product.MaSP = maSP;
                                }
                            }
                        }

                        // 2. TenSP
                        Cell tenCell = GetCellByField(rowCellDict, colToFieldMap, "TenSP");
                        string rawTenSP = tenCell != null ? GetCellText(tenCell, sharedStrings) : "";
                        if (tenCell == null || string.IsNullOrWhiteSpace(rawTenSP))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột TenSP: Tên sản phẩm không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else
                        {
                            product.TenSP = rawTenSP.Trim();
                        }

                        // 3. LoaiSP
                        Cell loaiCell = GetCellByField(rowCellDict, colToFieldMap, "LoaiSP");
                        string rawLoaiSP = loaiCell != null ? GetCellText(loaiCell, sharedStrings) : "";
                        if (loaiCell == null || string.IsNullOrWhiteSpace(rawLoaiSP))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột LoaiSP: Loại sản phẩm không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else
                        {
                            string trimmedLoai = rawLoaiSP.Trim();
                            if (AllowedProductTypes.TryGetValue(trimmedLoai, out string standardType))
                            {
                                product.LoaiSP = standardType;
                            }
                            else
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột LoaiSP: Loại sản phẩm '{1}' không hợp lệ. Chỉ chấp nhận: Điện tử, Văn phòng, Gia dụng, Thời trang.", displayRowNum, trimmedLoai));
                                rowHasError = true;
                            }
                        }

                        // 4. DonGia
                        Cell giaCell = GetCellByField(rowCellDict, colToFieldMap, "DonGia");
                        if (giaCell == null || string.IsNullOrWhiteSpace(giaCell.InnerText))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Đơn giá không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else if (IsStringCell(giaCell))
                        {
                            string rawVal = GetCellText(giaCell, sharedStrings);
                            result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Đơn giá đang ở dạng chuỗi '{1}'. Vui lòng chuyển ô sang kiểu số trong Excel.", displayRowNum, rawVal));
                            rowHasError = true;
                        }
                        else
                        {
                            if (!decimal.TryParse(giaCell.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal donGia))
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Giá trị đơn giá '{1}' không hợp lệ.", displayRowNum, giaCell.InnerText));
                                rowHasError = true;
                            }
                            else
                            {
                                if (donGia <= 0)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Đơn giá phải lớn hơn 0 (giá trị hiện tại: {1}).", displayRowNum, donGia));
                                    rowHasError = true;
                                }
                                if (donGia > 1000000000000M)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Đơn giá vượt quá giới hạn tối đa 1.000.000.000.000 VNĐ.", displayRowNum));
                                    rowHasError = true;
                                }
                                if (decimal.Round(donGia, 2) != donGia)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột DonGia: Đơn giá chỉ được có tối đa 2 chữ số thập phân (giá trị: {1}).", displayRowNum, donGia));
                                    rowHasError = true;
                                }
                                product.DonGia = donGia;
                            }
                        }

                        // 5. SoLuong
                        Cell slCell = GetCellByField(rowCellDict, colToFieldMap, "SoLuong");
                        if (slCell == null || string.IsNullOrWhiteSpace(slCell.InnerText))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột SoLuong: Số lượng không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else if (IsStringCell(slCell))
                        {
                            string rawVal = GetCellText(slCell, sharedStrings);
                            result.Errors.Add(string.Format("Dòng {0}, Cột SoLuong: Số lượng đang ở dạng chuỗi '{1}'. Vui lòng chuyển ô sang kiểu số trong Excel.", displayRowNum, rawVal));
                            rowHasError = true;
                        }
                        else
                        {
                            if (!decimal.TryParse(slCell.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out decimal slDec))
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột SoLuong: Giá trị số lượng '{1}' không hợp lệ.", displayRowNum, slCell.InnerText));
                                rowHasError = true;
                            }
                            else
                            {
                                if (decimal.Floor(slDec) != slDec)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột SoLuong: Số lượng phải là số nguyên, không được có phần lẻ thập phân (giá trị: {1}).", displayRowNum, slDec));
                                    rowHasError = true;
                                }
                                else if (slDec < 0 || slDec > int.MaxValue)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột SoLuong: Số lượng phải nằm trong khoảng từ 0 đến {1} (giá trị: {2}).", displayRowNum, int.MaxValue, slDec));
                                    rowHasError = true;
                                }
                                else
                                {
                                    product.SoLuong = (int)slDec;
                                }
                            }
                        }

                        // 6. NgayNhap
                        Cell ngayCell = GetCellByField(rowCellDict, colToFieldMap, "NgayNhap");
                        if (ngayCell == null || string.IsNullOrWhiteSpace(ngayCell.InnerText))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột NgayNhap: Ngày nhập không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else
                        {
                            bool dateParsed = false;
                            DateTime ngayNhapVal = DateTime.MinValue;

                            if (IsNumericCell(ngayCell))
                            {
                                if (double.TryParse(ngayCell.InnerText, NumberStyles.Float, CultureInfo.InvariantCulture, out double oaDate))
                                {
                                    try
                                    {
                                        if (is1904)
                                        {
                                            ngayNhapVal = new DateTime(1904, 1, 1).AddDays(oaDate).Date;
                                        }
                                        else
                                        {
                                            ngayNhapVal = DateTime.FromOADate(oaDate).Date;
                                        }
                                        dateParsed = true;
                                    }
                                    catch { dateParsed = false; }
                                }
                            }
                            else
                            {
                                string textDate = GetCellText(ngayCell, sharedStrings).Trim();
                                if (DateTime.TryParseExact(textDate, new[] { "dd/MM/yyyy", "d/M/yyyy" }, CultureInfo.InvariantCulture, DateTimeStyles.None, out ngayNhapVal))
                                {
                                    dateParsed = true;
                                    ngayNhapVal = ngayNhapVal.Date;
                                }
                            }

                            if (!dateParsed)
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột NgayNhap: Ngày nhập '{1}' không đúng định dạng. Yêu cầu ô ngày Excel hoặc chuỗi 'dd/MM/yyyy'.", displayRowNum, GetCellText(ngayCell, sharedStrings)));
                                rowHasError = true;
                            }
                            else
                            {
                                if (ngayNhapVal.Date > DateTime.Today)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột NgayNhap: Ngày nhập ({1:dd/MM/yyyy}) không được lớn hơn ngày hiện tại ({2:dd/MM/yyyy}).", displayRowNum, ngayNhapVal, DateTime.Today));
                                    rowHasError = true;
                                }
                                if (ngayNhapVal.Date < DateTimePicker.MinDateTime || ngayNhapVal.Date > DateTimePicker.MaxDateTime)
                                {
                                    result.Errors.Add(string.Format("Dòng {0}, Cột NgayNhap: Ngày nhập nằm ngoài khoảng hỗ trợ.", displayRowNum));
                                    rowHasError = true;
                                }
                                product.NgayNhap = ngayNhapVal.Date;
                            }
                        }

                        // 7. ConKinhDoanh
                        Cell kdCell = GetCellByField(rowCellDict, colToFieldMap, "ConKinhDoanh");
                        if (kdCell == null || string.IsNullOrWhiteSpace(kdCell.InnerText))
                        {
                            result.Errors.Add(string.Format("Dòng {0}, Cột ConKinhDoanh: Trạng thái còn kinh doanh không được để trống.", displayRowNum));
                            rowHasError = true;
                        }
                        else
                        {
                            bool kdParsed = false;
                            bool kdVal = false;

                            if (kdCell.DataType != null && kdCell.DataType.Value == CellValues.Boolean)
                            {
                                kdVal = (kdCell.InnerText == "1" || string.Equals(kdCell.InnerText, "true", StringComparison.OrdinalIgnoreCase));
                                kdParsed = true;
                            }
                            else
                            {
                                string rawKd = GetCellText(kdCell, sharedStrings).Trim();
                                if (string.Equals(rawKd, "1") ||
                                    string.Equals(rawKd, "true", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(rawKd, "Có", StringComparison.OrdinalIgnoreCase) ||
                                    string.Equals(rawKd, "Co", StringComparison.OrdinalIgnoreCase))
                                {
                                    kdVal = true;
                                    kdParsed = true;
                                }
                                else if (string.Equals(rawKd, "0") ||
                                         string.Equals(rawKd, "false", StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(rawKd, "Không", StringComparison.OrdinalIgnoreCase) ||
                                         string.Equals(rawKd, "Khong", StringComparison.OrdinalIgnoreCase))
                                {
                                    kdVal = false;
                                    kdParsed = true;
                                }
                            }

                            if (!kdParsed)
                            {
                                result.Errors.Add(string.Format("Dòng {0}, Cột ConKinhDoanh: Giá trị '{1}' không hợp lệ. Chỉ chấp nhận boolean Excel hoặc: Có/Không, True/False, 1/0.", displayRowNum, GetCellText(kdCell, sharedStrings)));
                                rowHasError = true;
                            }
                            else
                            {
                                product.ConKinhDoanh = kdVal;
                            }
                        }

                        // 8. DuongDanAnh (tùy chọn)
                        Cell imgCell = GetCellByField(rowCellDict, colToFieldMap, "DuongDanAnh");
                        string rawImg = imgCell != null ? GetCellText(imgCell, sharedStrings).Trim() : "";
                        if (string.IsNullOrEmpty(rawImg))
                        {
                            product.DuongDanAnh = "";
                        }
                        else
                        {
                            if (rawImg.StartsWith("http://", StringComparison.OrdinalIgnoreCase) ||
                                rawImg.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
                            {
                                result.Warnings.Add(string.Format("Dòng {0}, Cột DuongDanAnh: Đường dẫn là URL internet. Ứng dụng không tự tải ảnh từ mạng.", displayRowNum));
                                product.DuongDanAnh = rawImg;
                            }
                            else
                            {
                                string resolvedPath = rawImg;
                                try
                                {
                                    if (!Path.IsPathRooted(rawImg))
                                    {
                                        resolvedPath = Path.GetFullPath(Path.Combine(fileDirectory, rawImg));
                                    }
                                }
                                catch (Exception ex)
                                {
                                    result.Warnings.Add(string.Format("Dòng {0}, Cột DuongDanAnh: Đường dẫn ảnh không hợp lệ ({1}).", displayRowNum, ex.Message));
                                }

                                if (!File.Exists(resolvedPath))
                                {
                                    result.Warnings.Add(string.Format("Dòng {0}, Cột DuongDanAnh: Tệp ảnh không tồn tại ('{1}').", displayRowNum, resolvedPath));
                                }
                                product.DuongDanAnh = resolvedPath;
                            }
                        }

                        if (!rowHasError)
                        {
                            candidateProducts.Add(product);
                        }
                    }

                    if (dataRowCount == 0)
                    {
                        result.Errors.Add("Tệp Excel không có dòng dữ liệu nào.");
                    }

                    if (result.Errors.Count == 0)
                    {
                        result.Success = true;
                        result.ImportedProducts = candidateProducts;
                    }
                    else
                    {
                        result.Success = false;
                        result.ImportedProducts.Clear();
                    }

                    return result;
                }
            }
            catch (Exception ex)
            {
                result.Errors.Add("Không thể đọc tệp Excel: " + ex.Message);
                result.Success = false;
                result.ImportedProducts.Clear();
                return result;
            }
        }

        public static void CreateSampleTemplate(string targetFilePath)
        {
            string dir = Path.GetDirectoryName(Path.GetFullPath(targetFilePath));
            if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }

            using (var doc = SpreadsheetDocument.Create(targetFilePath, SpreadsheetDocumentType.Workbook))
            {
                WorkbookPart workbookPart = doc.AddWorkbookPart();
                workbookPart.Workbook = new Workbook();

                WorksheetPart worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
                var sheetData = new SheetData();
                worksheetPart.Worksheet = new Worksheet(sheetData);

                var sheets = workbookPart.Workbook.AppendChild(new Sheets());
                sheets.Append(new Sheet
                {
                    Id = workbookPart.GetIdOfPart(worksheetPart),
                    SheetId = 1,
                    Name = "DanhSachSanPham"
                });

                // Hàng 1: Tiêu đề
                var headerRow = new Row { RowIndex = 1 };
                headerRow.Append(
                    CreateTextCell("MaSP", "A1"),
                    CreateTextCell("TenSP", "B1"),
                    CreateTextCell("LoaiSP", "C1"),
                    CreateTextCell("DonGia", "D1"),
                    CreateTextCell("SoLuong", "E1"),
                    CreateTextCell("NgayNhap", "F1"),
                    CreateTextCell("ConKinhDoanh", "G1"),
                    CreateTextCell("DuongDanAnh", "H1")
                );
                sheetData.Append(headerRow);

                // Dữ liệu mẫu (SP004, SP005, SP006, SP007)
                var sampleData = new[]
                {
                    new { Ma = "SP004", Ten = "Bàn phím cơ không dây", Loai = "Điện tử", Gia = 1250000m, SL = 15, Ngay = "01/10/2026", KD = "Có", Anh = "" },
                    new { Ma = "SP005", Ten = "Bút ký cao cấp", Loai = "Văn phòng", Gia = 85000m, SL = 40, Ngay = "02/10/2026", KD = "Có", Anh = "" },
                    new { Ma = "SP006", Ten = "Áo thun polo nam", Loai = "Thời trang", Gia = 220000m, SL = 30, Ngay = "03/10/2026", KD = "Có", Anh = "" },
                    new { Ma = "SP007", Ten = "Nồi chiên không dầu", Loai = "Gia dụng", Gia = 1750000m, SL = 8, Ngay = "04/10/2026", KD = "Không", Anh = "" }
                };

                uint rIndex = 2;
                foreach (var item in sampleData)
                {
                    var dataRow = new Row { RowIndex = rIndex };
                    dataRow.Append(
                        CreateTextCell(item.Ma, "A" + rIndex),
                        CreateTextCell(item.Ten, "B" + rIndex),
                        CreateTextCell(item.Loai, "C" + rIndex),
                        CreateNumberCell(item.Gia, "D" + rIndex),
                        CreateNumberCell(item.SL, "E" + rIndex),
                        CreateTextCell(item.Ngay, "F" + rIndex),
                        CreateTextCell(item.KD, "G" + rIndex),
                        CreateTextCell(item.Anh, "H" + rIndex)
                    );
                    sheetData.Append(dataRow);
                    rIndex++;
                }

                workbookPart.Workbook.Save();
            }
        }

        private static Cell CreateTextCell(string text, string cellRef)
        {
            return new Cell
            {
                CellReference = cellRef,
                DataType = CellValues.InlineString,
                InlineString = new InlineString(new Text(text ?? ""))
            };
        }

        private static Cell CreateNumberCell(decimal value, string cellRef)
        {
            return new Cell
            {
                CellReference = cellRef,
                DataType = CellValues.Number,
                CellValue = new CellValue(value.ToString(CultureInfo.InvariantCulture))
            };
        }

        private static Cell CreateNumberCell(int value, string cellRef)
        {
            return new Cell
            {
                CellReference = cellRef,
                DataType = CellValues.Number,
                CellValue = new CellValue(value.ToString(CultureInfo.InvariantCulture))
            };
        }

        private static string MatchCanonicalHeader(string header)
        {
            string trimmed = header.Trim();
            foreach (var rh in RequiredHeaders)
            {
                if (string.Equals(rh, trimmed, StringComparison.OrdinalIgnoreCase))
                    return rh;
            }
            if (string.Equals("DuongDanAnh", trimmed, StringComparison.OrdinalIgnoreCase))
                return "DuongDanAnh";
            return null;
        }

        private static Cell GetCellByField(Dictionary<string, Cell> rowCells, Dictionary<string, string> colMap, string fieldName)
        {
            foreach (var kvp in colMap)
            {
                if (string.Equals(kvp.Value, fieldName, StringComparison.OrdinalIgnoreCase))
                {
                    if (rowCells.TryGetValue(kvp.Key, out Cell cell))
                        return cell;
                }
            }
            return null;
        }

        private static bool IsStringCell(Cell cell)
        {
            if (cell == null || cell.DataType == null) return false;
            return cell.DataType.Value == CellValues.SharedString ||
                   cell.DataType.Value == CellValues.String ||
                   cell.DataType.Value == CellValues.InlineString;
        }

        private static bool IsNumericCell(Cell cell)
        {
            if (cell == null) return false;
            if (cell.DataType == null || cell.DataType.Value == CellValues.Number)
            {
                return !string.IsNullOrWhiteSpace(cell.InnerText);
            }
            return false;
        }

        private static string GetCellText(Cell cell, List<string> sharedStrings)
        {
            if (cell == null) return "";
            if (cell.DataType != null && cell.DataType.Value == CellValues.SharedString)
            {
                if (int.TryParse(cell.InnerText, out int id) && sharedStrings != null && id >= 0 && id < sharedStrings.Count)
                {
                    return sharedStrings[id];
                }
            }
            if (cell.DataType != null && cell.DataType.Value == CellValues.InlineString)
            {
                return cell.InlineString?.Text?.Text ?? cell.InlineString?.InnerText ?? "";
            }
            return cell.InnerText ?? "";
        }

        private static string GetColumnLetters(string cellReference, int fallbackColIndex)
        {
            if (!string.IsNullOrEmpty(cellReference))
            {
                string letters = new string(cellReference.TakeWhile(char.IsLetter).ToArray()).ToUpperInvariant();
                if (!string.IsNullOrEmpty(letters)) return letters;
            }
            return IndexToColumnLetters(fallbackColIndex);
        }

        private static string IndexToColumnLetters(int colIndex)
        {
            string result = "";
            while (colIndex > 0)
            {
                int mod = (colIndex - 1) % 26;
                result = (char)('A' + mod) + result;
                colIndex = (colIndex - mod) / 26;
            }
            return result;
        }
    }
}
