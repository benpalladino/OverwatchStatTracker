using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace UtilityLogger
{
    public class ErrorLogger
    {
        public void LogError(Exception errorToWrite)
        {
            string message = string.Format("Time: {0}", DateTime.Now.ToString(""));
            message += Environment.NewLine;
            message += "--------------------------------------------";
            message += Environment.NewLine;
            message += string.Format("Message: {0}", errorToWrite.Message);
            message += Environment.NewLine;
            message += string.Format("StackTrace: {0}", errorToWrite.StackTrace);
            message += Environment.NewLine;
            message += string.Format("Message: {0}", errorToWrite.Source);
            message += Environment.NewLine;
            message += string.Format("Message: {0}", errorToWrite.TargetSite.ToString());
            message += Environment.NewLine;
            message += "--------------------------------------------";
            message += Environment.NewLine;

            using (StreamWriter writer = new StreamWriter("C:\\Users\\admin2\\source\\repos\\OverwatchStatTracker\\UtilityLogger\\ErrorLog.txt", true))
            {
                writer.WriteLine(message);
            }

        }
    }
}
