using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DVLD_Project.License.Controls
{
    public partial class ctrlLocalDriverLicenseWithFilter : UserControl
    {

        public event Action<int> OnLicenseSelected;

        //// 1. تعريف الـ Delegate يحمل الرقم اللي بدك ترسله (مثلاً LicenseID أو PersonID)
        //public delegate void DataBackEventHandler(object sender, int LicenseID);

        //// 2. تعريف الـ Event باستخدام الـ Delegate
        //public event DataBackEventHandler OnLicenseSelected;

        private int _LicenseID = -1;
        public int LicenseID
        {
            get { return ctrlDriverLicenseInfo1.LicenseID /*_LicenseID*/; }
        }

        private bool _FilterEnable = true;
        public bool FilterEnable
        {
            get { return _FilterEnable; }
            set
            {
                _FilterEnable = value;
                gbFilter.Enabled = _FilterEnable;
            }
        }

        public ctrlLocalDriverLicenseWithFilter()
        {
            InitializeComponent();
        }

        private void _InvokeDelegate()
        {
            if (FilterEnable && this.LicenseID != -1)
            {
                OnLicenseSelected?.Invoke(this.LicenseID);
            }
        }


        public void LoadLicenseInfo(int LicenseID)
        {
            txtFilterValue.Text = LicenseID.ToString();
            ctrlDriverLicenseInfo1.LoadLicenseInfo(LicenseID);
            _LicenseID = LicenseID;

            //if (FilterEnable)
            //{
            _InvokeDelegate();
            //}
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!this.ValidateChildren())
            {
                // إظهار رسالة التنبيه التي طلبتها في حال وجود خطأ
                MessageBox.Show("The filter field is required and must be filled in.",
                                "Validation Error",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                return; // إيقاف عملية الحفظ تماماً ومنع الوصول لقاعدة البيانات
            }
            _LicenseID = int.Parse(txtFilterValue.Text);
            ctrlDriverLicenseInfo1.LoadLicenseInfo(_LicenseID);

            _InvokeDelegate();
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnSearch.PerformClick();
            }
            // السماح فقط بالأرقام (IsDigit) وزر المسح Backspace (IsControl)
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // إلغاء الضغطة تماماً وكأنها لم تحدث!
            }
        }

        public void FilterFocus()
        {
            txtFilterValue.Focus();
        }

        //Gemini Code
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            // هل الزر المضغوط هو Enter؟
            if (keyData == Keys.Enter)
            {
                // هل مربع النص هو الذي عليه التركيز (محدد حالياً)؟
                if (txtFilterValue.Focused)
                {
                    btnSearch.PerformClick(); // نفذ ضغطة زر البحث
                    return true; // أخبر النظام أننا تعاملنا مع الضغطة بنجاح (لإيقاف صوت الرنة)
                }
            }

            // إذا لم يكن Enter، دع النظام يكمل عمله الطبيعي
            return base.ProcessCmdKey(ref msg, keyData);
        }

        private void txtFilterValue_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterValue.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtFilterValue, "This field is required!");
                return;
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(txtFilterValue, null);
            }
        }
    }
}