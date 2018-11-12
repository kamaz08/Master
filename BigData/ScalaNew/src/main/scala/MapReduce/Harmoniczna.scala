package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}

object Harmoniczna {
  def mapT(a: Int): Seq[(Int, Int)] = {
    Seq((1, a))
  }

  def shuffT(hashmap: HashMap[Int, ListBuffer[Int]]): HashMap[Int, ListBuffer[Int]] = {
    hashmap
  }

  def reducerT(key: Int, list: ListBuffer[Int]) = {
    var n = list.length
    var devideBy = 0.0
    list.groupBy(x=>x).foreach(x=> devideBy += x._2.length / x._1.toDouble)
    println("Harmoniczna " + n / devideBy)
  }

  def main(args: Array[String]): Unit = {

    val input = Seq(Seq(1, 2, 3, 4, 5, 5, 4, 3, 2, 1), Seq(1, 2, 3))
    var mapRed = new MapReduceScala[Int, Int, Int, Int]

    var reducers = new ListBuffer[((Int) => Boolean, (Int, ListBuffer[Int]) => Unit)]()
    reducers += ((x) => x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
