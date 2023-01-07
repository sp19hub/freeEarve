using System.Text.RegularExpressions;

namespace freeArve.util
{
    internal class Util
    {
        public static DateTime NOW = DateTime.Now;

        public static string getDesktopFolderPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        public static long getRandomInt()
        {
            return (new DateTimeOffset(DateTime.UtcNow)).ToUnixTimeSeconds();
        }


        public static DateTime makeDefaultDueDate()
        {
            DateTime dueDate = new DateTime(NOW.Year, NOW.Month, Conf.fallBackPaymentDay, 10, 0, 0, 0);
            if (NOW.Day >= Conf.fallBackPaymentDay)
            {
                dueDate = dueDate.AddMonths(1);
            }

            return dueDate;
        }


        public static void reportException(Exception e, string msg)
        {
            string fullMsg = $"{msg} {Environment.NewLine} {e.Message} {Environment.NewLine} {Environment.NewLine} {e.ToString()}";

            Form1.addMsg(fullMsg, true);
            Log.error(fullMsg);
            MessageBox.Show(fullMsg);
        }

        public static string normalizeStr(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }
            str = str.Trim();
            str = Regex.Replace(str, "[\\r\\n]", " ");
            return Regex.Replace(str.Trim(), "\\s\\s+", " ");
        }

    }
}
