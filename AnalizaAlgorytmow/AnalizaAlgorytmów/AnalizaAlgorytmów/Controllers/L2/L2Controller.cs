using Lista2;
using Lista2.Core;
using Lista2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace AnalizaAlgorytmów.Controllers.L2
{
    public class L2Controller : Controller
    {
        // GET: L2

        public ActionResult Index()
        {
            var set = new GoodMultiset(1, 10000);

            var hash = new HashFunction();
            var multiset = new HashMultiset(set, hash);
            var kmin = new KMin(multiset);

            int a, b;
            a = 200;
           
            for (int i =2; i < 1000; i++)
            {
                var he = kmin.Test(10000, 2);
                var test = he.Where(x => x.Value < 0.9 || x.Value > 1.1).Count();
                if (test >= 500)
                    b = i;
            }


            return View(new EstimationModel
            {
                ListGraph = new List<GraphModel>()
                {
                    new GraphModel {Title = "K2", Id = "K2", List = kmin.Test(10000, 2) },
                    new GraphModel {Title = "K3", Id = "K3", List = kmin.Test(10000, 3) },
                    new GraphModel {Title = "K10", Id = "K10", List = kmin.Test(10000, 10) },
                    new GraphModel {Title = "K100", Id = "K100", List = kmin.Test(10000, 100) },
                    new GraphModel {Title = "K400", Id = "K400", List = kmin.Test(10000, 400) }
                }
            });
        }
    }
}