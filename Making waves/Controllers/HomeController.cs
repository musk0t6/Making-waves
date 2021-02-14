using Making_waves.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;


namespace Making_waves.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // setting up project directory
            String directory = AppDomain.CurrentDomain.BaseDirectory;

            // Downloading json
            var reader = new WebClient().DownloadString("https://reqres.in/api/example?per_page=2&page=1");
            // Declearing json to object
            var jsonObject = JsonConvert.DeserializeObject<JsonModel>(reader);

            ViewModel viewValues = new ViewModel();
            foreach (var j in jsonObject.data.OrderBy(o => o.year))
            {
                var tmp = j.pantone_value.Split('-');
                if (int.Parse(tmp[0]) % 3 == 0)
                {
                    viewValues.group1.Add(j);
                }
                if (int.Parse(tmp[0]) % 2 == 0)
                {
                    viewValues.group2.Add(j);
                }
                if (int.Parse(tmp[0]) % 3 != 0 && int.Parse(tmp[0]) % 2 != 0)
                {
                    viewValues.group3.Add(j);
                }
            }
            return View(viewValues);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}