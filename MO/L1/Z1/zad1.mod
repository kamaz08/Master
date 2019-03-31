/*
Kamil Sikorski
221481
 */

/*Rozmiar macierzy*/
param n > 0;

/* zbiór indeksów kolumn i wierszy */
set Range:= 1..n;

/*wektor reprezentujący kolumny b i c*/
param b_c{i in Range}:= sum{j in Range} 1/(i+j-1);

/*zmienna do rozwiązania układu*/
var x{i in Range} >= 0;

minimize gain : sum{i in Range} b_c[i]*x[i];

/*Rozwiazanie macierzy Hilberta*/
s.t. hilbert{i in Range}: sum{j in Range} (1/(i+j-1))*x[j] = b_c[i];

solve;

for{i in Range}{
		printf "x[%d] = %.10f\n", i, x[i];
}

display 'blad wzgledny: ', sqrt(sum{i in Range}((1-x[i])*(1-x[i])))/sqrt(n);

end;
