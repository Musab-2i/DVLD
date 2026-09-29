namespace DVLD_Project.License
{
    partial class frmShowLocalLicenseInfo
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
            this.ctrlDriverLicenseInfo1 = new DVLD_Project.License.Controls.ctrlLocalDriverLicenseInfo();
            this.ctrlCloseButton1 = new DVLD_Project.ctrlCloseButton();
            this.SuspendLayout();
            // 
            // ctrlDriverLicenseInfo1
            // 
            this.ctrlDriverLicenseInfo1.Location = new System.Drawing.Point(12, 12);
            this.ctrlDriverLicenseInfo1.Name = "ctrlDriverLicenseInfo1";
            this.ctrlDriverLicenseInfo1.Size = new System.Drawing.Size(726, 404);
            this.ctrlDriverLicenseInfo1.TabIndex = 0;
            // 
            // ctrlCloseButton1
            // 
            this.ctrlCloseButton1.Location = new System.Drawing.Point(650, 421);
            this.ctrlCloseButton1.Name = "ctrlCloseButton1";
            this.ctrlCloseButton1.Size = new System.Drawing.Size(81, 34);
            this.ctrlCloseButton1.TabIndex = 1;
            // 
            // frmShowLocalLicenseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 458);
            this.Controls.Add(this.ctrlCloseButton1);
            this.Controls.Add(this.ctrlDriverLicenseInfo1);
            this.Name = "frmShowLocalLicenseInfo";
            this.Text = "Local License Info";
            this.Load += new System.EventHandler(this.frmShowLicenseInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlLocalDriverLicenseInfo ctrlDriverLicenseInfo1;
        private ctrlCloseButton ctrlCloseButton1;
    }
}