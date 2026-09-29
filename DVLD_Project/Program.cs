using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmMainMenu());
            //Application.Run(new fmLoginScreen());

            fmLoginScreen loginForm = new fmLoginScreen();

            // 2. نختبر: هل رجعت الشاشة بإشارة "OK" (نجاح الدخول)؟
            if (loginForm.ShowDialog() == DialogResult.OK)
            {
                // إذا نجح الدخول، نجعل الشاشة الرئيسية هي الأساسية للبرنامج
                Application.Run(new frmMainMenu());
            }
            else
            {
                // إذا قام المستخدم بالضغط على زر (X) لإغلاق شاشة الدخول، ننهي البرنامج تماماً
                Application.Exit();
            }

        }
    }
}
