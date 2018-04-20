using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Lista2.Graph;
using Lista2.Hash;
using Lista2.List;
using Lista2.Model;

namespace Lista2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var pr = new Lista3cs();
            //pr.TestSHA();
            pr.Campare();
           // pr.TestMD5();
           // pr.TestDek();
        }

       
    }
}
