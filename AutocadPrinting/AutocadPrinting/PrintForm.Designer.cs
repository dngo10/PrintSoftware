namespace AutocadPrinting
{
    partial class PrintForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintForm));
            this.PlotStyleLabel = new System.Windows.Forms.Label();
            this.PlotStylesComboBox = new System.Windows.Forms.ComboBox();
            this.outPutLocationLabel = new System.Windows.Forms.Label();
            this.outPutLocationTextBox = new System.Windows.Forms.TextBox();
            this.LocationButton = new System.Windows.Forms.Button();
            this.fileTypeLabel = new System.Windows.Forms.Label();
            this.fileTypeComboBox = new System.Windows.Forms.ComboBox();
            this.AddButton = new System.Windows.Forms.Button();
            this.minusButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.DownButton = new System.Windows.Forms.Button();
            this.dwgGridView = new System.Windows.Forms.DataGridView();
            this.dwgName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SheetFile = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PageSize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Status = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.publishButton = new System.Windows.Forms.Button();
            this.messageTextBox = new System.Windows.Forms.RichTextBox();
            this.messageLabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.cadVersionsComboBox = new System.Windows.Forms.ComboBox();
            this.accoreconsoleBrowserButton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.accoreconsoleLocationTextBox = new System.Windows.Forms.TextBox();
            this.orientationLabel = new System.Windows.Forms.Label();
            this.orientationComboBox = new System.Windows.Forms.ComboBox();
            this.showConsoleCheckBox = new System.Windows.Forms.CheckBox();
            this.showConsoleLabel = new System.Windows.Forms.Label();
            this.StyleLabel = new System.Windows.Forms.Label();
            this.styleComboBox = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.dwgGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // PlotStyleLabel
            // 
            this.PlotStyleLabel.AutoSize = true;
            this.PlotStyleLabel.Location = new System.Drawing.Point(15, 6);
            this.PlotStyleLabel.Name = "PlotStyleLabel";
            this.PlotStyleLabel.Size = new System.Drawing.Size(82, 13);
            this.PlotStyleLabel.TabIndex = 4;
            this.PlotStyleLabel.Text = "PLOT STYLES:";
            // 
            // PlotStylesComboBox
            // 
            this.PlotStylesComboBox.FormattingEnabled = true;
            this.PlotStylesComboBox.Location = new System.Drawing.Point(103, 2);
            this.PlotStylesComboBox.Name = "PlotStylesComboBox";
            this.PlotStylesComboBox.Size = new System.Drawing.Size(273, 21);
            this.PlotStylesComboBox.TabIndex = 5;
            // 
            // outPutLocationLabel
            // 
            this.outPutLocationLabel.AutoSize = true;
            this.outPutLocationLabel.Location = new System.Drawing.Point(42, 35);
            this.outPutLocationLabel.Name = "outPutLocationLabel";
            this.outPutLocationLabel.Size = new System.Drawing.Size(55, 13);
            this.outPutLocationLabel.TabIndex = 6;
            this.outPutLocationLabel.Text = "OUTPUT:";
            // 
            // outPutLocationTextBox
            // 
            this.outPutLocationTextBox.Location = new System.Drawing.Point(103, 32);
            this.outPutLocationTextBox.Name = "outPutLocationTextBox";
            this.outPutLocationTextBox.Size = new System.Drawing.Size(273, 20);
            this.outPutLocationTextBox.TabIndex = 7;
            // 
            // LocationButton
            // 
            this.LocationButton.Location = new System.Drawing.Point(382, 31);
            this.LocationButton.Name = "LocationButton";
            this.LocationButton.Size = new System.Drawing.Size(28, 23);
            this.LocationButton.TabIndex = 8;
            this.LocationButton.Text = "...";
            this.LocationButton.UseVisualStyleBackColor = true;
            this.LocationButton.Click += new System.EventHandler(this.LocationButton_Click);
            // 
            // fileTypeLabel
            // 
            this.fileTypeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypeLabel.AutoSize = true;
            this.fileTypeLabel.Location = new System.Drawing.Point(503, 7);
            this.fileTypeLabel.Name = "fileTypeLabel";
            this.fileTypeLabel.Size = new System.Drawing.Size(63, 13);
            this.fileTypeLabel.TabIndex = 9;
            this.fileTypeLabel.Text = "FILE TYPE:";
            // 
            // fileTypeComboBox
            // 
            this.fileTypeComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.fileTypeComboBox.FormattingEnabled = true;
            this.fileTypeComboBox.Items.AddRange(new object[] {
            "Multiple-Sheet file",
            "Single-Sheet files"});
            this.fileTypeComboBox.Location = new System.Drawing.Point(572, 4);
            this.fileTypeComboBox.Name = "fileTypeComboBox";
            this.fileTypeComboBox.Size = new System.Drawing.Size(114, 21);
            this.fileTypeComboBox.TabIndex = 10;
            this.fileTypeComboBox.SelectedIndexChanged += new System.EventHandler(this.fileTypeComboBox_SelectedIndexChanged);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(12, 80);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(25, 25);
            this.AddButton.TabIndex = 11;
            this.AddButton.Text = "+";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // minusButton
            // 
            this.minusButton.Location = new System.Drawing.Point(43, 80);
            this.minusButton.Name = "minusButton";
            this.minusButton.Size = new System.Drawing.Size(25, 25);
            this.minusButton.TabIndex = 12;
            this.minusButton.Text = "-";
            this.minusButton.UseVisualStyleBackColor = true;
            this.minusButton.Click += new System.EventHandler(this.minusButton_Click);
            // 
            // upButton
            // 
            this.upButton.Location = new System.Drawing.Point(92, 79);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(25, 25);
            this.upButton.TabIndex = 13;
            this.upButton.Text = "↑";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // DownButton
            // 
            this.DownButton.Location = new System.Drawing.Point(124, 79);
            this.DownButton.Name = "DownButton";
            this.DownButton.Size = new System.Drawing.Size(25, 25);
            this.DownButton.TabIndex = 14;
            this.DownButton.Text = "↓";
            this.DownButton.UseVisualStyleBackColor = true;
            this.DownButton.Click += new System.EventHandler(this.DownButton_Click);
            // 
            // dwgGridView
            // 
            this.dwgGridView.AllowDrop = true;
            this.dwgGridView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dwgGridView.BackgroundColor = System.Drawing.Color.White;
            this.dwgGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dwgGridView.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dwgName,
            this.SheetFile,
            this.PageSize,
            this.Status});
            this.dwgGridView.GridColor = System.Drawing.Color.SandyBrown;
            this.dwgGridView.Location = new System.Drawing.Point(10, 109);
            this.dwgGridView.Name = "dwgGridView";
            this.dwgGridView.ReadOnly = true;
            this.dwgGridView.RowHeadersVisible = false;
            this.dwgGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dwgGridView.Size = new System.Drawing.Size(676, 179);
            this.dwgGridView.TabIndex = 15;
            this.dwgGridView.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dwgGridView_CellContentClick);
            this.dwgGridView.DragDrop += new System.Windows.Forms.DragEventHandler(this.dwgGridView_DragDrop);
            this.dwgGridView.DragEnter += new System.Windows.Forms.DragEventHandler(this.dwgGridView_DragEnter);
            this.dwgGridView.DoubleClick += new System.EventHandler(this.dwgGridView_DoubleClick);
            this.dwgGridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dwgGridView_KeyDown);
            // 
            // dwgName
            // 
            this.dwgName.HeaderText = "DWG";
            this.dwgName.Name = "dwgName";
            this.dwgName.ReadOnly = true;
            this.dwgName.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.dwgName.Width = 200;
            // 
            // SheetFile
            // 
            this.SheetFile.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.SheetFile.HeaderText = "Sheet Name";
            this.SheetFile.Name = "SheetFile";
            this.SheetFile.ReadOnly = true;
            this.SheetFile.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // PageSize
            // 
            this.PageSize.HeaderText = "PageSize";
            this.PageSize.Name = "PageSize";
            this.PageSize.ReadOnly = true;
            this.PageSize.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.PageSize.Width = 65;
            // 
            // Status
            // 
            this.Status.HeaderText = "Status";
            this.Status.Name = "Status";
            this.Status.ReadOnly = true;
            this.Status.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // publishButton
            // 
            this.publishButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.publishButton.BackColor = System.Drawing.Color.GreenYellow;
            this.publishButton.Location = new System.Drawing.Point(582, 382);
            this.publishButton.Name = "publishButton";
            this.publishButton.Size = new System.Drawing.Size(104, 23);
            this.publishButton.TabIndex = 16;
            this.publishButton.Text = "PUBLISH";
            this.publishButton.UseVisualStyleBackColor = false;
            this.publishButton.Click += new System.EventHandler(this.publishButton_Click);
            // 
            // messageTextBox
            // 
            this.messageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.messageTextBox.Location = new System.Drawing.Point(10, 308);
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.ReadOnly = true;
            this.messageTextBox.Size = new System.Drawing.Size(676, 68);
            this.messageTextBox.TabIndex = 17;
            this.messageTextBox.Text = "";
            // 
            // messageLabel
            // 
            this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.messageLabel.AutoSize = true;
            this.messageLabel.Location = new System.Drawing.Point(12, 291);
            this.messageLabel.Name = "messageLabel";
            this.messageLabel.Size = new System.Drawing.Size(53, 13);
            this.messageLabel.TabIndex = 18;
            this.messageLabel.Text = "Message:";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(483, 32);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 13);
            this.label7.TabIndex = 20;
            this.label7.Text = "CAD VERSION:";
            // 
            // cadVersionsComboBox
            // 
            this.cadVersionsComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cadVersionsComboBox.FormattingEnabled = true;
            this.cadVersionsComboBox.Location = new System.Drawing.Point(572, 29);
            this.cadVersionsComboBox.Name = "cadVersionsComboBox";
            this.cadVersionsComboBox.Size = new System.Drawing.Size(114, 21);
            this.cadVersionsComboBox.TabIndex = 21;
            this.cadVersionsComboBox.SelectedIndexChanged += new System.EventHandler(this.cadVersionsComboBox_SelectedIndexChanged);
            // 
            // accoreconsoleBrowserButton
            // 
            this.accoreconsoleBrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accoreconsoleBrowserButton.Location = new System.Drawing.Point(656, 413);
            this.accoreconsoleBrowserButton.Name = "accoreconsoleBrowserButton";
            this.accoreconsoleBrowserButton.Size = new System.Drawing.Size(30, 23);
            this.accoreconsoleBrowserButton.TabIndex = 23;
            this.accoreconsoleBrowserButton.Text = "...";
            this.accoreconsoleBrowserButton.UseVisualStyleBackColor = true;
            this.accoreconsoleBrowserButton.Click += new System.EventHandler(this.accoreconsoleBrowserButton_Click);
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(275, 418);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(105, 13);
            this.label5.TabIndex = 24;
            this.label5.Text = "ACCORECONSOLE:";
            // 
            // accoreconsoleLocationTextBox
            // 
            this.accoreconsoleLocationTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.accoreconsoleLocationTextBox.Location = new System.Drawing.Point(386, 415);
            this.accoreconsoleLocationTextBox.Name = "accoreconsoleLocationTextBox";
            this.accoreconsoleLocationTextBox.ReadOnly = true;
            this.accoreconsoleLocationTextBox.Size = new System.Drawing.Size(259, 20);
            this.accoreconsoleLocationTextBox.TabIndex = 22;
            // 
            // orientationLabel
            // 
            this.orientationLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.orientationLabel.AutoSize = true;
            this.orientationLabel.Location = new System.Drawing.Point(483, 59);
            this.orientationLabel.Name = "orientationLabel";
            this.orientationLabel.Size = new System.Drawing.Size(84, 13);
            this.orientationLabel.TabIndex = 25;
            this.orientationLabel.Text = "ORIENTATION:";
            // 
            // orientationComboBox
            // 
            this.orientationComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.orientationComboBox.FormattingEnabled = true;
            this.orientationComboBox.Location = new System.Drawing.Point(572, 55);
            this.orientationComboBox.Name = "orientationComboBox";
            this.orientationComboBox.Size = new System.Drawing.Size(114, 21);
            this.orientationComboBox.TabIndex = 26;
            // 
            // showConsoleCheckBox
            // 
            this.showConsoleCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showConsoleCheckBox.AutoSize = true;
            this.showConsoleCheckBox.Location = new System.Drawing.Point(10, 403);
            this.showConsoleCheckBox.Name = "showConsoleCheckBox";
            this.showConsoleCheckBox.Size = new System.Drawing.Size(94, 17);
            this.showConsoleCheckBox.TabIndex = 29;
            this.showConsoleCheckBox.Text = "Show Console";
            this.showConsoleCheckBox.UseVisualStyleBackColor = true;
            // 
            // showConsoleLabel
            // 
            this.showConsoleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.showConsoleLabel.AutoSize = true;
            this.showConsoleLabel.ForeColor = System.Drawing.Color.Red;
            this.showConsoleLabel.Location = new System.Drawing.Point(7, 387);
            this.showConsoleLabel.Name = "showConsoleLabel";
            this.showConsoleLabel.Size = new System.Drawing.Size(318, 13);
            this.showConsoleLabel.TabIndex = 30;
            this.showConsoleLabel.Text = "Check this box when printing huge files, mainly to check for errors.";
            // 
            // StyleLabel
            // 
            this.StyleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.StyleLabel.AutoSize = true;
            this.StyleLabel.Location = new System.Drawing.Point(446, 85);
            this.StyleLabel.Name = "StyleLabel";
            this.StyleLabel.Size = new System.Drawing.Size(54, 13);
            this.StyleLabel.TabIndex = 31;
            this.StyleLabel.Text = "STYLES :";
            // 
            // styleComboBox
            // 
            this.styleComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.styleComboBox.FormattingEnabled = true;
            this.styleComboBox.Location = new System.Drawing.Point(506, 82);
            this.styleComboBox.Name = "styleComboBox";
            this.styleComboBox.Size = new System.Drawing.Size(180, 21);
            this.styleComboBox.TabIndex = 32;
            // 
            // PrintForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(697, 449);
            this.Controls.Add(this.styleComboBox);
            this.Controls.Add(this.StyleLabel);
            this.Controls.Add(this.showConsoleLabel);
            this.Controls.Add(this.showConsoleCheckBox);
            this.Controls.Add(this.orientationComboBox);
            this.Controls.Add(this.orientationLabel);
            this.Controls.Add(this.accoreconsoleBrowserButton);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.accoreconsoleLocationTextBox);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.cadVersionsComboBox);
            this.Controls.Add(this.messageLabel);
            this.Controls.Add(this.messageTextBox);
            this.Controls.Add(this.publishButton);
            this.Controls.Add(this.DownButton);
            this.Controls.Add(this.upButton);
            this.Controls.Add(this.minusButton);
            this.Controls.Add(this.AddButton);
            this.Controls.Add(this.fileTypeComboBox);
            this.Controls.Add(this.fileTypeLabel);
            this.Controls.Add(this.LocationButton);
            this.Controls.Add(this.outPutLocationTextBox);
            this.Controls.Add(this.outPutLocationLabel);
            this.Controls.Add(this.PlotStylesComboBox);
            this.Controls.Add(this.PlotStyleLabel);
            this.Controls.Add(this.dwgGridView);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(670, 360);
            this.Name = "PrintForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "PrintForm";
            this.Load += new System.EventHandler(this.PrintForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dwgGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label PlotStyleLabel;
        private System.Windows.Forms.ComboBox PlotStylesComboBox;
        private System.Windows.Forms.Label outPutLocationLabel;
        private System.Windows.Forms.TextBox outPutLocationTextBox;
        private System.Windows.Forms.Button LocationButton;
        private System.Windows.Forms.Label fileTypeLabel;
        private System.Windows.Forms.ComboBox fileTypeComboBox;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.Button minusButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button DownButton;
        private System.Windows.Forms.DataGridView dwgGridView;
        private System.Windows.Forms.Button publishButton;
        private System.Windows.Forms.RichTextBox messageTextBox;
        private System.Windows.Forms.Label messageLabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cadVersionsComboBox;
        private System.Windows.Forms.Button accoreconsoleBrowserButton;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox accoreconsoleLocationTextBox;
        private System.Windows.Forms.DataGridViewTextBoxColumn dwgName;
        private System.Windows.Forms.DataGridViewTextBoxColumn SheetFile;
        private System.Windows.Forms.DataGridViewTextBoxColumn PageSize;
        private System.Windows.Forms.DataGridViewTextBoxColumn Status;
        private System.Windows.Forms.Label orientationLabel;
        private System.Windows.Forms.ComboBox orientationComboBox;
        private System.Windows.Forms.CheckBox showConsoleCheckBox;
        private System.Windows.Forms.Label showConsoleLabel;
        private System.Windows.Forms.Label StyleLabel;
        private System.Windows.Forms.ComboBox styleComboBox;
    }
}