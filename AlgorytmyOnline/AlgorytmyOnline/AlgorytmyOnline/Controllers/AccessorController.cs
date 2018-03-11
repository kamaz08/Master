using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlgorytmyOnline.L1
{
    public class AccessorController : Controller
    {
        // GET: Accessor
        public ActionResult Index()
        {

            //var noself = Test(new NoSelfAssembler<CountItem>());
            //var movetofron = Test(new MoveToFronAssembler<CountItem>());
            //var trans = Test(new TransposeAssebler<CountItem>());
            //var cout = Test(new CountAssembler<CountItem>());

            var line = Test(new LinearPropability());
            var line2 = Test(new HarmonicPropability());
            var line3 = Test(new GeometricPropability());

            var model = new Models.L1Model
            {
                E1 = new Models.L1GraphModelcs
                {
                    Id = "line",
                    Data = line
                },
                E2 = new Models.L1GraphModelcs
                {
                    Id = "harmonic",
                    Data = line2
                },
                E3 = new Models.L1GraphModelcs
                {
                    Id = "geometric",
                    Data = line3
                }
            };


            return View(model);
        }


        private List<List<double>> Test(ISelfAssembler<CountItem> selfAssembler)
        {
            var result = new List<List<double>>();
            result.Add(Test(selfAssembler, new LinearPropability()));
            result.Add(Test(selfAssembler, new HarmonicPropability()));
            result.Add(Test(selfAssembler, new GeometricPropability()));

            return result;
        }

        private List<List<double>> Test(NumberPropability numberPropability)
        {
            var result = new List<List<double>>();
            result.Add(Test(new NoSelfAssembler<CountItem>(), numberPropability));
            result.Add(Test(new MoveToFronAssembler<CountItem>(), numberPropability));
            result.Add(Test(new TransposeAssebler<CountItem>(), numberPropability));
            result.Add(Test(new CountAssembler<CountItem>(), numberPropability));

            return result;
        }



        private List<double> Test(ISelfAssembler<CountItem> selfAssembler, NumberPropability numberPropability)
        {
            var test = new AccessorTest(selfAssembler, numberPropability);
            var result = new List<double>();

            result.Add(test.Test(100, 10) / 100.0);
            result.Add(test.Test(500, 10) / 500.0);
            result.Add(test.Test(1000, 10) / 1000.0);
            result.Add(test.Test(5000, 10) / 5000.0);
            result.Add(test.Test(10000, 10) / 10000.0);
            result.Add(test.Test(50000, 10) / 50000.0);
            result.Add(test.Test(100000, 10) / 100000.0);
            return result;
        }
    }
}