using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace asp_scheduler.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public string Day { get; set; }
        public string Want { get; set; }
        public string Done { get; set; }

        const string FORMAT = "dd.MM.yyyy"; //dont forget edit Day class if change FORMAT

        //public void OnGet()
        //{
        //    OnGet(null);
        //}

        public void OnGet(string day)
        {
            string now = DateTime.Now.ToString(FORMAT);
            if (string.IsNullOrEmpty(day))
                day = now;
            var w1 = new Week(now, FORMAT);
            now = w1.GetCurWeek();

            //get Monday (week schedule)
            var w = new Week(day, FORMAT);
            day = w.GetCurWeek();
            
            var d = new Day(day, FORMAT);
            d.Load();
            Day = day;
            Want = d.Want;
            Done = d.Done;

            ViewData["CurDay"] = day;
            ViewData["CurWeek"] = w.GetFullWeek();
            DateTime dt = DateTime.ParseExact(day, FORMAT, null);
            ViewData["NextDay"] = dt.AddDays(7).ToString(FORMAT);
            ViewData["PrevDay"] = dt.AddDays(-7).ToString(FORMAT);
            ViewData["IsToday"] = day == now ? "1" : "0";
        }

        public IActionResult OnPostSave()
        {
            string want = Request.Form["want"][0];
            string done = Request.Form["done"][0];
            string day = Request.Form["day"][0];

            var d = new Day(day, FORMAT);
            d.Want = want;
            d.Done = done;
            d.Save();

            return Redirect("~/?Day=" + day);
        }
    }
}
