using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace FileDiff
{
    public class HtmlUIWriter : IWriter
    {
        public void Write(string data)
        {
            var fileName = String.Format(@"c:\diffFile{0}.html", 
                                         DateTime.Now.ToString()
                                         .Replace("/", "_")
                                         .Replace(":", "."));

            var proc = new Process();
            try
            {
                File.WriteAllText(fileName, data);
                proc.StartInfo = new ProcessStartInfo(fileName);
                proc.Start();
            }
            finally
            {
                Thread.Sleep(500);
                if (File.Exists(fileName))
                    File.Delete(fileName);                        
            }
        }
    }
}
