using Microsoft.Graph;
using System.Text.RegularExpressions;
using File = System.IO.File;

namespace freeArve.util
{
    internal class Log
    {
        public const int ERROR_LEVEL = 1;
        public const int INFO_LEVEL = 2;
        public const int DEBUG_LEVEL = 3;
        public static DateTime NOW = DateTime.Now;
        private static string logFilePath;


        public static string getLogFilePath()
        {
            if (logFilePath == null)
            {
                logFilePath = Conf.logFileFolderPath;
                if (!Conf.logFileFolderPath.EndsWith("\\")) {
                    logFilePath +=  "\\";
                }
                logFilePath += DateTime.Now.ToString("yyyy-MM-dd") + "_" + "freeEArve.log";
            }
            return logFilePath;
        }

        public static void logException(Exception e, string msg)
        {
            string fullMsg = $"{msg} {Environment.NewLine} {e.Message} {Environment.NewLine} {Environment.NewLine} {e.ToString()}";

            error(fullMsg);
        }

        public static void error(string v)
        {
            if (Conf.logLevel >= ERROR_LEVEL)
            {
                writeLogMsg("--- ERROR --- : " + v);
            }
        }

        public static void info(string v)
        {
            if (Conf.logLevel >= INFO_LEVEL)
            {
                writeLogMsg(v);
            }
        }

        public static void debug(string v)
        {
            if (Conf.logLevel >= DEBUG_LEVEL)
            {
                writeLogMsg(v);
            }
        }

        public static void deleteLogFile()
        {
            try
            {
                File.Delete(getLogFilePath());
            } catch (Exception e)
            {
                Util.reportException(e, "Failed to delete file: " + getLogFilePath());
            }
        }

        private static void writeLogMsg(string v)
        {
            v += Environment.NewLine;
            System.IO.File.AppendAllText(getLogFilePath(), v);
            Form1.addMsg(v);
        }
    }
}
