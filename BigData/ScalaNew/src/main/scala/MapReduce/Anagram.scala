package MapReduce
import scala.collection.mutable.{HashMap, ListBuffer}

object Anagram {
    def mapT(a: String): Seq[(String, String)] = {
      var result = a.toCharArray().sortWith((x,y) => x < y).mkString("")
      Seq((result, a))
    }

    def shuffT(hashmap: HashMap[String, ListBuffer[String]]): HashMap[String, ListBuffer[String]] = {
      hashmap
    }

    def reducerT(key: String, list: ListBuffer[String]) = {
      if(list.length > 1)
        println("Anagrams " + list)
    }

    def main(args: Array[String]): Unit = {

      val input = Seq(Seq("aaaabbbb", "abababab", "asedf"), Seq("fdesa", "asdfasdfasdf", "qweqwe"))
      var mapRed = new MapReduceScala[String, String, String, String]

      var reducers = new ListBuffer[((String) => Boolean, (String, ListBuffer[String]) => Unit)]()
      reducers += ((x) => x == x, reducerT)

      mapRed.Run(input, mapT, shuffT, reducers);
    }
}


