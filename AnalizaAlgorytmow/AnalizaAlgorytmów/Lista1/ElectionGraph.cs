using Lista1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lista1
{
    public class ElectionGraph
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Item> GraphData { get; set; }
        public double E { get; set; }
        public double Var { get; set; }
        public double Pr { get; set; }
    }
}