﻿using Making_waves.Models;
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
            string directory = AppDomain.CurrentDomain.BaseDirectory;
            string reader = "";
            // Downloading json
            try { 
                reader = new WebClient().DownloadString("https://reqres.in/api/example");
            } catch (Exception e)
            {

            }
            // Declearing json to object
            var jsonObject = JsonConvert.DeserializeObject<PageModel>(reader);

            ViewModel viewValues = new ViewModel();
            foreach (var j in jsonObject.data.OrderBy(o => o.year))
            {
                var tmp = j.pantone_value.Split('-');
                if (int.Parse(tmp[0]) % 3 == 0)
                {
                    viewValues.group1.Add(j);
                    continue;
                }
                else if (int.Parse(tmp[0]) % 2 == 0)
                {
                    viewValues.group2.Add(j);
                }
                else if (int.Parse(tmp[0]) % 3 != 0 && int.Parse(tmp[0]) % 2 != 0)
                {
                    viewValues.group3.Add(j);
                }
            }
            return View(viewValues);
        }


    }
}