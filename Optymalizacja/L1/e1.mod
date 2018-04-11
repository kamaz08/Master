/* 
Metody optymalizacji 
Lista 1
Kamil Sikorski 221481 

Ax = b
m - wielkosc macierzy 
J - przechodzenie po macierzy
b - wektor b
A - macierz A - hilberta
x - wektor x
*/

param m;
set J := {1..m};

param b{j in J} := sum{i in J}(1/(i+j-1)); 
param A{j in J, i in J} := 1/(i+j-1);
var x{i in J} >= 0;

minimize suma : sum{j in J}(x[j]*b[j]);

s.t. oblicz_x{j in J} :  sum{k in J}( A[j,k] * x[k]) = b[j];
solve;
printf "\n";
display x;
printf "\nblad wzgledny: %.32f\n",sqrt(sum{k in J}(abs(1.0-x[k])**2))/sqrt(sum{k in J}(1.0));

data;
param m:=8;
end;
