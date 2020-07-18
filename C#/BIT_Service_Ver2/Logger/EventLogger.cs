using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Logger
{
    class EventLogger : LogBase
    {
        //If you have any write permission then you are able to use this:
        public override void Log(string message)
        {
            lock (lockObj)
            {
                EventLog eventLog = new EventLog("");
                eventLog.Source = "BIT_Service_Ver2";
                eventLog.WriteEntry(message);
            }
        }

        public override int Log(string className, string message)
        {
            throw new NotImplementedException();
        }
    }
}
