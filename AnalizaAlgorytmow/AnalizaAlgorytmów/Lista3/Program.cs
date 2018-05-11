using Lista3.Dijkstra;
using Lista3.IndependentSet;

namespace Lista3
{
    class Program
    {
        static void Main(string[] args)
        {
            //Dijkstra();
            IndependentSet();
        }


        public static void Dijkstra()
        {
            var test = new DijkstraTab(9);
            var a = test.Caluculate();
        }

        public static void IndependentSet()
        {
            var graph = new Graph(50, 5);

            graph.WriteGraph();
            graph.Random();
            graph.IndependentSet();
           // graph.
        }
    }
}

// 9 - 98 ~ 25 minut
// 8 - 75 - < 1 minuta
// 7 - 55 - ;) 