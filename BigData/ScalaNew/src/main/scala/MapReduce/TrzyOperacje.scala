package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}


object TrzyOperacje {
  def mapT(a: (Int,Int,(Int) => Int)): Seq[(Int,(Int, Int))] = {
    Seq((a._2, (a._1, a._3(a._2))))
  }

  def shuffT(hashmap: HashMap[Int, ListBuffer[(Int,Int)]]): HashMap[Int, ListBuffer[(Int,Int)]] = {
    hashmap
  }

  def reducerT(key: Int, list: ListBuffer[(Int,Int)]) = {
    println( " FuG( " + key + " ) = " +list.map(x=>x._2).max)
    println( " FnG( " + key + " ) = " +list.map(x=>x._2).min)
    var roz = (list.find(x=>x._1 == 1).get._2 -  list.find(x=>x._1 == 2).get._2)
    if (roz < 0) roz = 0
    println( " F\\G( " + key + " ) = " + roz)
    println()
  }

  def main(args: Array[String]): Unit = {

    val input = Seq(
      Seq((1,1,(a: Int)=>a*a + 10), (1,2,(a: Int)=>a*a+10) , (1,2,(a: Int)=>a*a+10), (1,3,(a: Int)=>a*a+10), (1,4,(a: Int)=>a*a+10)),
      Seq((2,1,(a: Int)=>a*a*a), (2,2,(a: Int)=>a*a*a) , (2,2,(a: Int)=>a*a*a), (2,3,(a: Int)=>a*a*a), (2,4,(a: Int)=>a*a*a)))
    var mapRed = new MapReduceScala[(Int,Int,(Int) => Int), (Int,Int), (Int,Int), Int]

    var reducers = new ListBuffer[((Int) => Boolean, (Int, ListBuffer[(Int,Int)]) => Unit)]()
    reducers += ((x) => x == x, reducerT)

    mapRed.Run(input, mapT, shuffT, reducers);
  }
}
