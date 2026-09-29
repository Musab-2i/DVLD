namespace DVLD_Project.Users
{
    partial class frmShowUserInfo
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
            this.ctrlUserCard1 = new DVLD_Project.Users.Controls.ctrlUserCard();
            this.ctrlCloseButton1 = new DVLD_Project.ctrlCloseButton();
            this.SuspendLayout();
            // 
            // ctrlUserCard1
            // 
            this.ctrlUserCard1.Location = new System.Drawing.Point(10, 9);
            this.ctrlUserCard1.Name = "ctrlUserCard1";
            this.ctrlUserCard1.Size = new System.Drawing.Size(769, 400);
            this.ctrlUserCard1.TabIndex = 0;
            // 
            // ctrlCloseButton1
            // 
            this.ctrlCloseButton1.Location = new System.Drawing.Point(688, 404);
            this.ctrlCloseButton1.Name = "ctrlCloseButton1";
            this.ctrlCloseButton1.Size = new System.Drawing.Size(81, 34);
            this.ctrlCloseButton1.TabIndex = 1;
            // 
            // frmShowUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(780, 442);
            this.Controls.Add(this.ctrlCloseButton1);
            this.Controls.Add(this.ctrlUserCard1);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmShowUserInfo";
            this.Text = "User Information";
            this.Load += new System.EventHandler(this.frmShowUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ctrlUserCard ctrlUserCard1;
        private ctrlCloseButton ctrlCloseButton1;
    }
}