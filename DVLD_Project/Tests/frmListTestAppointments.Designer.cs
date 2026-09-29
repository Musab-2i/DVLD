namespace DVLD_Project.Tests
{
    partial class frmListTestAppointments
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
            this.ctrlDrivingLicenseApplicationInfo1 = new DVLD_Project.Applications.LocalDrivingLicenseApplications.Controls.ctrlDrivingLicenseApplicationInfo();
            this.lblTestTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dgvAllTestAppointments = new System.Windows.Forms.DataGridView();
            this.cmsTakesEditTest = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.takeTestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lblDataRecords = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ctrlCloseButton1 = new DVLD_Project.ctrlCloseButton();
            this.btnScheduleTest = new System.Windows.Forms.Button();
            this.pbTestImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllTestAppointments)).BeginInit();
            this.cmsTakesEditTest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDrivingLicenseApplicationInfo1
            // 
            this.ctrlDrivingLicenseApplicationInfo1.Location = new System.Drawing.Point(29, 113);
            this.ctrlDrivingLicenseApplicationInfo1.Name = "ctrlDrivingLicenseApplicationInfo1";
            this.ctrlDrivingLicenseApplicationInfo1.Size = new System.Drawing.Size(743, 396);
            this.ctrlDrivingLicenseApplicationInfo1.TabIndex = 0;
            // 
            // lblTestTitle
            // 
            this.lblTestTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTestTitle.ForeColor = System.Drawing.Color.CadetBlue;
            this.lblTestTitle.Location = new System.Drawing.Point(186, 41);
            this.lblTestTitle.Name = "lblTestTitle";
            this.lblTestTitle.Size = new System.Drawing.Size(602, 46);
            this.lblTestTitle.TabIndex = 21;
            this.lblTestTitle.Text = "Vision Test Appointments";
            this.lblTestTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.label1.Location = new System.Drawing.Point(37, 519);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(104, 15);
            this.label1.TabIndex = 77;
            this.label1.Text = "Appointments :";
            // 
            // dgvAllTestAppointments
            // 
            this.dgvAllTestAppointments.AllowUserToAddRows = false;
            this.dgvAllTestAppointments.AllowUserToDeleteRows = false;
            this.dgvAllTestAppointments.AllowUserToOrderColumns = true;
            this.dgvAllTestAppointments.BackgroundColor = System.Drawing.Color.White;
            this.dgvAllTestAppointments.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAllTestAppointments.ContextMenuStrip = this.cmsTakesEditTest;
            this.dgvAllTestAppointments.Location = new System.Drawing.Point(33, 548);
            this.dgvAllTestAppointments.Name = "dgvAllTestAppointments";
            this.dgvAllTestAppointments.ReadOnly = true;
            this.dgvAllTestAppointments.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAllTestAppointments.Size = new System.Drawing.Size(734, 128);
            this.dgvAllTestAppointments.TabIndex = 78;
            // 
            // cmsTakesEditTest
            // 
            this.cmsTakesEditTest.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.takeTestToolStripMenuItem});
            this.cmsTakesEditTest.Name = "cmsTakesEditTest";
            this.cmsTakesEditTest.Size = new System.Drawing.Size(201, 86);
            this.cmsTakesEditTest.Opening += new System.ComponentModel.CancelEventHandler(this.cmsTakesEditTest_Opening);
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.ContextEditInfo;
            this.editToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(200, 30);
            this.editToolStripMenuItem.Text = "Edit Test Appointment";
            this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
            // 
            // takeTestToolStripMenuItem
            // 
            this.takeTestToolStripMenuItem.Image = global::DVLD_Project.Properties.Resources.ContextTakeTest;
            this.takeTestToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.takeTestToolStripMenuItem.Name = "takeTestToolStripMenuItem";
            this.takeTestToolStripMenuItem.Size = new System.Drawing.Size(200, 30);
            this.takeTestToolStripMenuItem.Text = "Take Test";
            this.takeTestToolStripMenuItem.Click += new System.EventHandler(this.takeTestToolStripMenuItem_Click);
            // 
            // lblDataRecords
            // 
            this.lblDataRecords.AutoSize = true;
            this.lblDataRecords.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDataRecords.Location = new System.Drawing.Point(112, 688);
            this.lblDataRecords.Name = "lblDataRecords";
            this.lblDataRecords.Size = new System.Drawing.Size(28, 16);
            this.lblDataRecords.TabIndex = 80;
            this.lblDataRecords.Text = "???";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(37, 687);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 18);
            this.label2.TabIndex = 79;
            this.label2.Text = "# Records:";
            // 
            // ctrlCloseButton1
            // 
            this.ctrlCloseButton1.Location = new System.Drawing.Point(686, 682);
            this.ctrlCloseButton1.Name = "ctrlCloseButton1";
            this.ctrlCloseButton1.Size = new System.Drawing.Size(81, 28);
            this.ctrlCloseButton1.TabIndex = 81;
            // 
            // btnScheduleTest
            // 
            this.btnScheduleTest.BackgroundImage = global::DVLD_Project.Properties.Resources.ButtonAppointments;
            this.btnScheduleTest.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnScheduleTest.FlatAppearance.BorderSize = 0;
            this.btnScheduleTest.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnScheduleTest.Location = new System.Drawing.Point(729, 511);
            this.btnScheduleTest.Name = "btnScheduleTest";
            this.btnScheduleTest.Size = new System.Drawing.Size(38, 31);
            this.btnScheduleTest.TabIndex = 82;
            this.btnScheduleTest.UseVisualStyleBackColor = true;
            this.btnScheduleTest.Click += new System.EventHandler(this.btnScheduleTest_Click);
            // 
            // pbTestImage
            // 
            this.pbTestImage.Image = global::DVLD_Project.Properties.Resources.VisionTest;
            this.pbTestImage.Location = new System.Drawing.Point(40, 11);
            this.pbTestImage.Name = "pbTestImage";
            this.pbTestImage.Size = new System.Drawing.Size(140, 106);
            this.pbTestImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pbTestImage.TabIndex = 20;
            this.pbTestImage.TabStop = false;
            // 
            // frmListTestAppointments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 718);
            this.Controls.Add(this.btnScheduleTest);
            this.Controls.Add(this.ctrlCloseButton1);
            this.Controls.Add(this.lblDataRecords);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgvAllTestAppointments);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTestTitle);
            this.Controls.Add(this.pbTestImage);
            this.Controls.Add(this.ctrlDrivingLicenseApplicationInfo1);
            this.Name = "frmListTestAppointments";
            this.Text = "frmListTestAppointments";
            this.Load += new System.EventHandler(this.frmListTestAppointments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAllTestAppointments)).EndInit();
            this.cmsTakesEditTest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pbTestImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Applications.LocalDrivingLicenseApplications.Controls.ctrlDrivingLicenseApplicationInfo ctrlDrivingLicenseApplicationInfo1;
        private System.Windows.Forms.Label lblTestTitle;
        private System.Windows.Forms.PictureBox pbTestImage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dgvAllTestAppointments;
        private System.Windows.Forms.Label lblDataRecords;
        private System.Windows.Forms.Label label2;
        private ctrlCloseButton ctrlCloseButton1;
        private System.Windows.Forms.Button btnScheduleTest;
        private System.Windows.Forms.ContextMenuStrip cmsTakesEditTest;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem takeTestToolStripMenuItem;
    }
}