using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using Log4NetHtmlViewer.Models;

namespace Log4NetHtmlViewer.Controllers
{
    public class HomeController : Controller
    {
        string WEB_DIRECTORY = @"\\web";
        string ROOT_WEB_FOLDER = "\\c$\\inetpub\\wwwroot";
        string LOGS_FOLDER = "\\Logs";
        string LOG_FILE_NAME = "log_Web.txt";

        public ActionResult Index()
        {
            //List<LogEntry> list = GetLogEntries(@"C:\Temp\log_Web2.txt");
            List<LogEntry> list = new List<LogEntry>();
            LogViewModel model = new LogViewModel();
            model.Entries = list;

            return View(model);
        }

        private static List<LogEntry> GetLogEntries(string logPath)
        {
            LogParser parser = new LogParser(logPath);
            List<LogEntry> list = parser.GetLogEntries();
            list.OrderByDescending(d => d.TimeStamp);
            return list;
        }

        public ActionResult About()
        {

            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetSites(string server)
        {
            string[] sites = Directory.GetDirectories(WEB_DIRECTORY + server.ToLower() + ROOT_WEB_FOLDER);
            List<string> webs = new List<string>(); // Example List

            foreach (var item in sites)
            {
                string[] subfoldres = Directory.GetDirectories(item);
                if (subfoldres.Contains<string>(item + LOGS_FOLDER))
                    webs.Add(item);
            }

            return this.Json(webs, JsonRequestBehavior.AllowGet);
        }

         [HttpGet]
        public ActionResult GetLog(string logPath)
        {
            List<LogEntry> list = GetLogEntries(logPath + LOGS_FOLDER + "\\" + LOG_FILE_NAME);

            LogViewModel model = new LogViewModel();
            model.Entries = list;

            return PartialView("_LogEntries", list);
        }
    }
}