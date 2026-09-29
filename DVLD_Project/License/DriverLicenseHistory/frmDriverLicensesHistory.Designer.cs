namespace DVLD_Project.License
{
    partial class frmDriverLicensesHistory
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
            this.ctrlDriverLicenses1 = new DVLD_Project.License.DriverLicenseHistory.Controls.ctrlDriverLicenses();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblShowPersonInfo = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // ctrlDriverLicenses1
            // 
            this.ctrlDriverLicenses1.Location = new System.Drawing.Point(8, 192);
            this.ctrlDriverLicenses1.Name = "ctrlDriverLicenses1";
            this.ctrlDriverLicenses1.Size = new System.Drawing.Size(915, 339);
            this.ctrlDriverLicenses1.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.CadetBlue;
            this.label3.Location = new System.Drawing.Point(300, 146);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(330, 46);
            this.label3.TabIndex = 21;
            this.label3.Text = "Licenses History";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.DriverLicensesHistory;
            this.pictureBox1.Location = new System.Drawing.Point(395, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(140, 122);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 20;
            this.pictureBox1.TabStop = false;
            // 
            // lblShowPersonInfo
            // 
            this.lblShowPersonInfo.AutoSize = true;
            this.lblShowPersonInfo.Font = new System.Drawing.Font("Arial Rounded MT Bold", 9.75F);
            this.lblShowPersonInfo.LinkColor = System.Drawing.Color.CadetBlue;
            this.lblShowPersonInfo.Location = new System.Drawing.Point(783, 177);
            this.lblShowPersonInfo.Name = "lblShowPersonInfo";
            this.lblShowPersonInfo.Size = new System.Drawing.Size(115, 15);
            this.lblShowPersonInfo.TabIndex = 86;
            this.lblShowPersonInfo.TabStop = true;
            this.lblShowPersonInfo.Text = "View Person Info";
            this.lblShowPersonInfo.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lblShowPersonInfo_LinkClicked);
            // 
            // frmDriverLicensesHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 530);
            this.Controls.Add(this.lblShowPersonInfo);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ctrlDriverLicenses1);
            this.Name = "frmDriverLicensesHistory";
            this.Text = "Driver Licenses History";
            this.Load += new System.EventHandler(this.frmDriverLicensesHistory_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DriverLicenseHistory.Controls.ctrlDriverLicenses ctrlDriverLicenses1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.LinkLabel lblShowPersonInfo;
    }
}