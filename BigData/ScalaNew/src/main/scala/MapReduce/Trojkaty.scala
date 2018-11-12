package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}

object Trojkaty {

  def mapT(a: (Int, Int, Int)): Seq[(Int,(Int,Int,Int))] = {
    Seq(((100 * a._1 + a._2), (a._1, a._2, a._3)),((100*a._2+a._3), (a._1, a._2, a._3)))
  }

  def shuffT(hashmap :HashMap[Int, ListBuffer [(Int,Int,Int)]]): HashMap[Int, ListBuffer [(Int,Int,Int)]] ={
    hashmap
  }

  def reducerT(key :Int, list: ListBuffer [(Int,Int,Int)]) = {
    if(list.length > 1)
      println(key + " " + list)
  }


  def main(args:Array[String]) : Unit = {
    val input = Seq(Seq[(Int,Int,Int)]((3,1,1),(1,1,2),(2,3,4),(5,6,7)), Seq[(Int,Int,Int)]((3,4,5),(3,4,6)))
    var mapRed = new MapReduceScala[(Int,Int,Int), (Int,Int,Int), (Int,Int,Int), Int]

    var reducers = new ListBuffer[((Int) => Boolean, (Int,ListBuffer[(Int,Int,Int)]) => Unit)]()
    reducers += ((x)=> x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
