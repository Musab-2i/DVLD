namespace DVLD_Project.Applications.InternationalDrivringLiceinseApplications
{
    partial class frmManageInternationalLicenseApplications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageInternationalLicenseApplications));
            this.cbIsActiveFilter = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.ctrlCloseButton1 = new DVLD_Project.ctrlCloseButton();
            this.btnAddNewEdit = new System.Windows.Forms.Button();
            this.showPersonLicenseHestoyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsLocalLicenseApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDataRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAllInternationalApplications = new System.Windows.Forms.DataGridView();
            this.cmsLocalLicenseApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllInternationalApplications)).BeginInit();
            this.SuspendLayout();
            // 
            // cbIsActiveFilter
            // 
            this.cbIsActiveFilter.BackColor = System.Drawing.SystemColors.Window;
            this.cbIsActiveFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActiveFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsActiveFilter.FormattingEnabled = true;
            this.cbIsActiveFilter.Items.AddRange(new object[] {
            "All",
            "Yes",
            "No"});
            this.cbIsActiveFilter.Location = new System.Drawing.Point(281, 221);
            this.cbIsActiveFilter.Name = "cbIsActiveFilter";
            this.cbIsActiveFilter.Size = new System.Drawing.Size(89, 24);
            this.cbIsActiveFilter.TabIndex = 33;
            this.cbIsActiveFilter.SelectedIndexChanged += new System.EventHandler(this.cbIsActiveFilter_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(281, 222);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(175, 22);
            this.txtFilterValue.TabIndex = 32;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // ctrlCloseButton1
            // 
            this.ctrlCloseButton1.Location = new System.Drawing.Point(1085, 508);
            this.ctrlCloseButton1.Name = "ctrlCloseButton1";
            this.ctrlCloseButton1.Size = new System.Drawing.Size(87, 40);
            this.ctrlCloseButton1.TabIndex = 31;
            // 
            // btnAddNewEdit
            // 
            this.btnAddNewEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddNewEdit.BackgroundImage")));
            this.btnAddNewEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddNewEdit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddNewEdit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddNewEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAddNewEdit.Location = new System.Drawing.Point(1106, 210);
            this.btnAddNewEdit.Name = "btnAddNewEdit";
            this.btnAddNewEdit.Size = new System.Drawing.Size(66, 34);
            this.btnAddNewEdit.TabIndex = 26;
            this.btnAddNewEdit.UseVisualStyleBackColor = true;
            this.btnAddNewEdit.Click += new System.EventHandler(this.btnAddNewEdit_Click);
            // 
            // showPersonLicenseHestoyToolStripMenuItem
            // 
            this.showPersonLicenseHestoyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPersonLicenseHestoyToolStripMenuItem.Image")));
            this.showPersonLicenseHestoyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHestoyToolStripMenuItem.Name = "showPersonLicenseHestoyToolStripMenuItem";
            this.showPersonLicenseHestoyToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
            this.showPersonLicenseHestoyToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHestoyToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHestoyToolStripMenuItem_Click);
            // 
            // ShowLicenseToolStripMenuItem
            // 
            this.ShowLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ShowLicenseToolStripMenuItem.Image")));
            this.ShowLicenseToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicenseToolStripMenuItem.Name = "ShowLicenseToolStripMenuItem";
            this.ShowLicenseToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
            this.ShowLicenseToolStripMenuItem.Text = "Show License";
            this.ShowLicenseToolStripMenuItem.Click += new System.EventHandler(this.ShowLicenseToolStripMenuItem_Click);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showDetailsToolStripMenuItem.Image")));
            this.showDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(233, 30);
            this.showDetailsToolStripMenuItem.Text = "Show Person Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // cmsLocalLicenseApp
            // 
            this.cmsLocalLicenseApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.ShowLicenseToolStripMenuItem,
            this.showPersonLicenseHestoyToolStripMenuItem});
            this.cmsLocalLicenseApp.Name = "contextMenuStrip1";
            this.cmsLocalLicenseApp.Size = new System.Drawing.Size(234, 94);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CadetBlue;
            this.label3.Location = new System.Drawing.Point(268, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(649, 46);
            this.label3.TabIndex = 30;
            this.label3.Text = "International License Applications";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(522, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 29;
            this.pictureBox1.TabStop = false;
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.SystemColors.Window;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "International License ID",
            "Application ID",
            "Driver ID",
            "Local License ID",
            "Is Active"});
            this.cbFilterBy.Location = new System.Drawing.Point(76, 221);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(199, 24);
            this.cbFilterBy.TabIndex = 28;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Filter By:";
            // 
            // lblDataRecords
            // 
            this.lblDataRecords.AutoSize = true;
            this.lblDataRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRecords.Location = new System.Drawing.Point(87, 510);
            this.lblDataRecords.Name = "lblDataRecords";
            this.lblDataRecords.Size = new System.Drawing.Size(28, 16);
            this.lblDataRecords.TabIndex = 25;
            this.lblDataRecords.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 508);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 24;
            this.label1.Text = "# Records:";
            // 
            // dgvAllInternationalApplications
            // 
            this.dgvAllInternationalApplications.AllowUserToAddRows = false;
            this.dgvAllInternationalApplications.AllowUserToDeleteRows = false;
            this.dgvAllInternationalApplications.AllowUserToOrderColumns = true;
            this.dgvAllInternationalApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllInternationalApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllInternationalApplications.ContextMenuStrip = this.cmsLocalLicenseApp;
            this.dgvAllInternationalApplications.Location = new System.Drawing.Point(12, 250);
            this.dgvAllInternationalApplications.Name = "dgvAllInternationalApplications";
            this.dgvAllInternationalApplications.ReadOnly = true;
            this.dgvAllInternationalApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllInternationalApplications.Size = new System.Drawing.Size(1160, 247);
            this.dgvAllInternationalApplications.TabIndex = 23;
            this.dgvAllInternationalApplications.DoubleClick += new System.EventHandler(this.dgvAllInternationalApplications_DoubleClick);
            // 
            // frmManageInternationalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.cbIsActiveFilter);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.ctrlCloseButton1);
            this.Controls.Add(this.btnAddNewEdit);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblDataRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAllInternationalApplications);
            this.Name = "frmManageInternationalLicenseApplications";
            this.Text = "Manage International License Applications";
            this.Load += new System.EventHandler(this.frmManageInternationalLicenseApplications_Load);
            this.cmsLocalLicenseApp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllInternationalApplications)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbIsActiveFilter;
        private System.Windows.Forms.TextBox txtFilterValue;
        private ctrlCloseButton ctrlCloseButton1;
        private System.Windows.Forms.Button btnAddNewEdit;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHestoyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicenseApp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblDataRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAllInternationalApplications;
    }
}