using Krypto.L1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Krypto.Controllers
{
    public class L1Controller : Controller
    {
        public ActionResult Index()
        {
            var lcg = new LcgGenerator(12, 10, 43);
            var lcgBroke = new LcgBrokeGenerator();
            lcgBroke.Broke(lcg);

            

            return View();
        }
    }
}