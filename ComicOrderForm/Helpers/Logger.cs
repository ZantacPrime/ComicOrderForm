using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ComicOrderForm.Helpers {
    public static class Logger {
        private static bool _isInitialized { get; set; }
        private static FileInfo _log {get;set;}

        public static void Initialize() {
            if(!_isInitialized) {
                _log = new FileInfo($@"{AppDomain.CurrentDomain.BaseDirectory}\logs\order_log_{DateTime.Now.ToString("yyyy_MM_dd")}");
                _isInitialized = true;
            }
        }

        public static void Log(string Message, Dictionary<string, object> Params = null) {
            using(var writer = _log.AppendText()) {
                writer.WriteLine($"{DateTime.Now.ToString("yyyyMMddHHmmss")} - {Message}{(Params != null ? ":" : "")}");
                if(Params != null) {
                    foreach(var param in Params) {
                        writer.WriteLine($"{param.Key}: {param.Value}");
                    }
                }
            }
        }

        public static void LogError(string Message, Dictionary<string, object> Params = null) {
            Log($"*ERROR* - {Message}", Params);
        }

        public static void LogError(string Message, Exception Ex, Dictionary<string, object> Params = null) {
            var objs = new Dictionary<string, object>();
            objs.Add("Ex.Message", Ex.Message);
            objs.Add("Ex.StackTrace", Ex.StackTrace);
            Exception inner = Ex.InnerException;
            int innerCount = 1;
            while(inner != null) {
                objs.Add($"InnerEx{innerCount++}.Message", inner.Message);
                if(innerCount >= 5) {
                    //I have never known to have this many inner exceptions, so I'm going to go with "This shit is broke as fuck, so bail out."
                    break;
                }
                inner = inner.InnerException;
            }
            if(Params!= null) {
                foreach(var param in Params) {
                    objs.Add(param.Key, param.Value);
                }
            }
            LogError(Message, objs);
        }
    }
}
