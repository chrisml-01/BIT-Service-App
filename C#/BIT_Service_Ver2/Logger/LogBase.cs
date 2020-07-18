using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Logger
{
    public abstract class LogBase
    {
        protected readonly object lockObj = new object();
        public abstract int Log(string className , string message);
        public abstract void Log(string message);
    }
}
