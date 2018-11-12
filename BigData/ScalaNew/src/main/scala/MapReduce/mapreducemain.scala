package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer};

object mapreducemain {


  def srRed(a :Int, listBuffer: ListBuffer[(Int, Int)]) = {
    var sum = listBuffer.map(x=>x._1).sum
    var count = listBuffer.map(x=>x._2).sum.toDouble
    println("Srednia " + (sum/count))
  }

  def distinctRed(a: Int,listBuffer: ListBuffer[Int]) = {
    println("distinct  " + listBuffer.distinct)
    println("distinct count " + listBuffer.distinct.length)
  }

  def main(args:Array[String]) : Unit = {
    var mapRed = new MapReduceScala[Int, Int, Int, Int]

    val input = Seq(Seq[Int](1,2,3,45,6,2,1,0), Seq[Int](10,20,30,40,-6,2,1,0))

    def minRed(a: Int, listBuffer: ListBuffer[Int]) = {
      println("Min " + listBuffer.min + " Max ", listBuffer.max)
    }
    var reducers = new ListBuffer[((Int) => Boolean, (Int,ListBuffer[Int]) => Unit)]()
    reducers += (((a) => (a==1), minRed))


    mapRed.Run(input, (a) => Seq((1,a)), (in : HashMap[Int, ListBuffer [Int]]) => {
        var suffres = new HashMap[Int, ListBuffer[Int]]();
        in.foreach(h => {
          var min = h._2.minBy(x=>x)
          var max = h._2.max
          suffres += (h._1 -> ListBuffer(min,max))
        })
        suffres
      },
      reducers
      )

    var reducers2 = new ListBuffer[((Int) => Boolean, ((Int,ListBuffer[(Int,Int)]) => Unit))]()
    reducers2 += ((a) => (a==1), srRed)


    var mapRed2 = new MapReduceScala[Int, Int, (Int, Int), Int]
    mapRed2.Run(input,
      (a) => Seq((1,a)),
      (in : HashMap[Int, ListBuffer [Int]]) => {
        var suffres = new HashMap[Int, ListBuffer[(Int,Int)]]();
        in.foreach(h => {
          var sum = h._2.sum
          var count = h._2.length
          suffres += (h._1 -> ListBuffer((sum,count)))
        })
      suffres
    },
      reducers2
    )

    reducers += ((a) => (a==2), distinctRed)

    mapRed.Run(input, (a) => Seq((2,a)),
      (in : HashMap[Int, ListBuffer [Int]]) => {
      var suffres = new HashMap[Int, ListBuffer[Int]]();
      in.foreach(h => {
        suffres += (h._1 -> h._2.distinct)
      })
      suffres
    },
      reducers
    )

    //println(mapRed.Shuffle(mapRed.Map(input(0),(a) => (1,a)), ))

  }




}
