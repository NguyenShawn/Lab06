using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProductManager
{
    public partial class FrmProduct : Form
    {
        // Danh sách gốc luôn được giữ lại, tìm kiếm chỉ thay đổi danh sách hiển thị.
        private readonly List<Product> products = new List<Product>();
        private Product editingProduct;
        private string imagePath = "";
        private string searchTerm = "";
        private bool refreshing;

        public bool SuppressDialogs { get; set; }
        public DialogResult MockConfirmResult { get; set; } = DialogResult.Yes;
        public string LastDialogMessage { get; private set; }

        private DialogResult ShowInfo(string text, string caption, MessageBoxIcon icon)
        {
            LastDialogMessage = text;
            if (SuppressDialogs) return DialogResult.OK;
            return MessageBox.Show(this, text, caption, MessageBoxButtons.OK, icon);
        }

        private DialogResult ShowConfirm(string text, string caption)
        {
            LastDialogMessage = text;
            if (SuppressDialogs) return MockConfirmResult;
            return MessageBox.Show(this, text, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
        }

        public FrmProduct()
        {
            InitializeComponent();
            // Custom painting runs at runtime; the layout remains owned by Designer.
            tsActions.Renderer = new ActionStripRenderer();
        }

        private void FormLoaded(object sender, EventArgs e)
        {
            cboLoaiSP.Items.AddRange(new object[] { "Điện tử", "Văn phòng", "Gia dụng", "Thời trang" });
            products.Add(new Product { MaSP = "SP001", TenSP = "Chuột không dây", LoaiSP = "Điện tử", DonGia = 250000, SoLuong = 20, NgayNhap = DateTime.Today.AddDays(-3), ConKinhDoanh = true, DuongDanAnh = "" });
            products.Add(new Product { MaSP = "SP002", TenSP = "Sổ tay", LoaiSP = "Văn phòng", DonGia = 35000, SoLuong = 50, NgayNhap = DateTime.Today.AddDays(-2), ConKinhDoanh = true, DuongDanAnh = "" });
            products.Add(new Product { MaSP = "SP003", TenSP = "Bình giữ nhiệt", LoaiSP = "Gia dụng", DonGia = 180000, SoLuong = 10, NgayNhap = DateTime.Today.AddDays(-1), ConKinhDoanh = false, DuongDanAnh = "" });
            ResetAll(sender, e);
            UpdateStatus("Sẵn sàng — đã nạp 3 sản phẩm mẫu.");
        }

        private bool ValidateInput(Product excludedProduct)
        {
            errorProvider.Clear();
            bool valid = true;
            if (string.IsNullOrWhiteSpace(txtMaSP.Text))
            { errorProvider.SetError(txtMaSP, "Mã sản phẩm không được rỗng."); valid = false; }
            else if (products.Any(p => p != excludedProduct && string.Equals(p.MaSP, txtMaSP.Text.Trim(), StringComparison.OrdinalIgnoreCase)))
            { errorProvider.SetError(txtMaSP, "Mã sản phẩm đã tồn tại (không phân biệt hoa/thường)."); valid = false; }
            if (string.IsNullOrWhiteSpace(txtTenSP.Text))
            { errorProvider.SetError(txtTenSP, "Tên sản phẩm không được rỗng."); valid = false; }
            if (cboLoaiSP.SelectedIndex < 0)
            { errorProvider.SetError(cboLoaiSP, "Hãy chọn loại sản phẩm."); valid = false; }
            if (numDonGia.Value <= 0)
            { errorProvider.SetError(numDonGia, "Đơn giá phải lớn hơn 0."); valid = false; }
            // NumericUpDown.Minimum = 0 cũng ngăn nhập số âm trên giao diện.
            if (numSoLuong.Value < 0)
            { errorProvider.SetError(numSoLuong, "Số lượng phải lớn hơn hoặc bằng 0."); valid = false; }
            if (dtpNgayNhap.Value.Date > DateTime.Today)
            { errorProvider.SetError(dtpNgayNhap, "Ngày nhập không được lớn hơn ngày hiện tại."); valid = false; }
            if (!valid)
            {
                UpdateStatus("Dữ liệu chưa hợp lệ.");
                ShowInfo("Vui lòng kiểm tra các ô có biểu tượng lỗi.", "Kiểm tra dữ liệu", MessageBoxIcon.Warning);
            }
            return valid;
        }

        private void CopyInputToProduct(Product product)
        {
            product.MaSP = txtMaSP.Text.Trim();
            product.TenSP = txtTenSP.Text.Trim();
            product.LoaiSP = cboLoaiSP.SelectedItem.ToString();
            product.DonGia = numDonGia.Value;
            product.SoLuong = (int)numSoLuong.Value;
            product.NgayNhap = dtpNgayNhap.Value.Date;
            product.ConKinhDoanh = chkConKinhDoanh.Checked;
            product.DuongDanAnh = imagePath;
        }

        private void AddProduct(object sender, EventArgs e)
        {
            if (!ValidateInput(null)) return;
            var product = new Product();
            CopyInputToProduct(product);
            products.Add(product);
            RefreshGrid();
            ClearInput();
            UpdateStatus("Đã thêm " + product.MaSP + ".");
        }

        private void EditProduct(object sender, EventArgs e)
        {
            if (editingProduct == null || !products.Contains(editingProduct))
            { NotifyNoSelection(); return; }
            if (!ValidateInput(editingProduct)) return;
            CopyInputToProduct(editingProduct);
            string code = editingProduct.MaSP;
            RefreshGrid();
            ClearInput();
            UpdateStatus("Đã cập nhật " + code + ".");
        }

        private void DeleteProduct(object sender, EventArgs e)
        {
            Product product = SelectedProduct();
            if (product == null) { NotifyNoSelection(); return; }
            if (ShowConfirm("Xóa sản phẩm " + product.MaSP + " — " + product.TenSP + "?", "Xác nhận xóa") != DialogResult.Yes)
            { UpdateStatus("Đã hủy xóa."); return; }
            products.Remove(product);
            RefreshGrid();
            ClearInput();
            UpdateStatus("Đã xóa " + product.MaSP + ".");
        }

        private void SearchProducts(object sender, EventArgs e)
        {
            searchTerm = txtTimKiem.Text.Trim();
            RefreshGrid();
            UpdateStatus("Tìm thấy " + productSource.Count + " sản phẩm.");
        }

        private void FocusSearch(object sender, EventArgs e)
        { txtTimKiem.Focus(); txtTimKiem.SelectAll(); UpdateStatus("Nhập mã hoặc tên rồi nhấn Enter."); }

        private void ResetAll(object sender, EventArgs e)
        {
            searchTerm = "";
            txtTimKiem.Clear();
            cboSapXep.SelectedIndex = 0;
            RefreshGrid();
            ClearInput();
            txtMaSP.Focus();
            UpdateStatus("Đã làm mới — hiển thị toàn bộ dữ liệu.");
        }

        private void RefreshGrid()
        {
            IEnumerable<Product> view = products.Where(p =>
                p.MaSP.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0 ||
                p.TenSP.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0);
            switch (cboSapXep.SelectedIndex)
            {
                case 1: view = view.OrderBy(p => p.DonGia); break;
                case 2: view = view.OrderByDescending(p => p.DonGia); break;
                case 3: view = view.OrderBy(p => p.SoLuong); break;
                case 4: view = view.OrderByDescending(p => p.SoLuong); break;
            }
            refreshing = true;
            try
            {
                productSource.DataSource = new BindingList<Product>(view.ToList());
                dgvProducts.ClearSelection();
                dgvProducts.CurrentCell = null;
            }
            finally { refreshing = false; }
            ClearInput();
        }

        private void ClearInput()
        {
            editingProduct = null;
            txtMaSP.Clear();
            txtTenSP.Clear();
            cboLoaiSP.SelectedIndex = -1;
            numDonGia.Value = 0;
            numSoLuong.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            imagePath = "";
            SetPreviewImage(null);
            errorProvider.Clear();
        }

        private Product SelectedProduct()
        {
            return dgvProducts.SelectedRows.Count == 0 ? null : dgvProducts.SelectedRows[0].DataBoundItem as Product;
        }

        private void GridSelectionChanged(object sender, EventArgs e)
        {
            if (refreshing) return;
            var product = SelectedProduct();
            if (product != null)
            {
                LoadProductToForm(product);
                UpdateStatus("Đã chọn " + product.MaSP + " — chỉnh thông tin rồi nhấn Sửa.");
            }
            else ClearInput();
        }

        private void LoadProductToForm(Product product)
        {
            errorProvider.Clear();
            editingProduct = product;
            txtMaSP.Text = product.MaSP;
            txtTenSP.Text = product.TenSP;
            cboLoaiSP.SelectedItem = product.LoaiSP;
            numDonGia.Value = product.DonGia;
            numSoLuong.Value = product.SoLuong;
            dtpNgayNhap.Value = product.NgayNhap;
            chkConKinhDoanh.Checked = product.ConKinhDoanh;
            imagePath = product.DuongDanAnh ?? "";
            SetPreviewImage(null);
            if (!string.IsNullOrEmpty(imagePath))
            {
                try { SetPreviewImage(ReadImageCopy(imagePath)); }
                catch (Exception ex) when (IsImageError(ex))
                { errorProvider.SetError(picProduct, "Không đọc được ảnh. Hãy chọn ảnh khác."); }
            }
        }

        private void GridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button != MouseButtons.Right) return;
            if (e.RowIndex < 0) { dgvProducts.ClearSelection(); return; }
            dgvProducts.CurrentCell = dgvProducts.Rows[e.RowIndex].Cells[Math.Max(0, e.ColumnIndex)];
            dgvProducts.Rows[e.RowIndex].Selected = true;
        }

        private void BeginEditSelected(object sender, EventArgs e)
        {
            var product = SelectedProduct();
            if (product == null) { NotifyNoSelection(); return; }
            LoadProductToForm(product);
            txtTenSP.Focus();
            txtTenSP.SelectAll();
            UpdateStatus("Sửa thông tin " + product.MaSP + ", sau đó nhấn Sửa hoặc Ctrl+S để lưu.");
        }

        private void NotifyNoSelection()
        {
            UpdateStatus("Chưa chọn sản phẩm.");
            ShowInfo("Hãy chọn một dòng sản phẩm trước.", "Thông báo", MessageBoxIcon.Information);
        }

        private void ShowDetails(object sender, EventArgs e)
        {
            var p = SelectedProduct();
            if (p == null) { NotifyNoSelection(); return; }
            ShowInfo(string.Format("Mã: {0}\nTên: {1}\nLoại: {2}\nĐơn giá: {3:N2} VNĐ\nSố lượng: {4:N0}\nNgày nhập: {5:dd/MM/yyyy}\nTrạng thái: {6}\nẢnh: {7}", p.MaSP, p.TenSP, p.LoaiSP, p.DonGia, p.SoLuong, p.NgayNhap, p.ConKinhDoanh ? "Còn kinh doanh" : "Ngừng kinh doanh", p.DuongDanAnh), "Chi tiết sản phẩm", MessageBoxIcon.Information);
            UpdateStatus("Đã xem chi tiết " + p.MaSP + ".");
        }

        private static bool IsImageError(Exception ex)
        {
            return ex is IOException || ex is UnauthorizedAccessException || ex is ArgumentException || ex is OutOfMemoryException || ex is System.Runtime.InteropServices.ExternalException || ex is NotSupportedException || ex is System.Security.SecurityException;
        }

        private static Image ReadImageCopy(string path)
        {
            // Sao chép ảnh để không khóa file nguồn sau khi đóng stream.
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var image = Image.FromStream(stream))
                return new Bitmap(image);
        }

        private void SetPreviewImage(Image image)
        {
            Image old = picProduct.Image;
            picProduct.Image = image;
            if (old != null) old.Dispose();
        }

        private void ChooseImage(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog { Title = "Chọn ảnh sản phẩm", Filter = "Tệp ảnh|*.jpg;*.jpeg;*.png;*.bmp;*.gif|Tất cả tệp|*.*", CheckFileExists = true })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) { UpdateStatus("Đã hủy chọn ảnh."); return; }
                try
                {
                    Image image = ReadImageCopy(dialog.FileName);
                    SetPreviewImage(image);
                    imagePath = dialog.FileName;
                    errorProvider.SetError(picProduct, "");
                    UpdateStatus("Đã chọn ảnh; nhấn Thêm/Sửa để lưu sản phẩm.");
                }
                catch (Exception ex) when (IsImageError(ex))
                {
                    UpdateStatus("Không thể mở ảnh.");
                    ShowInfo("Tệp không phải ảnh hợp lệ hoặc không thể đọc. Hãy chọn tệp khác.", "Lỗi ảnh", MessageBoxIcon.Warning);
                }
            }
        }

        private void UpdateStatus(string message)
        {
            lblStatus.Text = message;
            lblCount.Text = "Hiển thị: " + productSource.Count + " / Tổng: " + products.Count;
            decimal total = products.Sum(p => p.DonGia * p.SoLuong);
            lblTotal.Text = "Tồn kho: " + total.ToString("N2") + " VNĐ";
        }

        public void ExportToFile(string filePath)
        {
            var text = new StringBuilder();
            text.AppendLine("DANH SÁCH SẢN PHẨM");
            text.AppendLine("Mã\tTên\tLoại\tĐơn giá (VNĐ)\tSố lượng\tNgày nhập\tCòn kinh doanh\tĐường dẫn ảnh");
            decimal total = 0;
            foreach (Product p in productSource.List)
            {
                text.AppendLine(string.Format("{0}\t{1}\t{2}\t{3:N2}\t{4}\t{5:dd/MM/yyyy}\t{6}\t{7}", p.MaSP, p.TenSP, p.LoaiSP, p.DonGia, p.SoLuong, p.NgayNhap, p.ConKinhDoanh ? "Có" : "Không", p.DuongDanAnh));
                total += p.DonGia * p.SoLuong;
            }
            text.AppendLine("Số sản phẩm xuất: " + productSource.Count);
            text.AppendLine("Giá trị tồn kho của danh sách xuất: " + total.ToString("N2") + " VNĐ");
            File.WriteAllText(filePath, text.ToString(), new UTF8Encoding(true));
            UpdateStatus("Đã xuất " + productSource.Count + " sản phẩm ra TXT.");
        }

        private void ExportProducts(object sender, EventArgs e)
        {
            using (var dialog = new SaveFileDialog { Title = "Xuất danh sách đang hiển thị", Filter = "Tệp văn bản|*.txt", FileName = "DanhSachSanPham.txt", DefaultExt = "txt", AddExtension = true, OverwritePrompt = true })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK) { UpdateStatus("Đã hủy xuất tệp."); return; }
                try
                {
                    ExportToFile(dialog.FileName);
                }
                catch (Exception ex) when (ex is IOException || ex is UnauthorizedAccessException || ex is System.Security.SecurityException)
                {
                    UpdateStatus("Xuất tệp thất bại.");
                    ShowInfo("Không ghi được tệp. Hãy chọn thư mục khác hoặc đóng tệp đang mở.", "Lỗi xuất tệp", MessageBoxIcon.Error);
                }
            }
        }

        private void RemoveImage(object sender, EventArgs e)
        {
            imagePath = "";
            SetPreviewImage(null);
            errorProvider.SetError(picProduct, "");
            UpdateStatus("Đã bỏ ảnh trên ô nhập; nhấn Thêm/Sửa để lưu.");
        }

        private void ImportProductsExcel(object sender, EventArgs e)
        {
            using (var dialog = new OpenFileDialog
            {
                Title = "Nhập sản phẩm từ Excel",
                Filter = "Tệp Excel (*.xlsx)|*.xlsx|Tất cả tệp (*.*)|*.*",
                CheckFileExists = true
            })
            {
                if (dialog.ShowDialog(this) != DialogResult.OK)
                {
                    UpdateStatus("Đã hủy nhập tệp Excel.");
                    return;
                }

                ImportFromFile(dialog.FileName, true);
            }
        }

        public bool ImportFromFile(string filePath, bool showDialogFeedback = false)
        {
            try
            {
                var result = ProductExcelImporter.ImportProductsFromExcel(filePath, products);
                if (!result.Success)
                {
                    UpdateStatus(string.Format("Nhập thất bại: phát hiện {0} lỗi.", result.Errors.Count));
                    if (showDialogFeedback)
                    {
                        string msg = "Không thể nạp dữ liệu. Vui lòng sửa các lỗi sau trong tệp Excel và thử lại:\n\n"
                            + string.Join("\n", result.Errors.Take(15))
                            + (result.Errors.Count > 15 ? string.Format("\n... và {0} lỗi khác.", result.Errors.Count - 15) : "");
                        MessageBox.Show(this, msg, "Lỗi dữ liệu Excel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    return false;
                }

                products.AddRange(result.ImportedProducts);
                searchTerm = "";
                txtTimKiem.Clear();
                cboSapXep.SelectedIndex = 0;
                RefreshGrid();
                ClearInput();
                UpdateStatus(string.Format("Đã nạp {0} sản phẩm từ Excel.", result.ImportedProducts.Count));

                if (showDialogFeedback)
                {
                    string successMsg = string.Format("Đã nhập thành công {0} sản phẩm từ tệp Excel.", result.ImportedProducts.Count);
                    if (result.Warnings.Count > 0)
                    {
                        successMsg += "\n\nCảnh báo (" + result.Warnings.Count + "):\n"
                            + string.Join("\n", result.Warnings.Take(5))
                            + (result.Warnings.Count > 5 ? string.Format("\n... và {0} cảnh báo khác.", result.Warnings.Count - 5) : "");
                    }
                    MessageBox.Show(this, successMsg, "Nhập Excel thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                return true;
            }
            catch (Exception ex)
            {
                UpdateStatus("Không thể đọc tệp Excel.");
                if (showDialogFeedback)
                {
                    ShowInfo("Không thể đọc tệp Excel: " + ex.Message, "Lỗi đọc tệp", MessageBoxIcon.Error);
                }
                return false;
            }
        }

        private void ShowHelp(object sender, EventArgs e)
        {
            ShowInfo("• Thêm: nhập thông tin rồi nhấn Thêm (Ctrl+N).\n• Sửa: chọn dòng, thay đổi thông tin, nhấn Sửa (Ctrl+S).\n• Xóa: chọn dòng, nhấn Xóa (Ctrl+Delete) và xác nhận.\n• Tìm kiếm: nhập mã/tên rồi Enter. Ctrl+F chuyển tới ô tìm.\n• Làm mới (F5): xóa ô nhập và hiện toàn bộ dữ liệu.\n• Nhấp phải: Sửa, Xóa, Xem chi tiết. Nhấp đúp để xem chi tiết.\n• File → Nhập sản phẩm từ Excel (.xlsx) (Ctrl+O): nạp hàng loạt sản phẩm từ file Excel.\n• File → Xuất danh sách TXT (Ctrl+E): xuất các dòng đang hiển thị.\n• Dữ liệu chỉ lưu trong bộ nhớ, đóng ứng dụng sẽ mất thay đổi.", "Hướng dẫn — Lab 06", MessageBoxIcon.Information);
            UpdateStatus("Đã mở hướng dẫn.");
        }

        private void SearchKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            SearchProducts(sender, e);
            e.SuppressKeyPress = true;
        }

        private void SortChanged(object sender, EventArgs e)
        {
            RefreshGrid();
            UpdateStatus("Đã sắp xếp danh sách.");
        }

        private void GridCellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) ShowDetails(sender, e);
        }

        private void ContextProductsOpening(object sender, CancelEventArgs e)
        {
            e.Cancel = SelectedProduct() == null;
        }

        private void ExitApplication(object sender, EventArgs e) { Close(); }

        private void DisposePreviewImage(object sender, EventArgs e)
        {
            if (picProduct.Image != null)
            {
                picProduct.Image.Dispose();
                picProduct.Image = null;
            }
        }

        // Vẽ nền nút rõ ràng, có phản hồi khi rê chuột hoặc nhấn.
        private sealed class ActionStripRenderer : ToolStripProfessionalRenderer
        {
            protected override void OnRenderButtonBackground(ToolStripItemRenderEventArgs e)
            {
                Color color = e.Item.BackColor;
                var button = e.Item as ToolStripButton;
                if (button != null && button.Pressed) color = ControlPaint.Dark(color, 0.12F);
                else if (e.Item.Selected) color = ControlPaint.Dark(color, 0.04F);
                using (var brush = new SolidBrush(color))
                    e.Graphics.FillRectangle(brush, new Rectangle(Point.Empty, e.Item.Size));
                if (e.Item.Selected)
                {
                    using (var pen = new Pen(ControlPaint.Dark(color, 0.15F)))
                        e.Graphics.DrawRectangle(pen, 0, 0, e.Item.Width - 1, e.Item.Height - 1);
                }
            }
            protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }
        }
    }
}
