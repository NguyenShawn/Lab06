# Bàn giao: kiểm tra nút Tìm kiếm và cảnh báo NonComVisibleBaseClass

## Yêu cầu và phạm vi

Người dùng yêu cầu đánh giá hai ảnh báo lỗi; nếu có vấn đề thật thì viết Markdown để agent khác tiếp tục. Lượt bàn giao này chỉ tạo tài liệu, chưa sửa code hay cài đặt debugger.

Dự án: `C:\Users\ADMIN\Downloads\Lab06_publish`
Solution: `ProductManager.sln`
Ứng dụng WinForms, .NET Framework 4.8. Giữ framework, chức năng nghiệp vụ và khả năng chỉnh FrmProduct bằng View Designer.

## Kết luận có căn cứ

### 1. Nút Tìm kiếm cạnh Làm mới

Ảnh đầu có lời báo nút trên ToolStrip không có tác dụng. Ảnh tĩnh chưa chứng minh được lỗi sự kiện hay kết quả sau khi nhấn.

Mã hiện tại đã có:

```csharp
this.tsbSearch.Click += new System.EventHandler(this.SearchProducts);
```

`SearchProducts` lấy `txtTimKiem.Text.Trim()`, gọi `RefreshGrid()` rồi cập nhật số kết quả trên thanh trạng thái. Vì vậy không được kết luận nút thiếu Click handler hoặc tự động gắn thêm handler lần nữa.

Nếu từ khóa rỗng, kết quả vẫn là toàn bộ danh sách. Nếu danh sách đã lọc theo cùng từ khóa, nhấn lại có thể không thay đổi bảng. Đây là các tình huống cần phân biệt với nút thực sự không hoạt động. `FocusSearch` là handler khác, hiện đưa con trỏ vào ô tìm kiếm; không tự ý đổi toolbar sang handler này nếu chưa xác định hành vi mong muốn.

### 2. Visual Studio dừng ở NonComVisibleBaseClass

Ảnh thứ hai xác nhận debugger bị ngắt bởi Managed Debugging Assistant `NonComVisibleBaseClass`. Nội dung chỉ rõ truy vấn COM QueryInterface tới `DataGridViewTextBoxCellAccessibleObject`, kế thừa `DataGridViewCellAccessibleObject` không COM-visible. Call stack trong ảnh chủ yếu là External Code, dừng ở dòng:

```csharp
Application.Run(new FrmProduct());
```

Dòng này là vòng lặp thông điệp của ứng dụng; vị trí dừng không chứng minh chính dòng đó sai. Chưa xác định tác nhân gọi accessibility, chưa chứng minh cảnh báo làm nút Tìm kiếm hỏng, và chưa xác nhận ứng dụng crash khi chạy không có debugger.

Có thể có công cụ UI Automation/accessibility tham gia; đây chỉ là giả thuyết cần kiểm chứng. Trước đó phiên sửa giao diện đã sử dụng computer-use, nhưng không được mặc định công cụ này là nguyên nhân.

Tài liệu Microsoft về MDA:
https://learn.microsoft.com/en-us/dotnet/framework/debug-trace-profile/noncomvisiblebaseclass-mda

Tài liệu giải thích điều kiện kích hoạt do COM QueryInterface và quan hệ lớp COM-visible/base không COM-visible. Không áp dụng máy móc cách sửa lớp COM tự viết cho các lớp framework trong thông báo này.

## Việc agent tiếp theo cần làm

1. Đọc hướng dẫn repository nếu có; kiểm tra Git diff hiện tại trước khi sửa. Có thay đổi chưa commit từ lần sửa Designer, không ghi đè hoặc reset chúng. Đọc lại mã mới nhất vì người dùng có thể tiếp tục chỉnh trong Visual Studio.
2. Dùng skill computer-use khi thao tác Visual Studio. Ghi nhận cấu hình Debug/Release, framework/runtime và trạng thái debugger; tránh dùng tác vụ truy vấn accessibility liên tục khi đang xác định tác nhân gây cảnh báo.
3. Tái hiện nút toolbar với từ khóa `SP001` từ danh sách 3 mẫu chưa lọc: phải còn 1 dòng và trạng thái báo 1 kết quả. Thử từ khóa không tồn tại: 0 dòng; xóa từ khóa: trở lại toàn bộ. So sánh nút toolbar, nút cạnh ô tìm và Enter. Ghi rõ thao tác nào thực sự thất bại.
4. Kiểm tra breakpoint vào `SearchProducts` khi nhấn toolbar; xem debugger có đang ở trạng thái Break trước thao tác hay không. Nếu handler chạy đúng, kiểm tra kết quả binding và thông báo trạng thái; không thêm handler trùng.
5. Với MDA, thu thập đầy đủ thông báo và call stack có External Code; so sánh F5 với Ctrl+F5 hoặc chạy EXE trực tiếp. So sánh khi công cụ accessibility/automation đang hoạt động và khi không có truy vấn từ công cụ đó. Ghi lại thao tác kích hoạt thay vì khẳng định một nguyên nhân chưa được chứng minh.
6. Chọn xử lý theo bằng chứng. Nếu lỗi thuộc code ứng dụng, sửa tối thiểu và kiểm tra lại. Nếu chỉ là debugger ngắt vì MDA trong framework/accessibility, mô tả rõ giới hạn; thay đổi tùy chọn break của riêng MDA (nếu thực sự phù hợp) chỉ là cách tránh ngắt debugger, không phải sửa nguyên nhân COM. Không tắt toàn bộ exception/MDA, không thêm ComVisible lên form hay bọc Application.Run bằng catch để che lỗi.
7. Nếu cần cập nhật sự kiện/layout, dùng Properties/Designer theo yêu cầu người dùng; để xử lý và renderer trong FrmProduct.cs. Giữ các control name được nghiệp vụ sử dụng.

## Tiêu chí kiểm chứng

- Toolbar Tìm kiếm, nút tìm cạnh ô nhập và Enter có kết quả đúng ở các trường hợp trên; không phát sinh handler gọi hai lần.
- Phân biệt được kết quả F5 và chạy ngoài debugger; ghi rõ MDA đã sửa nguyên nhân, chỉ tránh ngắt, hay còn chưa tái hiện được.
- Build thành công; View Designer mở, lưu, đóng và mở lại được, giữ đủ 8 cột của bảng.
- Kiểm tra lại các chức năng chịu ảnh hưởng bởi thay đổi; không tuyên bố máy khác hoạt động khi chưa kiểm chứng ở máy đó.
- Khi commit, đưa cả FrmProduct.resx mới vào Git cùng các file liên quan.

## Kết quả đã có từ lần sửa trước

- Designer cũ lỗi phân tích InitializeComponent; đã dựng lại control, chuyển helper/renderer ra FrmProduct.cs và để Visual Studio lưu mã Designer.
- Đã kiểm chứng kéo control, lưu, mở lại và khôi phục vị trí; đủ 8 cột.
- Debug build và Release build từ bản sao nguồn với thư mục NuGet mới đã thành công trên máy hiện tại.
- 27 kiểm tra chức năng đã qua. Bộ kiểm tra có tìm kiếm bằng nút cạnh ô nhập, nhưng không kiểm chứng riêng Click của tsbSearch và không kiểm chứng trường hợp MDA dưới debugger. Không dùng kết quả này để bác bỏ báo lỗi mới.
- Đã thử qua UI: chọn dòng, chọn/bỏ ảnh, nhập 4 sản phẩm Excel mẫu và xuất TXT.

Bản sao lưu và mã kiểm thử ngoài repository:
`C:\Users\ADMIN\Downloads\Lab06_FrmProduct_backup_20261014`

Các file cần xem: `ProductManager\FrmProduct.cs`, `ProductManager\FrmProduct.Designer.cs`, `ProductManager\FrmProduct.resx`, `ProductManager\Program.cs`, `ProductManager\ProductManager.csproj`.
