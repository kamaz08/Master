package Basic

case class Complex(real:Double, imaginary:Double) {
  override def toString() : String = real + " + " + imaginary + "i"

  def + (that:Complex) : Complex = Complex(this.real + that.real, this.imaginary + that.imaginary)

  def - (that:Complex) : Complex = Complex(this.real - that.real, this.imaginary - that.imaginary)

  def * (that:Complex) : Complex = Complex(this.real * that.real - this.imaginary * that.imaginary,
    this.real * that.imaginary + that.real * this.imaginary )

  def / (that:Complex) : Complex = {
    val temp = that.real * that.real + that.imaginary * that.imaginary
    Complex((this.real * that.real + this.imaginary * that.imaginary) / temp,
      (this.imaginary * that.real - this.real * that.imaginary) / temp )
  }



}
