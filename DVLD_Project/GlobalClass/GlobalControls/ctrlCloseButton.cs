using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ctrlCloseButton : UserControl
    {
        public ctrlCloseButton()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // 1. البحث عن الشاشة الرئيسية التي تستضيف هذا الزر
            Form parentForm = this.FindForm();

            // 2. التحقق من أننا وجدنا الشاشة فعلاً (حماية من الانهيار)
            if (parentForm != null)
            {
                // 3. إغلاق الشاشة
                parentForm.Close();
            }
        }
    }
}
