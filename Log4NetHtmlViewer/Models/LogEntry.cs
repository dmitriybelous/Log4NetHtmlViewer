using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Log4NetHtmlViewer.Models
{
    public class LogEntry
    {
        public int ThreadId { get; set; }
        public int ProcessId { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Level { get; set; }
        public string Source { get; set; }
        public string Message { get; set; }
        public string MachineName { get; set; }
        public string HostName { get; set; }

        public LogEntry()
        {

        }

        public LogEntry (string line)
        {
            try
            {
                string[] dateparts = line.Split(',');
                this.TimeStamp = Convert.ToDateTime(dateparts[0]);

                string[] parts = dateparts[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                parts.Where(s => !string.IsNullOrEmpty(s));

                this.ThreadId = int.Parse(parts[0]);
                this.ProcessId = int.Parse(parts[1].TrimStart('[').TrimEnd(']'));
                this.Level = parts[2];
                this.Source = parts[3];

                parts = string.Join(" ", parts).Split('-');
                this.Message = parts[1];
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}