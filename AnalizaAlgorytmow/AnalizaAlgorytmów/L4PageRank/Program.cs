﻿using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.DataVisualization.Charting;
using Lista2.Graph;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Complex;

namespace L4PageRank
{
    public class Graph
    {
        public double X { get; set; }
        public double Step { get; set; }
        public double Hold { get; set; }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // Zadanie1();
            //Zadanie2();
            //Zadanie3();
            Zadanie4();

        }
        // 5 13 21

        public static void Zadanie4cel(double mi, double la, int test)
        {
            var algo = new ProstaKolejka();
            var results = new List<List<Graph>>();

            for(int i =0; i<test; i++)
            {
                results.Add(new List<Graph>());
                var dane = algo.Test(mi, la);
                
                for(int j = 0; j < dane.Count; j++)
                {
                    results[i].Add(new Graph
                    {
                        X = j,
                        Step = dane[j].Steps.Count == 0 ? 10000 : dane[j].Steps.Average(),
                        Hold = dane[j].Count
                    });
                }
            }

            var max = results.Max(x => x.Count);

            var res = new List<Graph>();
            for (int i = 0; i < max; i++)
            {
                res.Add(new Graph
                {
                    X = i
                });
                for (int j = 0; j < test; j++)
                {
                    if (results[j].Count > i)
                    {
                        res[i].Step += results[j][i].Step;
                        res[i].Hold += results[j][i].Hold;
                    }
                }
                res[i].Step /= test;
                res[i].Hold /= test;
            }
            var graph = new GraphGenerator(600, 800);
            //graph.AddSeries($"series{0}", SeriesChartType.Line, Enumerable.Range(0, res.Count).Select(x => (double)x).ToList(), res.Select(x=>x.Hold).ToList(), Color.Red);
            graph.AddSeries($"series{1}", SeriesChartType.Line, Enumerable.Range(0, res.Count).Select(x => (double)x).ToList(), res.Select(x => x.Step).ToList(), Color.Green);
            graph.SaveGraph($"prosta kolejka mi{mi} lambda{la}");
        }



        public static void Zadanie4()
        {
            var test = 1000;
            //Zadanie4cel(0.333, 0.667, test);
            //Zadanie4cel(0.5, 0.5, test);
            Zadanie4cel(0.667, 0.333, test);

            /*
            var algo = new ProstaKolejka();
            var steps0 = new List<double>();
            var steps1 = new List<double>();
            var steps2 = new List<double>();

            var hold0 = new List<double>();
            var hold1 = new List<double>();
            var hold2 = new List<double>();
            var t1 = algo.Test(0.2, 0.1);
            var t2 = algo.Test(0.2, 0.2);
            var t3 = algo.Test(0.1, 0.2);

            steps0 = t1.Select(x => (double)x.Steps.Average()).ToList();
            steps1 = t2.Select(x => (double)x.Steps.Average()).ToList();
            steps2 = t3.Select(x => (double)x.Steps.Average()).ToList();

            hold0 = t1.Select(x => (double)x.Count).ToList();
            hold1 = t2.Select(x => (double)x.Count).ToList();
            hold2 = t3.Select(x => (double)x.Count).ToList();



            var graph = new GraphGenerator(600, 800);
            graph.AddSeries($"series{0}", SeriesChartType.Line, Enumerable.Range(0, hold0.Count).Select(x => (double)x).ToList(), hold0, Color.Red);
            graph.AddSeries($"series{1}", SeriesChartType.Line, Enumerable.Range(0, steps0.Count).Select(x => (double)x).ToList(), steps0, Color.Green);
            graph.SaveGraph($"zadanko {0}");

            graph = new GraphGenerator(600, 800);
            graph.AddSeries($"series{0}", SeriesChartType.Line, Enumerable.Range(0, hold1.Count).Select(x => (double)x).ToList(), hold1, Color.Red);
            graph.AddSeries($"series{1}", SeriesChartType.Line, Enumerable.Range(0, steps1.Count).Select(x => (double)x).ToList(), steps1, Color.Green);
            graph.SaveGraph($"zadanko {1}");

            graph = new GraphGenerator(600, 800);
            graph.AddSeries($"series{0}", SeriesChartType.Line, Enumerable.Range(0, hold2.Count).Select(x => (double)x).ToList(), hold2, Color.Red);
            graph.AddSeries($"series{1}", SeriesChartType.Line, Enumerable.Range(0, steps2.Count).Select(x => (double)x).ToList(), steps2, Color.Green);
            graph.SaveGraph($"zadanko {2}");

            //Console.WriteLine($@"Srednia ilosc krokow 0.2 0.1 {steps0.Average()}");
            //Console.WriteLine($@"Srednia ilosc krokow 0.2 0.2 {steps1.Average()}");
            //Console.WriteLine($@"Srednia ilosc krokow 0.1 0.2 {steps2.Average()}");

            //Console.WriteLine($@"Srednie przebywanie w jednym stanie  0.2 0.1 {hold0.Average()}");
            //Console.WriteLine($@"Srednie przebywanie w jednym stanie  0.2 0.2 {hold1.Average()}");
            //Console.WriteLine($@"Srednie przebywanie w jednym stanie  0.1 0.2 {hold2.Average()}");
            */

        }

        public static void Zadanie3()
        {
            var n = 5;
            var Ng = Matrix<double>.Build.Dense(n, n, 0.0);


            Ng[0, 1] = Ng[0, 2] = 0.5;
            Ng[2, 1] = Ng[2, 3] = Ng[2, 4] = 1.0 / 3.0;
            Ng[1, 3] = Ng[3, 0] = 1.0;
            Ng[4, 0] = Ng[4, 1] = Ng[4, 2] = Ng[4, 3] = Ng[4, 4] = 1.0 / n;

            for (int i = 0; i < Ng.RowCount; i++)
            {
                for (int j = 0; j < Ng.RowCount; j++)
                {
                    Console.Write($"{Math.Round(Ng[i, j], 4)}\t");
                }
                Console.WriteLine();
            }


            var pi = Vector<double>.Build.Dense(n, 1.0 / n);
            //var tempPi = pi;
            var alfa = new[] {0.0, 0.25, 0.5, 0.75, 0.85, 1.0}.ToList();
            Console.WriteLine("\n\nZadanie3");
            alfa.ForEach(a =>
            {
                var l0 = new List<double>();
                var l1 = new List<double>();
                var l2 = new List<double>();
                var l3 = new List<double>();
                var l4 = new List<double>();
                Console.WriteLine($"Alfa = {a}");
                var mac = (((1.0 - a) * Ng) + (a * (1.0 / n)));
                var temp = mac.Clone();

                for (int k = 0; k < 25; k++)
                { 
                    var tempPi = temp.Transpose() * pi;
                    l0.Add(tempPi[0]);
                    l1.Add(tempPi[1]);
                    l2.Add(tempPi[2]);
                    l3.Add(tempPi[3]);
                    l4.Add(tempPi[4]);
                    temp = mac * temp;
                }

                var graph = new GraphGenerator(600, 800);
                graph.AddSeries($"series{0}", SeriesChartType.Line, Enumerable.Range(1,25).Select(x=>(double)x).ToList(), l0, Color.Red );
                graph.AddSeries($"series{1}", SeriesChartType.Line, Enumerable.Range(1, 25).Select(x => (double)x).ToList(), l1, Color.Yellow);
                graph.AddSeries($"series{2}", SeriesChartType.Line, Enumerable.Range(1, 25).Select(x => (double)x).ToList(), l2, Color.Green);
                graph.AddSeries($"series{3}", SeriesChartType.Line, Enumerable.Range(1, 25).Select(x => (double)x).ToList(), l3, Color.Orange);
                graph.AddSeries($"series{4}", SeriesChartType.Line, Enumerable.Range(1, 25).Select(x => (double)x).ToList(), l4, Color.Black);
                graph.SaveGraph($"zadanko alpha{a}");
            });
        }


        public static void Zadanie2()
        {
            var n = 4;
            var Pg = Matrix<double>.Build.Dense(n, n, 0);

            Pg[0, 0] = 0.0;
            Pg[0, 1] = 0.3;
            Pg[0, 2] = 0.1;
            Pg[0, 3] = 0.6;

            Pg[1, 0] = 0.1;
            Pg[1, 1] = 0.1;
            Pg[1, 2] = 0.7;
            Pg[1, 3] = 0.1;

            Pg[2, 0] = 0.1;
            Pg[2, 1] = 0.7;
            Pg[2, 2] = 0.1;
            Pg[2, 3] = 0.1;

            Pg[3, 0] = 0.9;
            Pg[3, 1] = 0.1;
            Pg[3, 2] = 0.0;
            Pg[3, 3] = 0.0;

            for (int i1 = 0; i1 < Pg.RowCount; i1++)
            {
                for (int j1 = 0; j1 < Pg.RowCount; j1++)
                {
                    Console.Write($"{Math.Round(Pg[i1, j1], 4)}\t");
                }
                Console.WriteLine();
            }


            var G = Pg;
            
            for (int i = 0; i < 10; i++)
            {
                G = G * G;
            }
            /*
            Console.WriteLine("\n\n 2");
            Console.WriteLine("Rozkład");
            for (int i = 0; i < G.RowCount; i++)
            {
                for (int j = 0; j < G.RowCount; j++)
                {
                    Console.Write($"{Math.Round(G[i, j], 4)}\t");
                }
                Console.WriteLine();
            }

            var G2 = Pg;
            for (int i = 0; i < 5; i++)
            {
                G2 = G2 * G2;
            }
            Console.WriteLine($"0 -> 3 po 32 krokach = {G2[0,3]}");
            G2 = Pg;
            for (int i = 0; i < 7; i++)
            {
                G2 = G2 * G2;
            }
            Console.WriteLine($"x -> 3 po 128 krokach = {(G2[0, 3] +G2[1, 3] + G2[2,3] + G2[3,3]) / 4.0}");
            */
            var G2 = Pg;
            var poczatkowe = Vector<double>.Build.Dense(n, 0.0);
            poczatkowe[0] = 1.0;
            var epss = new[] {0.1, 0.01, 0.001}.ToList();
            epss.ForEach(eps =>
            {
                G2 = Pg;
                bool cont = true;
                for (int i = 2; cont; i++)
                {
                    G2 = G2 * Pg;
                    var G3 =  poczatkowe * G2;
                    for (int j = 0; j < 4; j++)
                    {
                        cont = false;
                        if (Math.Abs(G[0, j] - G3[j]) > eps)
                            cont = true;
                    }

                    if (!cont)
                        Console.WriteLine($" eps {eps} = {i+1} ");
                }

            });
            



        }

        public static void Zadanie1()
        {
            var n = 6;
            var alfa = new[] { 0.0, 0.15, 0.5, 1.0 }.ToList();
            var Pg = Matrix<double>.Build.Dense(n, n, 0);
            var Jn = Matrix<double>.Build.Dense(n, n, 1);
            //Pg.CoerceZero((double a)=>true);
            Pg[0, 0] = Pg[2, 0] = Pg[4, 3] = Pg[5, 2] = 1;
            Pg[1, 2] = Pg[1, 4] = Pg[3, 1] = Pg[3, 4] = 0.5;

            alfa.ForEach(a =>
            {
                Console.WriteLine($"Liczenie dla {a}");
                Calculate(Pg, Jn, a);
                Console.WriteLine();
            });

            Pg[1, 2] = 0;
            Pg[1, 4] = 1;

            alfa.ForEach(a =>
            {
                Console.WriteLine($"Liczenie dla {a}");
                Calculate(Pg, Jn, a);
                Console.WriteLine();
            });
        }

        public static void Calculate(Matrix<double> Pg, Matrix<double> Jn, double alfa)
        {
            var G = (1.0 - alfa) * Pg + alfa * (1.0 / Pg.RowCount) * Jn;
            for (int i = 0; i < 10; i++)
            {
                G = G * G;
            }
            for (int i = 0; i < G.RowCount; i++)
            {
                for (int j = 0; j < G.RowCount; j++)
                {
                    Console.Write($"{Math.Round(G[i, j], 4)}\t");
                }
                Console.WriteLine();
            }
        }
    }
}
