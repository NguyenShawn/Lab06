# ProductManager — LAB 06
Môn COMP1019: Lập trình trên Windows.

## Mở và chạy
1. Giải nén toàn bộ thư mục `ProductManager`.
2. Trên Windows, mở `ProductManager.sln` bằng Visual Studio có workload **.NET desktop development** và **.NET Framework 4.8 targeting pack**.
3. Dependency: Thư viện `DocumentFormat.OpenXml` (v3.5.1, bản quyền MIT) dùng để đọc/xử lý file Excel `.xlsx` an toàn, không yêu cầu cài đặt Microsoft Office / Excel và không dùng Office Interop. Package được cấu hình qua `<PackageReference>` tiêu chuẩn; Visual Studio hoặc `dotnet restore` sẽ tự động tải thư viện khi build.
4. Lệnh build bằng command line:
   ```shell
   dotnet build ProductManager.sln --configuration Release
   ```
5. Nhấn **F5** hoặc **Ctrl+F5** để chạy chương trình.

## Chỉnh giao diện bằng Designer
- Trong Solution Explorer, nhấp phải `FrmProduct.cs` → **View Designer** (hoặc Shift+F7). Toolbox và cửa sổ Properties dùng để thêm, di chuyển và cấu hình control; tab sự kiện (biểu tượng tia sét) chọn các handler trong `FrmProduct.cs`.
- Giữ `InitializeComponent` trong `FrmProduct.Designer.cs` do Visual Studio quản lý. Xử lý nghiệp vụ và renderer tùy chỉnh nằm trong `FrmProduct.cs`.
- Khi đưa lên Git hoặc gửi sang máy khác, phải kèm `FrmProduct.cs`, `FrmProduct.Designer.cs`, `FrmProduct.resx` và `ProductManager.csproj`. `.resx` chứa thông tin thiết kế của form và đã được khai báo là EmbeddedResource trong dự án.
- Khôi phục NuGet trước khi build; dự án dùng PackageReference, không cần thêm DLL từ thư mục NuGet của người khác. Trong Developer PowerShell for Visual Studio có thể chạy `msbuild ProductManager.sln /restore /p:Configuration=Release`.

- Liên kết dữ liệu của bảng được thiết lập trong `FormLoaded` (`FrmProduct.cs`). Giữ các cột trong Designer ở trạng thái không liên kết; không đặt `productSource.DataSource = typeof(ProductManager.Product)` trong Designer, vì trình thiết kế có thể chưa nạp được kiểu này khi mở bản nguồn chưa build.

## Bố cục giao diện
Menu ở trên cùng (File, Quản lý, Trợ giúp); thông tin sản phẩm và ảnh ở phía trên; thanh nút Thêm, Sửa, Xóa, Làm mới, Tìm kiếm nằm ngay dưới khối nhập liệu. Khu tìm kiếm/sắp xếp ở trên bảng. Bảng có dòng xen kẽ, tiêu đề rõ ràng và màu chọn nhẹ. Dưới cùng là StatusStrip hiển thị trạng thái, số lượng và tổng tồn kho.

## Sử dụng
- Khi mở form: có 3 sản phẩm mẫu và 4 loại: Điện tử, Văn phòng, Gia dụng, Thời trang.
- **Thêm:** nhập thông tin, chọn loại, nhấn Thêm hoặc **Ctrl+N**. Nếu đang chọn dòng cũ, nhấn Làm mới trước khi nhập sản phẩm khác.
- **Sửa:** chọn một dòng, đổi thông tin rồi nhấn Sửa hoặc **Ctrl+S**. Có thể sửa cả mã; mã mới phải không trùng sản phẩm khác.
- **Xóa:** chọn dòng, nhấn Xóa hoặc **Ctrl+Delete**; chỉ xóa khi xác nhận Có.
- **Tìm:** nhập một phần mã/tên rồi Enter hoặc nút Tìm kiếm. Không phân biệt hoa/thường; không bỏ qua dấu tiếng Việt.
- **Làm mới / F5:** xóa ô nhập, lỗi, từ khóa; khôi phục thứ tự và toàn bộ danh sách, không xóa dữ liệu đã thêm.
- **Chọn ảnh:** chọn ảnh hợp lệ, sau đó nhấn Thêm/Sửa để lưu đường dẫn. Bỏ ảnh xóa ảnh preview và xóa ErrorProvider trên ô ảnh.
- Nhấn chuột phải một dòng để Sửa, Xóa, Xem chi tiết. Sửa trong menu chuột phải đưa thông tin lên ô nhập; Ctrl+S lưu thay đổi. Nhấp đúp để xem chi tiết.
- Chọn thứ tự đơn giá/số lượng trên ComboBox sắp xếp.
- **File → Xuất danh sách TXT / Ctrl+E:** xuất danh sách đang hiển thị sau tìm kiếm/sắp xếp, dùng UTF-8 có BOM.
- **File → Nhập sản phẩm từ Excel (.xlsx) / Ctrl+O:** nạp hàng loạt sản phẩm từ file Excel vào danh sách hiện tại.
- StatusStrip hiển thị trạng thái, số dòng đang hiển thị/tổng sản phẩm và tổng giá trị tồn kho **toàn bộ danh sách**, gồm cả sản phẩm ngừng kinh doanh.

## Tính năng bổ sung: Nạp sản phẩm từ Excel (.xlsx)
- **Vị trí:** Menu `File → Nhập sản phẩm từ Excel (.xlsx)` (phím tắt `Ctrl+O`).
- **Cơ chế nạp:** Nhập nối tiếp vào danh sách hiện tại trong bộ nhớ; không quét thư mục tự động, không ghi đè sản phẩm cũ.
- **Nguyên tắc giao dịch All-or-Nothing:** Đọc và kiểm tra tính hợp lệ của toàn bộ file trước khi nạp. Nếu có bất kỳ lỗi dữ liệu nào, ứng dụng từ chối toàn bộ file, hiển thị danh sách lỗi chi tiết theo dòng/cột để người dùng chỉnh sửa và thử lại.
- **Quy ước cột:**
  - Đọc worksheet đầu tiên, hàng 1 là tiêu đề (cho phép đổi thứ tự cột, tự động trim và so khớp không phân biệt hoa/thường; bỏ qua các cột ngoài mẫu).
  - 7 cột bắt buộc: `MaSP`, `TenSP`, `LoaiSP`, `DonGia`, `SoLuong`, `NgayNhap`, `ConKinhDoanh`.
  - 1 cột tùy chọn: `DuongDanAnh` (để trống được chấp nhận; đường dẫn tương đối tính từ thư mục chứa file Excel; nếu file ảnh không tồn tại sẽ cảnh báo nhưng không chặn nạp sản phẩm).
  - Bỏ qua các dòng hoàn toàn trống.
- **Quy tắc kiểm tra dữ liệu:**
  - `MaSP`: ô định dạng Text để bảo toàn số 0 ở đầu, không rỗng; kiểm tra trùng mã không phân biệt hoa/thường cả trong file và với toàn bộ danh sách hiện có.
  - `TenSP`: không rỗng sau khi trim.
  - `LoaiSP`: chỉ chấp nhận 4 loại chuẩn (Điện tử, Văn phòng, Gia dụng, Thời trang).
  - `DonGia`: ô số Excel, > 0, tối đa 1.000.000.000.000 VNĐ, tối đa 2 chữ số thập phân; từ chối chuỗi văn bản.
  - `SoLuong`: ô số nguyên Excel, >= 0 và <= 2.147.483.647; từ chối số lẻ thập phân hoặc dạng chuỗi.
  - `NgayNhap`: nhận ô ngày Excel (tự động nhận diện và hỗ trợ cả hệ ngày chuẩn 1900 lẫn hệ ngày 1904 qua `WorkbookProperties.Date1904`) hoặc chuỗi `dd/MM/yyyy`, không lớn hơn ngày hiện tại và nằm trong khoảng hỗ trợ.
  - `ConKinhDoanh`: nhận boolean Excel hoặc text `Có/Không`, `True/False`, `1/0`.
  - `Công thức`: từ chối các ô chứa công thức trong các cột dữ liệu cần nạp, yêu cầu chuyển thành giá trị tĩnh.
- **File mẫu:** Tệp mẫu chuẩn `Samples/Products_Import_Template.xlsx` chứa sẵn 4 sản phẩm mẫu hợp lệ (`SP004` đến `SP007`).

## Cấu trúc mã nguồn
- `Product.cs`: đủ 8 thuộc tính theo đề.
- `Program.cs`: điểm khởi chạy, thiết lập CultureInfo tiếng Việt (`vi-VN`).
- `ProductExcelImporter.cs`: module đọc và kiểm tra tính hợp lệ file XLSX bằng OpenXml, hỗ trợ hệ ngày 1900/1904, tạo file mẫu.
- `FrmProduct.Designer.cs`: giao diện MenuStrip, ToolStrip, StatusStrip, GroupBox, DataGridView, ContextMenuStrip, PictureBox, ErrorProvider.
- `FrmProduct.cs`: quản lý danh sách sản phẩm, BindingSource, CRUD, tìm kiếm, sắp xếp, xử lý ảnh an toàn, xuất TXT và nạp XLSX.
