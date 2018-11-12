package MapReduce

import scala.collection.mutable.{HashMap, ListBuffer}

class MapReduceScala[Input,OutPutMapper, OutPutShuffer ,Key] {

  def Map(input : Seq[Input], func: (Input) => Seq[(Key, OutPutMapper)]) : HashMap[Key, ListBuffer [OutPutMapper]] = {
    var result = new HashMap[Key, ListBuffer[OutPutMapper]]
    input.foreach(in => {
      var elements = func(in)
      elements.foreach(el =>{
        if(result.contains(el._1))
          result(el._1) += el._2
        else
          result+= (el._1 -> ListBuffer{el._2})
        result
      })
    })
    result
  }

  def Shuffle(input :HashMap[Key, ListBuffer [OutPutMapper]], func: (HashMap[Key, ListBuffer [OutPutMapper]]) => HashMap[Key, ListBuffer [OutPutShuffer]]) : HashMap[Key, ListBuffer [OutPutShuffer]] = {
    func(input);
  }

  def Reducer(input: (Key,ListBuffer[OutPutShuffer]), func :(Key, ListBuffer[OutPutShuffer]) => Unit )= {
      func(input._1, input._2)
  }

  def Run(input: Seq[Seq[Input]],
          maperfunc :(Input) => Seq[(Key,OutPutMapper)],
          shuffleFunc: (HashMap[Key, ListBuffer [OutPutMapper]]) => HashMap[Key, ListBuffer [OutPutShuffer]],
          reducer : ListBuffer[((Key) => Boolean, (Key, ListBuffer[OutPutShuffer]) => Unit)]
         ): Unit
  ={
    var hashRes = new HashMap[Key, ListBuffer [OutPutShuffer]]
    input.foreach(in => {
      Shuffle(Map(in, maperfunc), shuffleFunc).foreach(res => {

        if(hashRes.contains(res._1))
          hashRes(res._1) ++= res._2
        else
          hashRes += (res._1 -> res._2)
        hashRes
      })
    })


    hashRes.foreach(hash => {
      Reducer(hash, reducer.find(red=> red._1(hash._1)).get._2)
    })
  }



}


