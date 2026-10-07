using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProductManager
{
    partial class FrmProduct
    {
        private IContainer components;
        private TextBox txtMaSP, txtTenSP, txtTimKiem;
        private ComboBox cboLoaiSP, cboSapXep;
        private NumericUpDown numDonGia, numSoLuong;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;
        private PictureBox picProduct;
        private DataGridView dgvProducts;
        private ToolStripStatusLabel lblStatus, lblCount, lblTotal;
        private ErrorProvider errorProvider;
        private BindingSource productSource;
        private ContextMenuStrip contextProducts;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (picProduct != null && picProduct.Image != null) picProduct.Image.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new Container();
            productSource = new BindingSource(components);
            errorProvider = new ErrorProvider(components) { ContainerControl = this, BlinkStyle = ErrorBlinkStyle.NeverBlink };
            Text = "Quản lý sản phẩm";
            Name = "FrmProduct";
            StartPosition = FormStartPosition.CenterScreen;
            ClientSize = new Size(1160, 750);
            MinimumSize = new Size(1050, 730);
            BackColor = Color.FromArgb(243, 246, 250);
            ForeColor = Color.FromArgb(35, 48, 65);
            Font = new Font("Segoe UI", 10F);
            AutoScaleMode = AutoScaleMode.Font;

            var menu = new MenuStrip { BackColor = Color.White, Padding = new Padding(12, 4, 0, 4) };
            var file = new ToolStripMenuItem("&File");
            file.DropDownItems.Add(MenuItem("Nhập sản phẩm từ Excel (.xlsx)", ImportProductsExcel, Keys.Control | Keys.O));
            file.DropDownItems.Add(MenuItem("Xuất danh sách TXT", ExportProducts, Keys.Control | Keys.E));
            file.DropDownItems.Add(MenuItem("Thoát", delegate { Close(); }, Keys.Alt | Keys.F4));
            var manage = new ToolStripMenuItem("&Quản lý");
            manage.DropDownItems.Add(MenuItem("Thêm", AddProduct, Keys.Control | Keys.N));
            manage.DropDownItems.Add(MenuItem("Sửa / Lưu thay đổi", EditProduct, Keys.Control | Keys.S));
            manage.DropDownItems.Add(MenuItem("Xóa", DeleteProduct, Keys.Control | Keys.Delete));
            manage.DropDownItems.Add(MenuItem("Làm mới", ResetAll, Keys.F5));
            manage.DropDownItems.Add(MenuItem("Tìm kiếm", FocusSearch, Keys.Control | Keys.F));
            var help = new ToolStripMenuItem("&Trợ giúp");
            help.DropDownItems.Add(MenuItem("Hướng dẫn", ShowHelp, Keys.F1));
            menu.Items.AddRange(new ToolStripItem[] { file, manage, help });
            MainMenuStrip = menu;

            // Thanh thao tác nằm ngay dưới khu vực nhập liệu, vẫn dùng ToolStrip theo đề.
            var toolbar = new ToolStrip {
                Name = "tsActions", Dock = DockStyle.Fill, AutoSize = false,
                GripStyle = ToolStripGripStyle.Hidden, BackColor = Color.White,
                Padding = new Padding(12, 6, 12, 6), Margin = new Padding(3, 0, 3, 8),
                Renderer = new ActionStripRenderer(), CanOverflow = false,
                LayoutStyle = ToolStripLayoutStyle.HorizontalStackWithOverflow
            };
            toolbar.Items.Add(ToolButton("Thêm", AddProduct, Color.FromArgb(37, 99, 235), Color.White, "Ctrl+N"));
            toolbar.Items.Add(ToolButton("Sửa", EditProduct, Color.FromArgb(231, 239, 255), Color.FromArgb(29, 78, 216), "Ctrl+S"));
            toolbar.Items.Add(ToolButton("Xóa", DeleteProduct, Color.FromArgb(255, 235, 238), Color.FromArgb(185, 28, 28), "Ctrl+Delete"));

            toolbar.Items.Add(ToolButton("Làm mới", ResetAll, Color.FromArgb(237, 241, 246), ForeColor, "F5"));
            toolbar.Items.Add(ToolButton("Tìm kiếm", SearchProducts, Color.FromArgb(237, 241, 246), ForeColor, "Nhập mã / tên trong ô bên dưới"));

            var status = new StatusStrip { BackColor = Color.White, Padding = new Padding(12, 4, 12, 4) };
            lblStatus = new ToolStripStatusLabel { Name = "lblStatus", Spring = true, TextAlign = ContentAlignment.MiddleLeft };
            lblCount = new ToolStripStatusLabel();
            lblTotal = new ToolStripStatusLabel();
            status.Items.AddRange(new ToolStripItem[] { lblStatus, lblCount, lblTotal });

            var body = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(16, 12, 16, 8), ColumnCount = 1, RowCount = 4 };
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 270));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 56));
            body.RowStyles.Add(new RowStyle(SizeType.Absolute, 55));
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100));
            var group = new GroupBox { Text = "Thông tin sản phẩm", Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10), Margin = new Padding(3, 3, 3, 0) };
            var fields = new TableLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(12), ColumnCount = 5, RowCount = 5 };
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 120));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50));
            fields.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 205));
            for (int i = 0; i < 5; i++) fields.RowStyles.Add(new RowStyle(SizeType.Percent, 20));
            txtMaSP = new TextBox { Name = "txtMaSP", MaxLength = 50 };
            txtTenSP = new TextBox { Name = "txtTenSP", MaxLength = 200 };
            cboLoaiSP = new ComboBox { Name = "cboLoaiSP", DropDownStyle = ComboBoxStyle.DropDownList };
            numDonGia = new NumericUpDown { Name = "numDonGia", Maximum = 1000000000000M, Minimum = 0, DecimalPlaces = 2, ThousandsSeparator = true };
            numSoLuong = new NumericUpDown { Name = "numSoLuong", Maximum = int.MaxValue, Minimum = 0, ThousandsSeparator = true };
            dtpNgayNhap = new DateTimePicker { Name = "dtpNgayNhap", Format = DateTimePickerFormat.Custom, CustomFormat = "dd/MM/yyyy" };
            chkConKinhDoanh = new CheckBox { Name = "chkConKinhDoanh", Text = "Còn kinh doanh", Checked = true, AutoSize = true };
            AddField(fields, "Mã sản phẩm *", txtMaSP, 0, 0);
            AddField(fields, "Tên sản phẩm *", txtTenSP, 0, 1);
            AddField(fields, "Loại sản phẩm *", cboLoaiSP, 0, 2);
            AddField(fields, "Đơn giá (VNĐ) *", numDonGia, 2, 0);
            AddField(fields, "Số lượng *", numSoLuong, 2, 1);
            AddField(fields, "Ngày nhập *", dtpNgayNhap, 2, 2);
            fields.Controls.Add(chkConKinhDoanh, 1, 3);
            var hint = new Label { Text = "Chọn một dòng để sửa thông tin. Nhấn Làm mới để nhập sản phẩm khác.", Dock = DockStyle.Fill, AutoSize = true, ForeColor = Color.FromArgb(100, 116, 139), Padding = new Padding(0, 6, 0, 0) };
            fields.Controls.Add(hint, 0, 4);
            fields.SetColumnSpan(hint, 4);
            picProduct = new PictureBox { Name = "picProduct", Dock = DockStyle.Fill, BorderStyle = BorderStyle.FixedSingle, SizeMode = PictureBoxSizeMode.Zoom, BackColor = Color.White };
            fields.Controls.Add(picProduct, 4, 0);
            fields.SetRowSpan(picProduct, 3);
            var imageButtons = new FlowLayoutPanel { Dock = DockStyle.Fill, WrapContents = false };
            var choose = new Button { Name = "btnChonAnh", Text = "Chọn ảnh", AutoSize = true };
            choose.Click += ChooseImage;
            var remove = new Button { Name = "btnBoAnh", Text = "Bỏ ảnh", AutoSize = true };
            remove.Click += RemoveImage;
            imageButtons.Controls.Add(choose);
            imageButtons.Controls.Add(remove);
            fields.Controls.Add(imageButtons, 4, 3);
            fields.SetRowSpan(imageButtons, 2);
            group.Controls.Add(fields);
            body.Controls.Add(group, 0, 0);
            body.Controls.Add(toolbar, 0, 1);

            var searchBar = new FlowLayoutPanel { Dock = DockStyle.Fill, Padding = new Padding(0, 8, 0, 0), WrapContents = false };
            searchBar.Controls.Add(new Label { Text = "Mã / tên:", AutoSize = true, Margin = new Padding(3, 7, 3, 0) });
            txtTimKiem = new TextBox { Name = "txtTimKiem", Width = 235 };
            txtTimKiem.KeyDown += delegate(object sender, KeyEventArgs e) { if (e.KeyCode == Keys.Enter) { SearchProducts(sender, e); e.SuppressKeyPress = true; } };
            searchBar.Controls.Add(txtTimKiem);
            var find = new Button { Text = "Tìm kiếm", AutoSize = true };
            find.Click += SearchProducts;
            searchBar.Controls.Add(find);
            searchBar.Controls.Add(new Label { Text = "Sắp xếp:", AutoSize = true, Margin = new Padding(20, 7, 3, 0) });
            cboSapXep = new ComboBox { Width = 215, DropDownStyle = ComboBoxStyle.DropDownList };
            cboSapXep.Items.AddRange(new object[] { "Thứ tự ban đầu", "Đơn giá tăng dần", "Đơn giá giảm dần", "Số lượng tăng dần", "Số lượng giảm dần" });
            cboSapXep.SelectedIndex = 0;
            cboSapXep.SelectedIndexChanged += delegate { RefreshGrid(); UpdateStatus("Đã sắp xếp danh sách."); };
            searchBar.Controls.Add(cboSapXep);
            body.Controls.Add(searchBar, 0, 2);

            dgvProducts = new DataGridView { Name = "dgvProducts", Dock = DockStyle.Fill, ReadOnly = true, SelectionMode = DataGridViewSelectionMode.FullRowSelect, MultiSelect = false, AutoGenerateColumns = false, AllowUserToAddRows = false, AllowUserToDeleteRows = false, RowHeadersVisible = false, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, BackgroundColor = Color.White };
            AddTextColumn("MaSP", "Mã SP", 70, null);
            AddTextColumn("TenSP", "Tên sản phẩm", 220, null);
            AddTextColumn("LoaiSP", "Loại", 90, null);
            AddTextColumn("DonGia", "Đơn giá (VNĐ)", 110, "N2");
            AddTextColumn("SoLuong", "Số lượng", 65, "N0");
            AddTextColumn("NgayNhap", "Ngày nhập", 90, "dd/MM/yyyy");
            dgvProducts.Columns.Add(new DataGridViewCheckBoxColumn { DataPropertyName = "ConKinhDoanh", HeaderText = "Còn KD", FillWeight = 60, SortMode = DataGridViewColumnSortMode.NotSortable });
            AddTextColumn("DuongDanAnh", "Đường dẫn ảnh", 130, null);
            dgvProducts.BorderStyle = BorderStyle.None;
            dgvProducts.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvProducts.GridColor = Color.FromArgb(228, 234, 242);
            dgvProducts.EnableHeadersVisualStyles = false;
            dgvProducts.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvProducts.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvProducts.ColumnHeadersHeight = 40;
            dgvProducts.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(232, 238, 247);
            dgvProducts.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);
            dgvProducts.ColumnHeadersDefaultCellStyle.Font = new Font(Font, FontStyle.Bold);
            dgvProducts.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(232, 238, 247);
            dgvProducts.DefaultCellStyle.Padding = new Padding(6, 0, 6, 0);
            dgvProducts.DefaultCellStyle.SelectionBackColor = Color.FromArgb(219, 234, 254);
            dgvProducts.DefaultCellStyle.SelectionForeColor = Color.FromArgb(30, 58, 138);
            dgvProducts.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 253);
            dgvProducts.RowTemplate.Height = 36;
            dgvProducts.AllowUserToResizeRows = false;
            dgvProducts.DataSource = productSource;
            dgvProducts.SelectionChanged += GridSelectionChanged;
            dgvProducts.CellMouseDown += GridCellMouseDown;
            dgvProducts.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e) { if (e.RowIndex >= 0) ShowDetails(sender, e); };
            contextProducts = new ContextMenuStrip(components);
            contextProducts.Items.Add(MenuItem("Sửa", BeginEditSelected, Keys.None));
            contextProducts.Items.Add(MenuItem("Xóa", DeleteProduct, Keys.None));
            contextProducts.Items.Add(MenuItem("Xem chi tiết", ShowDetails, Keys.None));
            contextProducts.Opening += delegate(object sender, CancelEventArgs e) { e.Cancel = SelectedProduct() == null; };
            dgvProducts.ContextMenuStrip = contextProducts;
            body.Controls.Add(dgvProducts, 0, 3);
            Controls.Add(body);
            Controls.Add(menu);
            Controls.Add(status);
            Load += FormLoaded;
        }

        private ToolStripMenuItem MenuItem(string text, System.EventHandler action, Keys shortcut)
        {
            var item = new ToolStripMenuItem(text) { ShortcutKeys = shortcut };
            item.Click += action;
            return item;
        }
        private ToolStripButton ToolButton(string text, System.EventHandler action, Color background, Color foreground, string shortcut)
        {
            var button = new ToolStripButton(text) {
                DisplayStyle = ToolStripItemDisplayStyle.Text,
                AutoSize = false, Size = new Size(112, 36),
                Margin = new Padding(0, 0, 10, 0),
                BackColor = background, ForeColor = foreground,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ToolTipText = text + " — " + shortcut
            };
            button.Click += action;
            return button;
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
        private void AddField(TableLayoutPanel table, string label, Control control, int column, int row)
        {
            table.Controls.Add(new Label { Text = label, AutoSize = true, Anchor = AnchorStyles.Left }, column, row);
            control.Dock = DockStyle.Top;
            control.Margin = new Padding(3, 5, 20, 3);
            table.Controls.Add(control, column + 1, row);
        }
        private void AddTextColumn(string property, string title, int weight, string format)
        {
            var column = new DataGridViewTextBoxColumn { DataPropertyName = property, HeaderText = title, FillWeight = weight, SortMode = DataGridViewColumnSortMode.NotSortable };
            if (format != null) column.DefaultCellStyle.Format = format;
            dgvProducts.Columns.Add(column);
        }
    }
}
