package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}

object Geometryczna {
  def mapT(a: Int): Seq[(Int, Int)] = {
    Seq((1, a))
  }

  def shuffT(hashmap: HashMap[Int, ListBuffer[Int]]): HashMap[Int, ListBuffer[(Int, Int)]] = {
    var sum = 1;
    var count = hashmap(1).length;
    hashmap(1).foreach(x => sum *= x)
    var result = new HashMap[Int, ListBuffer[(Int, Int)]]
    result += (1 -> ListBuffer {
      (count, sum)
    })
  }

  def reducerT(key: Int, list: ListBuffer[(Int, Int)]) = {
    var sum = 1;
    var count = list.map(x => x._1).sum;
    list.foreach(x => sum *= x._2)
    if (count > 0)
      println("Geometryczna " + Math.pow(sum, 1.0 / count))
    else
      println("Geo Zero")
  }

  def main(args: Array[String]): Unit = {

    val input = Seq(Seq(1, 2, 3, 4, 5, 5, 4, 3, 2, 1), Seq(1, 2, 3))
    var mapRed = new MapReduceScala[Int, Int, (Int, Int), Int]

    var reducers = new ListBuffer[((Int) => Boolean, (Int, ListBuffer[(Int, Int)]) => Unit)]()
    reducers += ((x) => x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
