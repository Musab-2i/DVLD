namespace DVLD_Project.Applications.LocalDrivingLicenseApplications
{
    partial class frmManageLocalLicenseApplications
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmManageLocalLicenseApplications));
            this.label3 = new System.Windows.Forms.Label();
            this.cbFilterBy = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblDataRecords = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAllLocalApplications = new System.Windows.Forms.DataGridView();
            this.cmsLocalLicenseApp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.showDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.DeleteApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.CancelApplicationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.ScheduleTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.visionTesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleWrittenTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.scheduleStreetTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.IssueDrivingLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ShowLicenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.showPersonLicenseHestoyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cbIsActiveFilter = new System.Windows.Forms.ComboBox();
            this.txtFilterValue = new System.Windows.Forms.TextBox();
            this.ctrlCloseButton1 = new DVLD_Project.ctrlCloseButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.btnAddNewEdit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalApplications)).BeginInit();
            this.cmsLocalLicenseApp.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CadetBlue;
            this.label3.Location = new System.Drawing.Point(252, 147);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(681, 46);
            this.label3.TabIndex = 19;
            this.label3.Text = "Manage Local License Applications";
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.SystemColors.Window;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFilterBy.FormattingEnabled = true;
            this.cbFilterBy.Items.AddRange(new object[] {
            "None",
            "L.D.L.AppID",
            "National No",
            "Full Name",
            "Status"});
            this.cbFilterBy.Location = new System.Drawing.Point(76, 221);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(156, 24);
            this.cbFilterBy.TabIndex = 16;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 225);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 16);
            this.label2.TabIndex = 15;
            this.label2.Text = "Filter By:";
            // 
            // lblDataRecords
            // 
            this.lblDataRecords.AutoSize = true;
            this.lblDataRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRecords.Location = new System.Drawing.Point(87, 510);
            this.lblDataRecords.Name = "lblDataRecords";
            this.lblDataRecords.Size = new System.Drawing.Size(28, 16);
            this.lblDataRecords.TabIndex = 13;
            this.lblDataRecords.Text = "???";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 508);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 12;
            this.label1.Text = "# Records:";
            // 
            // dgvAllLocalApplications
            // 
            this.dgvAllLocalApplications.AllowUserToAddRows = false;
            this.dgvAllLocalApplications.AllowUserToDeleteRows = false;
            this.dgvAllLocalApplications.AllowUserToOrderColumns = true;
            this.dgvAllLocalApplications.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllLocalApplications.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllLocalApplications.ContextMenuStrip = this.cmsLocalLicenseApp;
            this.dgvAllLocalApplications.Location = new System.Drawing.Point(12, 250);
            this.dgvAllLocalApplications.Name = "dgvAllLocalApplications";
            this.dgvAllLocalApplications.ReadOnly = true;
            this.dgvAllLocalApplications.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllLocalApplications.Size = new System.Drawing.Size(1160, 247);
            this.dgvAllLocalApplications.TabIndex = 11;
            this.dgvAllLocalApplications.DoubleClick += new System.EventHandler(this.dgvAllLocalApplications_DoubleClick);
            // 
            // cmsLocalLicenseApp
            // 
            this.cmsLocalLicenseApp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.showDetailsToolStripMenuItem,
            this.toolStripMenuItem2,
            this.editToolStripMenuItem,
            this.DeleteApplicationToolStripMenuItem,
            this.toolStripMenuItem3,
            this.CancelApplicationToolStripMenuItem,
            this.toolStripMenuItem1,
            this.ScheduleTestToolStripMenuItem,
            this.toolStripMenuItem4,
            this.IssueDrivingLicenseToolStripMenuItem,
            this.toolStripMenuItem5,
            this.ShowLicenseToolStripMenuItem,
            this.showPersonLicenseHestoyToolStripMenuItem});
            this.cmsLocalLicenseApp.Name = "contextMenuStrip1";
            this.cmsLocalLicenseApp.Size = new System.Drawing.Size(255, 296);
            this.cmsLocalLicenseApp.Opening += new System.ComponentModel.CancelEventHandler(this.cmsLocalLicenseApp_Opening);
            // 
            // showDetailsToolStripMenuItem
            // 
            this.showDetailsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showDetailsToolStripMenuItem.Image")));
            this.showDetailsToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showDetailsToolStripMenuItem.Name = "showDetailsToolStripMenuItem";
            this.showDetailsToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.showDetailsToolStripMenuItem.Text = "Show Details";
            this.showDetailsToolStripMenuItem.Click += new System.EventHandler(this.showDetailsToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(251, 6);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("editToolStripMenuItem.Image")));
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.editToolStripMenuItem.Text = "Edit";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // DeleteApplicationToolStripMenuItem
            // 
            this.DeleteApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("DeleteApplicationToolStripMenuItem.Image")));
            this.DeleteApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DeleteApplicationToolStripMenuItem.Name = "DeleteApplicationToolStripMenuItem";
            this.DeleteApplicationToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.DeleteApplicationToolStripMenuItem.Text = "Delete Application";
            this.DeleteApplicationToolStripMenuItem.Click += new System.EventHandler(this.DeleteApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(251, 6);
            // 
            // CancelApplicationToolStripMenuItem
            // 
            this.CancelApplicationToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("CancelApplicationToolStripMenuItem.Image")));
            this.CancelApplicationToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CancelApplicationToolStripMenuItem.Name = "CancelApplicationToolStripMenuItem";
            this.CancelApplicationToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.CancelApplicationToolStripMenuItem.Text = "Cancel Application";
            this.CancelApplicationToolStripMenuItem.Click += new System.EventHandler(this.CancelApplicationToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(251, 6);
            // 
            // ScheduleTestToolStripMenuItem
            // 
            this.ScheduleTestToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visionTesToolStripMenuItem,
            this.scheduleWrittenTestToolStripMenuItem,
            this.scheduleStreetTestToolStripMenuItem});
            this.ScheduleTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ScheduleTestToolStripMenuItem.Image")));
            this.ScheduleTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ScheduleTestToolStripMenuItem.Name = "ScheduleTestToolStripMenuItem";
            this.ScheduleTestToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.ScheduleTestToolStripMenuItem.Text = "Schedule Test";
            // 
            // visionTesToolStripMenuItem
            // 
            this.visionTesToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("visionTesToolStripMenuItem.Image")));
            this.visionTesToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.visionTesToolStripMenuItem.Name = "visionTesToolStripMenuItem";
            this.visionTesToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.visionTesToolStripMenuItem.Text = "Schedule Vision Test";
            this.visionTesToolStripMenuItem.Click += new System.EventHandler(this.visionTesToolStripMenuItem_Click);
            // 
            // scheduleWrittenTestToolStripMenuItem
            // 
            this.scheduleWrittenTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scheduleWrittenTestToolStripMenuItem.Image")));
            this.scheduleWrittenTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scheduleWrittenTestToolStripMenuItem.Name = "scheduleWrittenTestToolStripMenuItem";
            this.scheduleWrittenTestToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.scheduleWrittenTestToolStripMenuItem.Text = "Schedule Written Test";
            this.scheduleWrittenTestToolStripMenuItem.Click += new System.EventHandler(this.scheduleWrittenTestToolStripMenuItem_Click);
            // 
            // scheduleStreetTestToolStripMenuItem
            // 
            this.scheduleStreetTestToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("scheduleStreetTestToolStripMenuItem.Image")));
            this.scheduleStreetTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.scheduleStreetTestToolStripMenuItem.Name = "scheduleStreetTestToolStripMenuItem";
            this.scheduleStreetTestToolStripMenuItem.Size = new System.Drawing.Size(196, 30);
            this.scheduleStreetTestToolStripMenuItem.Text = "Schedule Street Test";
            this.scheduleStreetTestToolStripMenuItem.Click += new System.EventHandler(this.scheduleStreetTestToolStripMenuItem_Click);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(251, 6);
            // 
            // IssueDrivingLicenseToolStripMenuItem
            // 
            this.IssueDrivingLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("IssueDrivingLicenseToolStripMenuItem.Image")));
            this.IssueDrivingLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.IssueDrivingLicenseToolStripMenuItem.Name = "IssueDrivingLicenseToolStripMenuItem";
            this.IssueDrivingLicenseToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.IssueDrivingLicenseToolStripMenuItem.Text = "Issue Driving License (First Time)";
            this.IssueDrivingLicenseToolStripMenuItem.Click += new System.EventHandler(this.IssueDrivingLicenseToolStripMenuItem_Click);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(251, 6);
            // 
            // ShowLicenseToolStripMenuItem
            // 
            this.ShowLicenseToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("ShowLicenseToolStripMenuItem.Image")));
            this.ShowLicenseToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.ShowLicenseToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ShowLicenseToolStripMenuItem.Name = "ShowLicenseToolStripMenuItem";
            this.ShowLicenseToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.ShowLicenseToolStripMenuItem.Text = "Show License";
            this.ShowLicenseToolStripMenuItem.Click += new System.EventHandler(this.ShowLicenseToolStripMenuItem_Click);
            // 
            // showPersonLicenseHestoyToolStripMenuItem
            // 
            this.showPersonLicenseHestoyToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("showPersonLicenseHestoyToolStripMenuItem.Image")));
            this.showPersonLicenseHestoyToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.showPersonLicenseHestoyToolStripMenuItem.Name = "showPersonLicenseHestoyToolStripMenuItem";
            this.showPersonLicenseHestoyToolStripMenuItem.Size = new System.Drawing.Size(254, 30);
            this.showPersonLicenseHestoyToolStripMenuItem.Text = "Show Person License History";
            this.showPersonLicenseHestoyToolStripMenuItem.Click += new System.EventHandler(this.showPersonLicenseHestoyToolStripMenuItem_Click);
            // 
            // cbIsActiveFilter
            // 
            this.cbIsActiveFilter.BackColor = System.Drawing.SystemColors.Window;
            this.cbIsActiveFilter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbIsActiveFilter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbIsActiveFilter.FormattingEnabled = true;
            this.cbIsActiveFilter.Items.AddRange(new object[] {
            "All",
            "New",
            "Cancelled",
            "Completed"});
            this.cbIsActiveFilter.Location = new System.Drawing.Point(238, 221);
            this.cbIsActiveFilter.Name = "cbIsActiveFilter";
            this.cbIsActiveFilter.Size = new System.Drawing.Size(89, 24);
            this.cbIsActiveFilter.TabIndex = 22;
            this.cbIsActiveFilter.SelectedIndexChanged += new System.EventHandler(this.cbIsActiveFilter_SelectedIndexChanged);
            // 
            // txtFilterValue
            // 
            this.txtFilterValue.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtFilterValue.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilterValue.Location = new System.Drawing.Point(238, 222);
            this.txtFilterValue.Name = "txtFilterValue";
            this.txtFilterValue.Size = new System.Drawing.Size(175, 22);
            this.txtFilterValue.TabIndex = 21;
            this.txtFilterValue.TextChanged += new System.EventHandler(this.txtFilterValue_TextChanged);
            this.txtFilterValue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterValue_KeyPress);
            // 
            // ctrlCloseButton1
            // 
            this.ctrlCloseButton1.Location = new System.Drawing.Point(1085, 508);
            this.ctrlCloseButton1.Name = "ctrlCloseButton1";
            this.ctrlCloseButton1.Size = new System.Drawing.Size(87, 40);
            this.ctrlCloseButton1.TabIndex = 20;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(522, 13);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 18;
            this.pictureBox1.TabStop = false;
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
            this.btnAddNewEdit.TabIndex = 14;
            this.btnAddNewEdit.UseVisualStyleBackColor = true;
            this.btnAddNewEdit.Click += new System.EventHandler(this.btnAddNewEdit_Click);
            // 
            // frmManageLocalLicenseApplications
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1184, 561);
            this.Controls.Add(this.cbIsActiveFilter);
            this.Controls.Add(this.txtFilterValue);
            this.Controls.Add(this.ctrlCloseButton1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.cbFilterBy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnAddNewEdit);
            this.Controls.Add(this.lblDataRecords);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dgvAllLocalApplications);
            this.Name = "frmManageLocalLicenseApplications";
            this.Text = "Manage Local License Applications";
            this.Load += new System.EventHandler(this.frmManageLocalLicenseApplications_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllLocalApplications)).EndInit();
            this.cmsLocalLicenseApp.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ctrlCloseButton ctrlCloseButton1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ComboBox cbFilterBy;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnAddNewEdit;
        private System.Windows.Forms.Label lblDataRecords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAllLocalApplications;
        private System.Windows.Forms.ComboBox cbIsActiveFilter;
        private System.Windows.Forms.TextBox txtFilterValue;
        private System.Windows.Forms.ContextMenuStrip cmsLocalLicenseApp;
        private System.Windows.Forms.ToolStripMenuItem showDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CancelApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem DeleteApplicationToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem ScheduleTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem IssueDrivingLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ShowLicenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem showPersonLicenseHestoyToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        private System.Windows.Forms.ToolStripMenuItem visionTesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleWrittenTestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scheduleStreetTestToolStripMenuItem;
    }
}