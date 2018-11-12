package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}

object Graf {
  def mapT(a: (Int, Seq[Int])): Seq[(Int, Int)] = {
    a._2.map(x=> (x, a._1))
  }

  def shuffT(hashmap :HashMap[Int, ListBuffer [(Int)]]): HashMap[Int, ListBuffer [Int]] ={
    hashmap
  }

  def reducerT(key :Int, list: ListBuffer [Int]) = {
      println(key + " " + list)
  }

  def main(args:Array[String]) : Unit = {

    val input = Seq(Seq((1, Seq(3,4,5)),(2, Seq(1,3))) ,Seq((3, Seq(4,5)),(4, Seq(1,2)),(5, Seq(4,5))))
    var mapRed = new MapReduceScala[(Int, Seq[Int]), Int, Int, Int]

    var reducers = new ListBuffer[((Int) => Boolean, (Int,ListBuffer[Int]) => Unit)]()
    reducers += ((x)=> x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
