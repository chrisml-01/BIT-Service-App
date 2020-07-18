using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BIT_Service_Ver2.Logger
{
    public static class LogHelper
    {
        private static LogBase logger = null;

        public static void Log(LogTarget target, string className, string message)
        {
            switch (target)
            {
                case LogTarget.Database:
                    logger = new DBLogger();
                    logger.Log(className, message);
                    break;
                case LogTarget.File:
                    logger = new FileLogger();
                    logger.Log(message);
                    break;

            }
        }
    }

    public enum LogTarget
    {
        Database, EventLog, File
    }
}
