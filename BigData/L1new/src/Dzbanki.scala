package Dzbanki

import scala.collection.mutable.HashSet;
import scala.collection.mutable.ListBuffer;

case class Dzbanki() {
  var hashes = HashSet[Int]();


  def Start(d8 : Int, d5 : Int, d3 : Int, listaruchow: ListBuffer[Int]): Unit ={
    if(d8 == 4 && d5 == 4){
      println(listaruchow);
    }else {
      hashes += d8 * 100 + d5 * 10 + d3;

      if (d8 != 0) {
        {
          var t5 = Math.min((d5 + d8), 5);
          var t8 = d8 - (t5 - d5);
          var thash = t8 * 100 + t5 * 10 + d3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(t8, t5, d3, tlist);
            }
        }
        {
          var t3 = Math.min((d3 + d8), 3);
          var t8 = d8 - (t3 - d3);
          var thash = t8 * 100 + d5 * 10 + t3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(t8, d5, t3, tlist);
          }
        }
      }
      if (d5 != 0) {
        {
          var t8 = Math.min((d8 + d5),8);
          var t5 = d5 - (t8 - d8);
          var thash = t8 * 100 + t5 * 10 + d3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(t8, t5, d3, tlist);
          }
        }
        {
          var t3 = Math.min((d3 + d5), 3);
          var t5 = d5 - (t3 - d3);
          var thash = d8 * 100 + t5 * 10 + t3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(d8, t5, t3, tlist);
          }
        }
      }
      if (d3 != 0) {
        {
          var t8 = Math.min((d8 + d3), 8);
          var t3 = d3 - (t8 - d8);
          var thash = t8 * 100 + d5 * 10 + t3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(t8, d5, t3, tlist);
          }
        }
        {
          var t5 = Math.min((d5 + d3), 5);
          var t3 = d3 - (t5 - d5);
          var thash = d8 * 100 + t5 * 10 + t3;
          if (!hashes.exists(x => x == thash)) {
            var tlist = listaruchow.clone();
            tlist += thash;
            Start(d8, t5, t3, tlist);
          }
        }
      }
    }
  }

}
