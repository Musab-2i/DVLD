using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Global_Forms
{
    public partial class frmBase : Form
    {
        //// استدعاء مكتبة الويندوز
        //[DllImport("dwmapi.dll")]
        //private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);

        //private const int DWMWA_USE_IMMERSIVE_DARK_MODE = 20;
        //private void EnableDarkModeTitleBar()
        //{
        //    int darkMode = 1;
        //    DwmSetWindowAttribute(this.Handle, DWMWA_USE_IMMERSIVE_DARK_MODE, ref darkMode, sizeof(int));
        //}

        public frmBase()
        {
            InitializeComponent();

            //this.FormBorderStyle = FormBorderStyle.FixedDialog;
            //// 2. إجبار الشاشة لتظهر في منتصف الشاشة دائماً
            //this.StartPosition = FormStartPosition.CenterParent;

            //// 3. تثبيت الحجم الافتراضي (مثلاً 800x500)
            //if (!this.DesignMode)
            //{
            //    this.Size = new System.Drawing.Size(1000, 550);
            //}
            //EnableDarkModeTitleBar();
        }

        //// استخدام هذه الدالة هو الممارسة الأفضل للتعامل مع الـ APIs الخاصة بالـ UI
        //protected override void OnHandleCreated(EventArgs e)
        //{
        //    base.OnHandleCreated(e);

        //    // تشغيل الدالة فقط في وقت التشغيل الفعلي وليس وقت التصميم
        //    if (!this.DesignMode)
        //    {
        //        EnableDarkModeTitleBar();
        //    }
        //}

    }
}
