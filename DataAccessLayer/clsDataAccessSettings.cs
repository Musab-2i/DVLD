using System;
using System.Diagnostics;
using System.Configuration;

namespace DataAccessLayer
{
    internal class clsDataAccessSettings
    {
        //public static string ConnectionString = "Server = .; Database = DVLD_DB; User Id = sa; Password = 123456";
        public static string ConnectionString = ConfigurationManager.ConnectionStrings["DVLD_Connection"].ConnectionString;

        // Before After Updating

        //Old Save In file refrencese => 90
        /*public static void LogError(string ErrorMessage)
        {
            // تحديد مسار واسم الملف (سيتم إنشاؤه في مجلد bin/Debug بجانب البرنامج)
            string filePath = "ErrorLog.txt";

            try
            {
                // صياغة الرسالة مع تاريخ ووقت حدوث الخطأ
                string logEntry = $"[{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")}] ERROR: {ErrorMessage}{Environment.NewLine}";

                // AppendAllText ستقوم بإنشاء الملف إذا لم يكن موجوداً، أو إضافة سطر جديد إذا كان موجوداً
                File.AppendAllText(filePath, logEntry);
            }
            catch
            {
                // لا نفعل شيئاً هنا حتى لا ينهار البرنامج إذا فشل تسجيل الخطأ
            }
        }*/

        //refrencese => 14
        public static void LogError(string ErrorMessage, string sourceName)
        {
            sourceName = "DVLD";
            try
            {
                if (!EventLog.SourceExists(sourceName))
                {
                    EventLog.CreateEventSource(sourceName, "Application");
                }

                EventLog.WriteEntry(sourceName, ErrorMessage, EventLogEntryType.Error);

            }
            catch
            {
                // لا نفعل شيئاً هنا حتى لا ينهار البرنامج إذا فشل تسجيل الخطأ
            }
        }

    }
}
