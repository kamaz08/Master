package book

import scala.io.Source
import java.io._

import scala.collection.mutable.ListBuffer

object L1Book {
  val stop = "a, aby, ach, acz, aczkolwiek, aj, albo, ale, ależ, ani, aż, bardziej, bardzo, bo, bowiem, by, byli, bynajmniej, być, był, była, było, były, będzie, będą, cali, cała, cały, ci, cię, ciebie, co, cokolwiek, coś, czasami, czasem, czemu, czy, czyli, daleko, dla, dlaczego, dlatego, do, dobrze, dokąd, dość, dużo, dwa, dwaj, dwie, dwoje, dziś, dzisiaj, gdy, gdyby, gdyż, gdzie, gdziekolwiek, gdzieś, i, ich, ile, im, inna, inne, inny, innych, iż, ja, ją, jak, jaka, jakaś, jakby, jaki, jakichś, jakie, jakiś, jakiż, jakkolwiek, jako, jakoś, je, jeden, jedna, jedno, jednak, jednakże, jego, jej, jemu, jest, jestem, jeszcze, jeśli, jeżeli, już, ją, każdy, kiedy, kilka, kimś, kto, ktokolwiek, ktoś, która, które, którego, której, który, których, którym, którzy, ku, lat, lecz, lub, ma, mają, mało, mam, mi, mimo, między, mną, mnie, mogą, moi, moim, moja, moje, może, możliwe, można, mój, mu, musi, my, na, nad, nam, nami, nas, nasi, nasz, nasza, nasze, naszego, naszych, natomiast, natychmiast, nawet, nią, nic, nich, nie, niech, niego, niej, niemu, nigdy, nim, nimi, niż, no, o, obok, od, około, on, ona, one, oni, ono, oraz, oto, owszem, pan, pana, pani, po, pod, podczas, pomimo, ponad, ponieważ, powinien, powinna, powinni, powinno, poza, prawie, przecież, przed, przede, przedtem, przez, przy, roku, również, sama, są, się, skąd, sobie, sobą, sposób, swoje, ta, tak, taka, taki, takie, także, tam, te, tego, tej, temu, ten, teraz, też, to, tobą, tobie, toteż, trzeba, tu, tutaj, twoi, twoim, twoja, twoje, twym, twój, ty, tych, tylko, tym, u, w, wam, wami, was, wasz, wasza, wasze, we, według, wiele, wielu, więc, więcej, wszyscy, wszystkich, wszystkie, wszystkim, wszystko, wtedy, wy, właśnie, z, za, zapewne, zawsze, ze, zł, znowu, znów, został, żaden, żadna, żadne, żadnych, że, żeby".split(",\\s+").toList


  def Z5(filename: String) = {
    val source = "D:\\Git\\Master\\BigData\\L1\\" + filename + ".txt";
    val book = Source
      .fromFile(source, "UTF-8").mkString
      .toLowerCase
      .split("[;:,.– \"\']*\\s+")
      .filterNot(stop.contains(_))
      .groupBy(x => x)
      .mapValues(x => x.length)
      .toSeq
      .sortWith((x, y) => x._2 > y._2)
      .toList

    val pw = new PrintWriter(new File(filename + ".wcld"))
    pw.write(book.map(x => x._2 + " " + x._1 + "\n").mkString)
    pw.close

    println(book)
  }

  def ToList(filename: String): List[(String, Int)] = {
    val source = "D:\\Git\\Master\\BigData\\L1\\" + filename + ".txt";
    Source
      .fromFile(source, "UTF-8").mkString
      .toLowerCase
      .split("[;:,.– \"\']*\\s+")
      .filterNot(stop.contains(_))
      .groupBy(x => x)
      .mapValues(x => x.length)
      .toSeq
      .sortWith((x, y) => x._2 > y._2)
      .toList
  }

  def tf(list: List[(String, Int)]): List[(String, Int, Double)] = {
    val suma = (list.map(x => x._2).sum).toDouble;
    list
      .map(x => (x._1, x._2, (x._2 / suma)))
      .toList;
  }

  def idf(all: List[(String, Int)], roz: List[(List[(String, Int, Double)])]): List[(String, Int, Double)] = {
    var countRoz = roz.length.toDouble;
    all.map(x => (x._1, x._2, Math.log(countRoz / roz.count(y => y.exists(z => z._1 == x._1)))));
  }

  def rozdzial(out: String, rozdzial: List[(String, Int, Double)], idf: List[(String, Int, Double)]) = {
    var book = rozdzial
      .map(x => (x._1, x._2 * (idf.find(_._1 == x._1)).get._3))
      .filterNot(x => x._2 < 1)
      .map(x => (x._1, x._2.toInt))
      .sortWith((x, y) => x._2 > y._2)
      .toList;

    val pw = new PrintWriter(new File("Z6\\" + out + "_tfidf.wcld"))
    pw.write(book.map(x => x._2 + " " + x._1 + "\n").mkString)
    pw.close
  }

  def Z6() = {
    val r1 = tf(ToList("rozdzial1"));
    var r2 = tf(ToList("rozdzial2"));
    var r3 = tf(ToList("rozdzial3"));
    var r4 = tf(ToList("rozdzial4"));
    var r5 = tf(ToList("rozdzial5"));
    var r6 = tf(ToList("rozdzial6"));
    var r7 = tf(ToList("rozdzial7"));
    var r8 = tf(ToList("rozdzial8"));
    var r9 = tf(ToList("rozdzial9"));
    var r10 = tf(ToList("rozdzial10"));
    var r11 = tf(ToList("rozdzial11"));

    var all = ToList("ksiega1");
    var tfall = tf(all);
    var listr = List(r1, r2, r3, r4, r5, r6, r7, r8, r9, r10, r11);
    var idfall = idf(all, listr);

    println(idfall);

    rozdzial("rozdzial1", r1, idfall);
    rozdzial("rozdzial2", r2, idfall);
    rozdzial("rozdzial3", r3, idfall);
    rozdzial("rozdzial4", r4, idfall);
    rozdzial("rozdzial5", r5, idfall);
    rozdzial("rozdzial6", r6, idfall);
    rozdzial("rozdzial7", r7, idfall);
    rozdzial("rozdzial8", r8, idfall);
    rozdzial("rozdzial9", r9, idfall);
    rozdzial("rozdzial10", r10, idfall);
    rozdzial("rozdzial11", r11, idfall);
    rozdzial("all", tfall, idfall);


  }

  def mapper(filename: String): Map[Int, ListBuffer[String]] = {
      var result = Map(0 -> ListBuffer[String](), 1 -> ListBuffer[String]());

      def emit(keyVal: Int, value: String): Unit = {
        result.get(keyVal).get += value;
      };


      val source = "D:\\Git\\Master\\BigData\\L1\\" + filename + ".txt";
      Source
        .fromFile(source, "UTF-8").mkString
        .toLowerCase
        .split("[;:,.– \"\']*\\s+")
        .filterNot(stop.contains(_))
        .map(x => (x.charAt(0) % 2, x))
        .toList
        .foreach(x => emit(x._1, x._2));
      print(result);

      result;
    }

  def reducer(list :ListBuffer[String]) = {

    def emit(key: Int, value: String)={
      println(key + " " + value);
    }

    list
      .groupBy(x => x)
      .mapValues(x => x.length)
      .toSeq
      .sortWith((x, y) => x._2 > y._2)
      .foreach(x=> emit(x._2, x._1))
  }

    def concat(l1 :Map[Int, ListBuffer[String]], l2: Map[Int, ListBuffer[String]]) ={
      l2.get(0).get.foreach(x =>l1.get(0).get += x);
      l2.get(0).get.foreach(x =>l1.get(0).get += x);
      l1;
    }

    def z14() = {
      var map1 = concat(concat(mapper("rozdzial1"), mapper("rozdzial2")), mapper("rozdzial3"));
      reducer(map1.get(0).get);
      reducer(map1.get(1).get);
    }

    def main(args: Array[String]) = {
      z14();

    }
}
