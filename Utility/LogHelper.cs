using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class LogHelper
    {
        private static string _logPath;
        public static string LogPath //如果外界没有给他初始化，那么默认该方法会按照App的方式去找路径
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_logPath))
                {
                    _logPath = AppDomain.CurrentDomain.BaseDirectory + "\\Logs\\";
                }
                return _logPath;
            }
        }

        public static void WriteLog(string sender, string type = "ApplicationError")
        {
            Task.Run(() =>
            {
                DoLog(type, sender);
            });
        }

        private static void DoLog(string logType, string msg)
        {
            var param = new List<string> { logType, msg, LogPath + DateTime.Now.ToString("yyyyMMdd") };
            try
            {
                var fileName = logType;
                var logPath = param[2];
                if (!Directory.Exists(logPath))
                {
                    Directory.CreateDirectory(logPath);
                }
                fileName = LogName(logPath, logType, 0);
                fileName += ".txt";
                var vFileName = logPath + @"\" + fileName;
                var fs = new FileStream(vFileName, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                var sw = new StreamWriter(fs);
                sw.WriteLine(DateTime.Now.ToLongTimeString() + ":" + msg);//记录生成log的时间
                sw.Close();
                fs.Close();
            }
            catch
            {
                // ignored
            }
        }


        /// <summary>
        /// log名称及大小验证
        /// </summary>
        /// <param name="logPath"></param>
        /// <param name="fileName"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        private static string LogName(string logPath, string fileName, int index)
        {
            //判断问价是否存在
            if (!Directory.Exists(logPath + @"\" + fileName + ".txt"))
            {
                //Directory.CreateDirectory(logPath + @"\" + fileName + ".txt");
                
            }
            else
            {
                FileInfo fileInfo = new FileInfo(logPath + @"\" + fileName + ".txt");
                long len = fileInfo.Length;
                if (len > 1000000)
                {
                    fileName += "(" + index + ")";
                    index++;
                    fileName = LogName(logPath, fileName, index);
                }
            }
            return fileName;
        }

    }
}
