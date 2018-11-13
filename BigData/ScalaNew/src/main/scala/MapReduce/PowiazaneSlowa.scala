package MapReduce

import book.L1Book.stop

import scala.collection.mutable.{HashMap, ListBuffer}
import scala.io.Source


object PowiazaneSlowa {
  def mapT(filename: String): Seq[(String, String)] = {
    val source = "Dane\\" + filename + ".txt";
    var old = "";
    Source
      .fromFile(source, "UTF-8").mkString
      .toLowerCase
      .split("[;:,.– \"\']*\\s+")
      .filterNot(stop.contains(_))
      .map(x=> {
        var result = Seq((old,x), (x,old))
        old = x
        result
      })
      .flatMap(x=>x)
  }

  def shuffT(hashmap: HashMap[String, ListBuffer[String]]): HashMap[String, ListBuffer[String]] = {
    hashmap
  }

  def reducerT(key: String, list: ListBuffer[String]) = {
    var elements = list.groupBy(x=>x).mapValues(x => x.length).toSeq.sortWith((x, y) => x._2 > y._2).take(5)
    if(elements.map(x=>x._2).max > 1)
      println(key + ": " + list.groupBy(x=>x).mapValues(x => x.length).toSeq.sortWith((x, y) => x._2 > y._2).take(5))
  }

  def main(args: Array[String]): Unit = {

    val input = Seq(Seq("rozdzial1"), Seq("rozdzial2"))
    var mapRed = new MapReduceScala[String, String, String, String]

    var reducers = new ListBuffer[((String) => Boolean, (String, ListBuffer[String]) => Unit)]()
    reducers += ((x) => x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
