namespace ProductManager
{
    partial class FrmProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle7 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle8 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle12 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle9 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle10 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle11 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grpProduct = new System.Windows.Forms.GroupBox();
            this.picProduct = new System.Windows.Forms.PictureBox();
            this.lblHint = new System.Windows.Forms.Label();
            this.lblNgayNhap = new System.Windows.Forms.Label();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.lblDonGia = new System.Windows.Forms.Label();
            this.lblLoaiSP = new System.Windows.Forms.Label();
            this.lblTenSP = new System.Windows.Forms.Label();
            this.lblMaSP = new System.Windows.Forms.Label();
            this.btnBoAnh = new System.Windows.Forms.Button();
            this.btnChonAnh = new System.Windows.Forms.Button();
            this.chkConKinhDoanh = new System.Windows.Forms.CheckBox();
            this.dtpNgayNhap = new System.Windows.Forms.DateTimePicker();
            this.numSoLuong = new System.Windows.Forms.NumericUpDown();
            this.numDonGia = new System.Windows.Forms.NumericUpDown();
            this.cboLoaiSP = new System.Windows.Forms.ComboBox();
            this.txtTenSP = new System.Windows.Forms.TextBox();
            this.txtMaSP = new System.Windows.Forms.TextBox();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.cboSapXep = new System.Windows.Forms.ComboBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.lblSapXep = new System.Windows.Forms.Label();
            this.dgvProducts = new System.Windows.Forms.DataGridView();
            this.contextProducts = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxDetails = new System.Windows.Forms.ToolStripMenuItem();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.menuMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuImport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExport = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuManage = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuReset = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuGuide = new System.Windows.Forms.ToolStripMenuItem();
            this.statusMain = new System.Windows.Forms.StatusStrip();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblTotal = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsActions = new System.Windows.Forms.ToolStrip();
            this.tsbAdd = new System.Windows.Forms.ToolStripButton();
            this.tsbEdit = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbReset = new System.Windows.Forms.ToolStripButton();
            this.tsbSearch = new System.Windows.Forms.ToolStripButton();
            this.colMaSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTenSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colLoaiSP = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colNgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colConKinhDoanh = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.colDuongDanAnh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.productSource = new System.Windows.Forms.BindingSource(this.components);
            this.grpProduct.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).BeginInit();
            this.contextProducts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.menuMain.SuspendLayout();
            this.statusMain.SuspendLayout();
            this.tsActions.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grpProduct
            // 
            this.grpProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grpProduct.BackColor = System.Drawing.Color.White;
            this.grpProduct.Controls.Add(this.picProduct);
            this.grpProduct.Controls.Add(this.lblHint);
            this.grpProduct.Controls.Add(this.lblNgayNhap);
            this.grpProduct.Controls.Add(this.lblSoLuong);
            this.grpProduct.Controls.Add(this.lblDonGia);
            this.grpProduct.Controls.Add(this.lblLoaiSP);
            this.grpProduct.Controls.Add(this.lblTenSP);
            this.grpProduct.Controls.Add(this.lblMaSP);
            this.grpProduct.Controls.Add(this.btnBoAnh);
            this.grpProduct.Controls.Add(this.btnChonAnh);
            this.grpProduct.Controls.Add(this.chkConKinhDoanh);
            this.grpProduct.Controls.Add(this.dtpNgayNhap);
            this.grpProduct.Controls.Add(this.numSoLuong);
            this.grpProduct.Controls.Add(this.numDonGia);
            this.grpProduct.Controls.Add(this.cboLoaiSP);
            this.grpProduct.Controls.Add(this.txtTenSP);
            this.grpProduct.Controls.Add(this.txtMaSP);
            this.grpProduct.Location = new System.Drawing.Point(19, 44);
            this.grpProduct.Name = "grpProduct";
            this.grpProduct.Size = new System.Drawing.Size(1122, 270);
            this.grpProduct.TabIndex = 0;
            this.grpProduct.TabStop = false;
            this.grpProduct.Text = "Thông tin sản phẩm";
            // 
            // picProduct
            // 
            this.picProduct.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picProduct.Location = new System.Drawing.Point(898, 37);
            this.picProduct.Name = "picProduct";
            this.picProduct.Size = new System.Drawing.Size(195, 132);
            this.picProduct.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picProduct.TabIndex = 16;
            this.picProduct.TabStop = false;
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHint.Location = new System.Drawing.Point(22, 225);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(462, 19);
            this.lblHint.TabIndex = 15;
            this.lblHint.Text = "Chọn một dòng để sửa thông tin. Nhấn Làm mới để nhập sản phẩm khác.";
            // 
            // lblNgayNhap
            // 
            this.lblNgayNhap.AutoSize = true;
            this.lblNgayNhap.Location = new System.Drawing.Point(400, 133);
            this.lblNgayNhap.Name = "lblNgayNhap";
            this.lblNgayNhap.Size = new System.Drawing.Size(86, 19);
            this.lblNgayNhap.TabIndex = 14;
            this.lblNgayNhap.Text = "Ngày nhập *";
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Location = new System.Drawing.Point(400, 87);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(73, 19);
            this.lblSoLuong.TabIndex = 13;
            this.lblSoLuong.Text = "Số lượng *";
            // 
            // lblDonGia
            // 
            this.lblDonGia.AutoSize = true;
            this.lblDonGia.Location = new System.Drawing.Point(400, 41);
            this.lblDonGia.Name = "lblDonGia";
            this.lblDonGia.Size = new System.Drawing.Size(108, 19);
            this.lblDonGia.TabIndex = 12;
            this.lblDonGia.Text = "Đơn giá (VNĐ) *";
            // 
            // lblLoaiSP
            // 
            this.lblLoaiSP.AutoSize = true;
            this.lblLoaiSP.Location = new System.Drawing.Point(22, 133);
            this.lblLoaiSP.Name = "lblLoaiSP";
            this.lblLoaiSP.Size = new System.Drawing.Size(108, 19);
            this.lblLoaiSP.TabIndex = 11;
            this.lblLoaiSP.Text = "Loại sản phẩm *";
            // 
            // lblTenSP
            // 
            this.lblTenSP.AutoSize = true;
            this.lblTenSP.Location = new System.Drawing.Point(22, 87);
            this.lblTenSP.Name = "lblTenSP";
            this.lblTenSP.Size = new System.Drawing.Size(104, 19);
            this.lblTenSP.TabIndex = 10;
            this.lblTenSP.Text = "Tên sản phẩm *";
            // 
            // lblMaSP
            // 
            this.lblMaSP.AutoSize = true;
            this.lblMaSP.Location = new System.Drawing.Point(22, 41);
            this.lblMaSP.Name = "lblMaSP";
            this.lblMaSP.Size = new System.Drawing.Size(103, 19);
            this.lblMaSP.TabIndex = 9;
            this.lblMaSP.Text = "Mã sản phẩm *";
            // 
            // btnBoAnh
            // 
            this.btnBoAnh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnBoAnh.Location = new System.Drawing.Point(998, 175);
            this.btnBoAnh.Name = "btnBoAnh";
            this.btnBoAnh.Size = new System.Drawing.Size(90, 32);
            this.btnBoAnh.TabIndex = 8;
            this.btnBoAnh.Text = "Bỏ ảnh";
            this.btnBoAnh.UseVisualStyleBackColor = true;
            this.btnBoAnh.Click += new System.EventHandler(this.RemoveImage);
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnChonAnh.Location = new System.Drawing.Point(898, 175);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(90, 32);
            this.btnChonAnh.TabIndex = 7;
            this.btnChonAnh.Text = "Chọn ảnh";
            this.btnChonAnh.UseVisualStyleBackColor = true;
            this.btnChonAnh.Click += new System.EventHandler(this.ChooseImage);
            // 
            // chkConKinhDoanh
            // 
            this.chkConKinhDoanh.AutoSize = true;
            this.chkConKinhDoanh.Checked = true;
            this.chkConKinhDoanh.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkConKinhDoanh.Location = new System.Drawing.Point(148, 175);
            this.chkConKinhDoanh.Name = "chkConKinhDoanh";
            this.chkConKinhDoanh.Size = new System.Drawing.Size(126, 23);
            this.chkConKinhDoanh.TabIndex = 6;
            this.chkConKinhDoanh.Text = "Còn kinh doanh";
            this.chkConKinhDoanh.UseVisualStyleBackColor = true;
            // 
            // dtpNgayNhap
            // 
            this.dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            this.dtpNgayNhap.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgayNhap.Location = new System.Drawing.Point(520, 129);
            this.dtpNgayNhap.Name = "dtpNgayNhap";
            this.dtpNgayNhap.Size = new System.Drawing.Size(240, 25);
            this.dtpNgayNhap.TabIndex = 5;
            // 
            // numSoLuong
            // 
            this.numSoLuong.Location = new System.Drawing.Point(520, 83);
            this.numSoLuong.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numSoLuong.Name = "numSoLuong";
            this.numSoLuong.Size = new System.Drawing.Size(240, 25);
            this.numSoLuong.TabIndex = 4;
            this.numSoLuong.ThousandsSeparator = true;
            // 
            // numDonGia
            // 
            this.numDonGia.DecimalPlaces = 2;
            this.numDonGia.Location = new System.Drawing.Point(520, 37);
            this.numDonGia.Maximum = new decimal(new int[] {
            -727379968,
            232,
            0,
            0});
            this.numDonGia.Name = "numDonGia";
            this.numDonGia.Size = new System.Drawing.Size(240, 25);
            this.numDonGia.TabIndex = 3;
            this.numDonGia.ThousandsSeparator = true;
            // 
            // cboLoaiSP
            // 
            this.cboLoaiSP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiSP.FormattingEnabled = true;
            this.cboLoaiSP.Location = new System.Drawing.Point(148, 129);
            this.cboLoaiSP.Name = "cboLoaiSP";
            this.cboLoaiSP.Size = new System.Drawing.Size(230, 25);
            this.cboLoaiSP.TabIndex = 2;
            // 
            // txtTenSP
            // 
            this.txtTenSP.Location = new System.Drawing.Point(148, 83);
            this.txtTenSP.MaxLength = 200;
            this.txtTenSP.Name = "txtTenSP";
            this.txtTenSP.Size = new System.Drawing.Size(230, 25);
            this.txtTenSP.TabIndex = 1;
            // 
            // txtMaSP
            // 
            this.txtMaSP.Location = new System.Drawing.Point(148, 37);
            this.txtMaSP.MaxLength = 50;
            this.txtMaSP.Name = "txtMaSP";
            this.txtMaSP.Size = new System.Drawing.Size(230, 25);
            this.txtMaSP.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(97, 386);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(235, 25);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.KeyDown += new System.Windows.Forms.KeyEventHandler(this.SearchKeyDown);
            // 
            // cboSapXep
            // 
            this.cboSapXep.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSapXep.FormattingEnabled = true;
            this.cboSapXep.Items.AddRange(new object[] {
            "Thứ tự ban đầu",
            "Đơn giá tăng dần",
            "Đơn giá giảm dần",
            "Số lượng tăng dần",
            "Số lượng giảm dần"});
            this.cboSapXep.Location = new System.Drawing.Point(555, 386);
            this.cboSapXep.Name = "cboSapXep";
            this.cboSapXep.Size = new System.Drawing.Size(215, 25);
            this.cboSapXep.TabIndex = 3;
            this.cboSapXep.SelectedIndexChanged += new System.EventHandler(this.SortChanged);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(349, 386);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(95, 29);
            this.btnTimKiem.TabIndex = 2;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.SearchProducts);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(22, 390);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(65, 19);
            this.lblTimKiem.TabIndex = 4;
            this.lblTimKiem.Text = "Mã / tên:";
            // 
            // lblSapXep
            // 
            this.lblSapXep.AutoSize = true;
            this.lblSapXep.Location = new System.Drawing.Point(471, 390);
            this.lblSapXep.Name = "lblSapXep";
            this.lblSapXep.Size = new System.Drawing.Size(59, 19);
            this.lblSapXep.TabIndex = 5;
            this.lblSapXep.Text = "Sắp xếp:";
            // 
            // dgvProducts
            // 
            this.dgvProducts.AllowUserToAddRows = false;
            this.dgvProducts.AllowUserToDeleteRows = false;
            this.dgvProducts.AllowUserToResizeRows = false;
            dataGridViewCellStyle7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(253)))));
            this.dgvProducts.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle7;
            this.dgvProducts.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvProducts.AutoGenerateColumns = false;
            this.dgvProducts.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProducts.BackgroundColor = System.Drawing.Color.White;
            this.dgvProducts.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProducts.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleHorizontal;
            this.dgvProducts.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle8.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            dataGridViewCellStyle8.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle8.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            dataGridViewCellStyle8.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(238)))), ((int)(((byte)(247)))));
            this.dgvProducts.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle8;
            this.dgvProducts.ColumnHeadersHeight = 40;
            this.dgvProducts.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvProducts.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colMaSP,
            this.colTenSP,
            this.colLoaiSP,
            this.colDonGia,
            this.colSoLuong,
            this.colNgayNhap,
            this.colConKinhDoanh,
            this.colDuongDanAnh});
            this.dgvProducts.ContextMenuStrip = this.contextProducts;
            this.dgvProducts.DataSource = this.productSource;
            dataGridViewCellStyle12.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle12.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle12.Font = new System.Drawing.Font("Segoe UI", 10F);
            dataGridViewCellStyle12.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            dataGridViewCellStyle12.Padding = new System.Windows.Forms.Padding(6, 0, 6, 0);
            dataGridViewCellStyle12.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(219)))), ((int)(((byte)(234)))), ((int)(((byte)(254)))));
            dataGridViewCellStyle12.SelectionForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(58)))), ((int)(((byte)(138)))));
            dataGridViewCellStyle12.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvProducts.DefaultCellStyle = dataGridViewCellStyle12;
            this.dgvProducts.EnableHeadersVisualStyles = false;
            this.dgvProducts.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(228)))), ((int)(((byte)(234)))), ((int)(((byte)(242)))));
            this.dgvProducts.Location = new System.Drawing.Point(19, 425);
            this.dgvProducts.MultiSelect = false;
            this.dgvProducts.Name = "dgvProducts";
            this.dgvProducts.ReadOnly = true;
            this.dgvProducts.RowHeadersVisible = false;
            this.dgvProducts.RowTemplate.Height = 36;
            this.dgvProducts.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvProducts.Size = new System.Drawing.Size(1122, 289);
            this.dgvProducts.TabIndex = 4;
            this.dgvProducts.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.GridCellDoubleClick);
            this.dgvProducts.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.GridCellMouseDown);
            this.dgvProducts.SelectionChanged += new System.EventHandler(this.GridSelectionChanged);
            // 
            // contextProducts
            // 
            this.contextProducts.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxEdit,
            this.ctxDelete,
            this.ctxDetails});
            this.contextProducts.Name = "contextProducts";
            this.contextProducts.Size = new System.Drawing.Size(138, 70);
            this.contextProducts.Opening += new System.ComponentModel.CancelEventHandler(this.ContextProductsOpening);
            // 
            // ctxEdit
            // 
            this.ctxEdit.Name = "ctxEdit";
            this.ctxEdit.Size = new System.Drawing.Size(137, 22);
            this.ctxEdit.Text = "Sửa";
            this.ctxEdit.Click += new System.EventHandler(this.BeginEditSelected);
            // 
            // ctxDelete
            // 
            this.ctxDelete.Name = "ctxDelete";
            this.ctxDelete.Size = new System.Drawing.Size(137, 22);
            this.ctxDelete.Text = "Xóa";
            this.ctxDelete.Click += new System.EventHandler(this.DeleteProduct);
            // 
            // ctxDetails
            // 
            this.ctxDetails.Name = "ctxDetails";
            this.ctxDetails.Size = new System.Drawing.Size(137, 22);
            this.ctxDetails.Text = "Xem chi tiết";
            this.ctxDetails.Click += new System.EventHandler(this.ShowDetails);
            // 
            // errorProvider
            // 
            this.errorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.errorProvider.ContainerControl = this;
            // 
            // menuMain
            // 
            this.menuMain.BackColor = System.Drawing.Color.White;
            this.menuMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuManage,
            this.mnuHelp});
            this.menuMain.Location = new System.Drawing.Point(0, 0);
            this.menuMain.Name = "menuMain";
            this.menuMain.Padding = new System.Windows.Forms.Padding(12, 4, 0, 4);
            this.menuMain.Size = new System.Drawing.Size(1160, 27);
            this.menuMain.TabIndex = 8;
            this.menuMain.Text = "menuMain";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuImport,
            this.mnuExport,
            this.mnuExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 19);
            this.mnuFile.Text = "&File";
            // 
            // mnuImport
            // 
            this.mnuImport.Name = "mnuImport";
            this.mnuImport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuImport.Size = new System.Drawing.Size(276, 22);
            this.mnuImport.Text = "Nhập sản phẩm từ Excel (.xlsx)";
            this.mnuImport.Click += new System.EventHandler(this.ImportProductsExcel);
            // 
            // mnuExport
            // 
            this.mnuExport.Name = "mnuExport";
            this.mnuExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.mnuExport.Size = new System.Drawing.Size(276, 22);
            this.mnuExport.Text = "Xuất danh sách TXT";
            this.mnuExport.Click += new System.EventHandler(this.ExportProducts);
            // 
            // mnuExit
            // 
            this.mnuExit.Name = "mnuExit";
            this.mnuExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.mnuExit.Size = new System.Drawing.Size(276, 22);
            this.mnuExit.Text = "Thoát";
            this.mnuExit.Click += new System.EventHandler(this.ExitApplication);
            // 
            // mnuManage
            // 
            this.mnuManage.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuAdd,
            this.mnuEdit,
            this.mnuDelete,
            this.mnuReset,
            this.mnuSearch});
            this.mnuManage.Name = "mnuManage";
            this.mnuManage.Size = new System.Drawing.Size(60, 19);
            this.mnuManage.Text = "&Quản lý";
            // 
            // mnuAdd
            // 
            this.mnuAdd.Name = "mnuAdd";
            this.mnuAdd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuAdd.Size = new System.Drawing.Size(210, 22);
            this.mnuAdd.Text = "Thêm";
            this.mnuAdd.Click += new System.EventHandler(this.AddProduct);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuEdit.Size = new System.Drawing.Size(210, 22);
            this.mnuEdit.Text = "Sửa / Lưu thay đổi";
            this.mnuEdit.Click += new System.EventHandler(this.EditProduct);
            // 
            // mnuDelete
            // 
            this.mnuDelete.Name = "mnuDelete";
            this.mnuDelete.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.mnuDelete.Size = new System.Drawing.Size(210, 22);
            this.mnuDelete.Text = "Xóa";
            this.mnuDelete.Click += new System.EventHandler(this.DeleteProduct);
            // 
            // mnuReset
            // 
            this.mnuReset.Name = "mnuReset";
            this.mnuReset.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.mnuReset.Size = new System.Drawing.Size(210, 22);
            this.mnuReset.Text = "Làm mới";
            this.mnuReset.Click += new System.EventHandler(this.ResetAll);
            // 
            // mnuSearch
            // 
            this.mnuSearch.Name = "mnuSearch";
            this.mnuSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuSearch.Size = new System.Drawing.Size(210, 22);
            this.mnuSearch.Text = "Tìm kiếm";
            this.mnuSearch.Click += new System.EventHandler(this.FocusSearch);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuGuide});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(63, 19);
            this.mnuHelp.Text = "&Trợ giúp";
            // 
            // mnuGuide
            // 
            this.mnuGuide.Name = "mnuGuide";
            this.mnuGuide.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.mnuGuide.Size = new System.Drawing.Size(153, 22);
            this.mnuGuide.Text = "Hướng dẫn";
            this.mnuGuide.Click += new System.EventHandler(this.ShowHelp);
            // 
            // statusMain
            // 
            this.statusMain.BackColor = System.Drawing.Color.White;
            this.statusMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblStatus,
            this.lblCount,
            this.lblTotal});
            this.statusMain.Location = new System.Drawing.Point(0, 722);
            this.statusMain.Name = "statusMain";
            this.statusMain.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.statusMain.Size = new System.Drawing.Size(1160, 28);
            this.statusMain.TabIndex = 9;
            this.statusMain.Text = "statusMain";
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(919, 15);
            this.lblStatus.Spring = true;
            this.lblStatus.Text = "Sẵn sàng";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCount
            // 
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(112, 15);
            this.lblCount.Text = "Hiển thị: 0 / Tổng: 0";
            // 
            // lblTotal
            // 
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(105, 15);
            this.lblTotal.Text = "Tồn kho: 0,00 VNĐ";
            // 
            // tsActions
            // 
            this.tsActions.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tsActions.AutoSize = false;
            this.tsActions.BackColor = System.Drawing.Color.White;
            this.tsActions.CanOverflow = false;
            this.tsActions.Dock = System.Windows.Forms.DockStyle.None;
            this.tsActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbAdd,
            this.tsbEdit,
            this.tsbDelete,
            this.tsbReset,
            this.tsbSearch});
            this.tsActions.Location = new System.Drawing.Point(19, 322);
            this.tsActions.Name = "tsActions";
            this.tsActions.Padding = new System.Windows.Forms.Padding(12, 6, 12, 6);
            this.tsActions.Size = new System.Drawing.Size(1122, 48);
            this.tsActions.TabIndex = 1;
            this.tsActions.Text = "tsActions";
            // 
            // tsbAdd
            // 
            this.tsbAdd.AutoSize = false;
            this.tsbAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.tsbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbAdd.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsbAdd.ForeColor = System.Drawing.Color.White;
            this.tsbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbAdd.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbAdd.Name = "tsbAdd";
            this.tsbAdd.Size = new System.Drawing.Size(112, 36);
            this.tsbAdd.Text = "Thêm";
            this.tsbAdd.ToolTipText = "Thêm — Ctrl+N";
            this.tsbAdd.Click += new System.EventHandler(this.AddProduct);
            // 
            // tsbEdit
            // 
            this.tsbEdit.AutoSize = false;
            this.tsbEdit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.tsbEdit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbEdit.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsbEdit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(78)))), ((int)(((byte)(216)))));
            this.tsbEdit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbEdit.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbEdit.Name = "tsbEdit";
            this.tsbEdit.Size = new System.Drawing.Size(112, 36);
            this.tsbEdit.Text = "Sửa";
            this.tsbEdit.ToolTipText = "Sửa — Ctrl+S";
            this.tsbEdit.Click += new System.EventHandler(this.EditProduct);
            // 
            // tsbDelete
            // 
            this.tsbDelete.AutoSize = false;
            this.tsbDelete.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(235)))), ((int)(((byte)(238)))));
            this.tsbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbDelete.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsbDelete.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(185)))), ((int)(((byte)(28)))), ((int)(((byte)(28)))));
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(112, 36);
            this.tsbDelete.Text = "Xóa";
            this.tsbDelete.ToolTipText = "Xóa — Ctrl+Delete";
            this.tsbDelete.Click += new System.EventHandler(this.DeleteProduct);
            // 
            // tsbReset
            // 
            this.tsbReset.AutoSize = false;
            this.tsbReset.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.tsbReset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbReset.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsbReset.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.tsbReset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbReset.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbReset.Name = "tsbReset";
            this.tsbReset.Size = new System.Drawing.Size(112, 36);
            this.tsbReset.Text = "Làm mới";
            this.tsbReset.ToolTipText = "Làm mới — F5";
            this.tsbReset.Click += new System.EventHandler(this.ResetAll);
            // 
            // tsbSearch
            // 
            this.tsbSearch.AutoSize = false;
            this.tsbSearch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(237)))), ((int)(((byte)(241)))), ((int)(((byte)(246)))));
            this.tsbSearch.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.tsbSearch.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.tsbSearch.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.tsbSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbSearch.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.tsbSearch.Name = "tsbSearch";
            this.tsbSearch.Size = new System.Drawing.Size(112, 36);
            this.tsbSearch.Text = "Tìm kiếm";
            this.tsbSearch.ToolTipText = "Tìm kiếm — Nhập mã / tên trong ô bên dưới";
            this.tsbSearch.Click += new System.EventHandler(this.SearchProducts);
            // 
            // colMaSP
            // 
            this.colMaSP.DataPropertyName = "MaSP";
            this.colMaSP.FillWeight = 70F;
            this.colMaSP.HeaderText = "Mã SP";
            this.colMaSP.Name = "colMaSP";
            this.colMaSP.ReadOnly = true;
            this.colMaSP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colTenSP
            // 
            this.colTenSP.DataPropertyName = "TenSP";
            this.colTenSP.FillWeight = 220F;
            this.colTenSP.HeaderText = "Tên sản phẩm";
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.ReadOnly = true;
            this.colTenSP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colLoaiSP
            // 
            this.colLoaiSP.DataPropertyName = "LoaiSP";
            this.colLoaiSP.FillWeight = 90F;
            this.colLoaiSP.HeaderText = "Loại";
            this.colLoaiSP.Name = "colLoaiSP";
            this.colLoaiSP.ReadOnly = true;
            this.colLoaiSP.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colDonGia
            // 
            this.colDonGia.DataPropertyName = "DonGia";
            dataGridViewCellStyle9.Format = "N2";
            this.colDonGia.DefaultCellStyle = dataGridViewCellStyle9;
            this.colDonGia.FillWeight = 110F;
            this.colDonGia.HeaderText = "Đơn giá (VNĐ)";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.ReadOnly = true;
            this.colDonGia.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colSoLuong
            // 
            this.colSoLuong.DataPropertyName = "SoLuong";
            dataGridViewCellStyle10.Format = "N0";
            this.colSoLuong.DefaultCellStyle = dataGridViewCellStyle10;
            this.colSoLuong.FillWeight = 65F;
            this.colSoLuong.HeaderText = "Số lượng";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.ReadOnly = true;
            this.colSoLuong.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colNgayNhap
            // 
            this.colNgayNhap.DataPropertyName = "NgayNhap";
            dataGridViewCellStyle11.Format = "dd/MM/yyyy";
            this.colNgayNhap.DefaultCellStyle = dataGridViewCellStyle11;
            this.colNgayNhap.FillWeight = 90F;
            this.colNgayNhap.HeaderText = "Ngày nhập";
            this.colNgayNhap.Name = "colNgayNhap";
            this.colNgayNhap.ReadOnly = true;
            this.colNgayNhap.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // colConKinhDoanh
            // 
            this.colConKinhDoanh.DataPropertyName = "ConKinhDoanh";
            this.colConKinhDoanh.FillWeight = 60F;
            this.colConKinhDoanh.HeaderText = "Còn KD";
            this.colConKinhDoanh.Name = "colConKinhDoanh";
            this.colConKinhDoanh.ReadOnly = true;
            // 
            // colDuongDanAnh
            // 
            this.colDuongDanAnh.DataPropertyName = "DuongDanAnh";
            this.colDuongDanAnh.FillWeight = 130F;
            this.colDuongDanAnh.HeaderText = "Đường dẫn ảnh";
            this.colDuongDanAnh.Name = "colDuongDanAnh";
            this.colDuongDanAnh.ReadOnly = true;
            this.colDuongDanAnh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // productSource
            // 
            this.productSource.DataSource = typeof(ProductManager.Product);
            // 
            // FrmProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(250)))));
            this.ClientSize = new System.Drawing.Size(1160, 750);
            this.Controls.Add(this.tsActions);
            this.Controls.Add(this.statusMain);
            this.Controls.Add(this.menuMain);
            this.Controls.Add(this.dgvProducts);
            this.Controls.Add(this.lblSapXep);
            this.Controls.Add(this.lblTimKiem);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.cboSapXep);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.grpProduct);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(48)))), ((int)(((byte)(65)))));
            this.MainMenuStrip = this.menuMain;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1050, 730);
            this.Name = "FrmProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý sản phẩm";
            this.Load += new System.EventHandler(this.FormLoaded);
            this.Disposed += new System.EventHandler(this.DisposePreviewImage);
            this.grpProduct.ResumeLayout(false);
            this.grpProduct.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picProduct)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numDonGia)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProducts)).EndInit();
            this.contextProducts.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.menuMain.ResumeLayout(false);
            this.menuMain.PerformLayout();
            this.statusMain.ResumeLayout(false);
            this.statusMain.PerformLayout();
            this.tsActions.ResumeLayout(false);
            this.tsActions.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.productSource)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem mnuFile;
        private System.Windows.Forms.ToolStripMenuItem mnuImport;
        private System.Windows.Forms.ToolStripMenuItem mnuExport;
        private System.Windows.Forms.ToolStripMenuItem mnuExit;
        private System.Windows.Forms.ToolStripMenuItem mnuManage;
        private System.Windows.Forms.ToolStripMenuItem mnuAdd;
        private System.Windows.Forms.ToolStripMenuItem mnuEdit;
        private System.Windows.Forms.ToolStripMenuItem mnuDelete;
        private System.Windows.Forms.ToolStripMenuItem mnuReset;
        private System.Windows.Forms.ToolStripMenuItem mnuSearch;
        private System.Windows.Forms.ToolStripMenuItem mnuHelp;
        private System.Windows.Forms.ToolStripMenuItem mnuGuide;
        private System.Windows.Forms.ToolStripMenuItem ctxEdit;
        private System.Windows.Forms.ToolStripMenuItem ctxDelete;
        private System.Windows.Forms.ToolStripMenuItem ctxDetails;


        private System.Windows.Forms.GroupBox grpProduct;
        private System.Windows.Forms.TextBox txtTenSP;
        private System.Windows.Forms.TextBox txtMaSP;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.NumericUpDown numSoLuong;
        private System.Windows.Forms.NumericUpDown numDonGia;
        private System.Windows.Forms.ComboBox cboLoaiSP;
        private System.Windows.Forms.ComboBox cboSapXep;
        private System.Windows.Forms.Label lblSoLuong;
        private System.Windows.Forms.Label lblDonGia;
        private System.Windows.Forms.Label lblLoaiSP;
        private System.Windows.Forms.Label lblTenSP;
        private System.Windows.Forms.Label lblMaSP;
        private System.Windows.Forms.Button btnBoAnh;
        private System.Windows.Forms.Button btnChonAnh;
        private System.Windows.Forms.CheckBox chkConKinhDoanh;
        private System.Windows.Forms.DateTimePicker dtpNgayNhap;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.PictureBox picProduct;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.Label lblNgayNhap;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Label lblSapXep;
        private System.Windows.Forms.DataGridView dgvProducts;
        private System.Windows.Forms.BindingSource productSource;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.StatusStrip statusMain;
        private System.Windows.Forms.MenuStrip menuMain;
        private System.Windows.Forms.ContextMenuStrip contextProducts;
        private System.Windows.Forms.ToolStrip tsActions;
        private System.Windows.Forms.ToolStripButton tsbAdd;
        private System.Windows.Forms.ToolStripButton tsbEdit;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripButton tsbReset;
        private System.Windows.Forms.ToolStripButton tsbSearch;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblCount;
        private System.Windows.Forms.ToolStripStatusLabel lblTotal;
        private System.Windows.Forms.DataGridViewTextBoxColumn colMaSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTenSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colLoaiSP;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn colNgayNhap;
        private System.Windows.Forms.DataGridViewCheckBoxColumn colConKinhDoanh;
        private System.Windows.Forms.DataGridViewTextBoxColumn colDuongDanAnh;
    }
}