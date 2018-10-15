package main.scala

import Complex.Complex

object L1 {
  def gcd(x:Int, y:Int): Int = if (y == 0) x.abs else gcd(y, x%y)
  def lcm(x:Int, y:Int): Int = {
    val _gcd = gcd(x, y)
    if (_gcd == 0) 0 else (x * y).abs / _gcd
  }
  def tau(x:Int): List[Int] = (1 to (x / 2)).filter(y => x%y == 0).toList
  def sigma(x:Int): Int = tau(x).sum
  def sigma(x:Int, y:Int): Int = tau(x).map( z=> Math.pow(z,y).toInt).sum

  def euler(x: Int): Int = (1 to x).count(y=> gcd(x, y) == 1)
  def eulerProof(x: Int): Int = (1 to x).filter(z => x%z == 0).map(z => euler(z)).sum

  def isPrime(n: Int): Boolean = {
    if (n < 2) false
    else if (n == 2) true
    else !(2 to n / 2).exists(x => n % x == 0)
  }


  def main(args:Array[String]) = {
    println("GCD")
    println(gcd(25, -15))

    println("LCM")
    println(lcm(25, -15))

    println("tau")
    println(tau(15))

    println("sigma")
    println(sigma(15))
    println(sigma(15, 2))

    println("\nEuler")
    println(euler(1))
    println(euler(15))
    println(euler(31))
    println("check")
    println(Range(1, 101).filter(x => 100 % x == 0).map(x => euler(x)).sum)
    println(eulerProof(31))
    println(eulerProof(12))

    println("\nPierwsza")
    println(isPrime(31))
    println(isPrime(8))
    println("Pierwsza check")
    println(Range(1,1000).filter(isPrime).length == 168)

    println("Complex")
    val c1 = Complex(-10,-10)
    val c2 = Complex(2,6)

    println("Complex c1: " + c1 + " c2: " +c2)
    println(c1 + c2)
    println(c1 - c2)
    println(c1 * c2)
    println(c1 / c2)

  }
}
