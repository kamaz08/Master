using AnalizaAlgorytmów.Models;
using Chart.Mvc.ComplexChart;
using Lista1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AnalizaAlgorytmow.Controllers.L1
{
    public class ElectionController : Controller
    {
        // GET: Election
        public ActionResult Index(int u = 1000, int t = 10000)
        {
            var x = GetModel(u, t);

            return View(x);
        }

        private ElectionModel GetModel(int u, int t)
        {
            return new ElectionModel
            {
                Data = new List<ElectionGraph>()
                {
                    new Election(u).Test(t),
                    new Election(2,u).Test(t),
                    new Election(u/2,u).Test(t),
                    new Election(u,u).Test(t)
                }
            };
        }
    }
}