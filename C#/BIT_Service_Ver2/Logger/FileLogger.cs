using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Logger
{
    class FileLogger : LogBase
    {
        
        public string filepath = "C:\\Users\\chris\\Desktop\\logger.txt";

        public override int Log(string className, string message)
        {
            throw new NotImplementedException();
        }

        public override void Log(string message)
        {
            
            lock (lockObj)
            {
                using (StreamWriter streamWriter = new StreamWriter(filepath))
                {
                    streamWriter.WriteLine(DateTime.Now + " ," + message);
                    streamWriter.Close();
                }
            }
        }
    }
}
