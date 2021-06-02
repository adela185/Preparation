using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IssueTrackerInfastructure
{
    public class LogDetailArgs
    {
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
        public override string ToString()
        {
            return $"[{Message}] - Logged At: {LogTime}";
        }
    }

    public class LogHelper
    {
        private static List<LogDetailArgs> _logs = new List<LogDetailArgs>();

        public event EventHandler<List<LogDetailArgs>> LogUpdated;

        public void AddLogInfo(string msg)
        {
            _logs.Add(new LogDetailArgs() { LogTime = DateTime.Now, Message = msg });
            LogUpdated(this, _logs);
        }

        public List<LogDetailArgs> GetAllLogInfo()
        {
            return _logs;
        }
    }
}
