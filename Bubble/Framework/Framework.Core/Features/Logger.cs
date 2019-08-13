using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Core.Features
{
    public static class Logger
    {
        static readonly ILog Log;

        static Logger()
        {
            log4net.Config.XmlConfigurator.Configure();

            Log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        }

        public static void Error(object msg, string moduleName)
        {
            Log.Error("Module : " + moduleName + "\n" + msg);
        }

        public static void Error(object msg, Exception exception, string moduleName)
        {
            Log.Error("Module : " + moduleName + "\n" + msg, exception);
        }

        public static void Error(Exception exception, string moduleName)
        {
            Log.Error("Module : " + moduleName + "\n" + Convert.ToString(exception.StackTrace), exception);
        }

        public static void Info(object msg, string moduleName)
        {
            Log.Info("Module : " + moduleName + "\n" + msg);
        }

        public static void Warn(object msg, string moduleName)
        {
            Log.Warn("Module : " + moduleName + "\n ALERT!" + msg);
        }

        public static void FatalMessage(string moduleName, string message)
        {
            Log.Fatal("Module: " + moduleName + "\n Message: " + message);
        }

    }
}
