using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text.RegularExpressions;


namespace Log4NetHtmlViewer.Models
{
    public class LogParser
    {
        public string LogPath { get; set; }

        public LogParser(string path)
        {
            LogPath = path;
        }

        public List<LogEntry> GetLogEntries()
        {
            List<LogEntry> logEntries = new List<LogEntry>();
            string error = "";

            foreach (string line in File.ReadLines(LogPath))
            {
                if (line != string.Empty)
                {
                    LogEntry entry = new LogEntry();

                    if (Regex.IsMatch(line, @"^\d+"))
                    {
                        if (error != "")
                        {
                            LogEntry errorEntry = ParseLine(error);
                            errorEntry.Message = error;
                            errorEntry.Level = "ERROR";
                            error = "";
                            logEntries.Add(errorEntry);
                        }
                        entry = ParseLine(line);
                        if (entry.Level != "ERROR")
                            logEntries.Add(entry);
                        else
                            error += line;
                    }
                    else
                    {
                        error += line;
                    }

                }
            }

            return logEntries;
        }

        private LogEntry ParseLine(string line)
        {
            LogEntry entry = new LogEntry();
            try
            {
                string[] dateparts = line.Split(',');
                entry.TimeStamp = Convert.ToDateTime(dateparts[0]);

                string[] parts = dateparts[1].Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
                parts.Where(s => !string.IsNullOrEmpty(s));

                if (parts.Count() > 0)
                {
                    entry.ThreadId = int.Parse(parts[0]);
                    entry.ProcessId = int.Parse(parts[1].TrimStart('[').TrimEnd(']'));
                    entry.Level = parts[2];
                    entry.Source = parts[3];

                    parts = string.Join(" ", parts).Split('-');
                    entry.Message = parts[1];
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return entry;
        }
    }
}