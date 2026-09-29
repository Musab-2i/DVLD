using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD_Project.GlobalClass
{
    internal class clsEventLogger
    {
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
