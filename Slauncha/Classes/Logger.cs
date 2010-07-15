using System;
using System.IO;

namespace Slauncha
{
    public static class Logger
    {
        public static void Log(string format, params object[] arg)
        {
            Log(String.Format(format, arg));
        }

        public static void Log(string message)
        {
            string appPath = Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            File.AppendAllText(appPath + "\\log.txt", DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + ": " + message + Environment.NewLine);
        }
    }
}