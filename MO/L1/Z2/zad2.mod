/*
Kamil Sikorski
221481
*/

/*typy danych */
set TypDzwigu;
set Miasto;

/*wspoczynniki danych*/
param odleglosc{i in Miasto, j in Miasto} >= 0;
param niedobor{i in Miasto, t in TypDzwigu} >= 0;
param nadmiar{i in Miasto, t in TypDzwigu} >= 0;
param wspolczynnikPrzejazdu{t in TypDzwigu} >= 0;
/*zastapywanie dzwigow*/
param zastapienie{t1 in TypDzwigu, t2 in TypDzwigu} >= 0;

/*zmienna ile dzwigow i jakiego typu t1 pojechalo z miasta i do miasta j  by uzupelnic typ t2*/
var przejazd{i in Miasto, j in Miasto, t1 in TypDzwigu, t2 in TypDzwigu} >= 0;


/* minimalizacja kosztu przejazdu*/
minimize cost: sum{i in Miasto, j in Miasto, t1 in TypDzwigu, t2 in TypDzwigu} 
    przejazd[i, j, t1, t2]*wspolczynnikPrzejazdu[t1]*odleglosc[i, j];

/*ograniczenia*/
s.t. rozwiazNadmiar{i in Miasto, t1 in TypDzwigu}:
	nadmiar[i, t1] - sum{j in Miasto, t2 in TypDzwigu} zastapienie[t1, t2]*przejazd[i, j, t1, t2] = 0;

s.t. rozwiazNiedobor{i in Miasto, t2 in TypDzwigu}: 
	niedobor[i, t2] - sum{j in Miasto, t1 in TypDzwigu} zastapienie[t1, t2]*przejazd[j, i, t1, t2] = 0;

solve;

for{i in Miasto, j in Miasto, t1 in TypDzwigu, t2 in TypDzwigu}{
	for{{0}:przejazd[i, j, t1, t2] > 0}{
			printf "%s -> %s %s %s %d\n", i, j, t1, t2, przejazd[i, j, t1, t2];
		}
}
/*
for{i in Miasto, j in Miasto, t1 in TypDzwigu, t2 in TypDzwigu}{
	for{{0}:przejazd[i, j, t1, t2] > 0}{
			printf "\\hline \n %s & %s & %s & %s & %d \\\\\n", i, j, t1, t2, przejazd[i, j, t1, t2];
		}
}
*/
printf 'Minimum Cost = %.2f\n', cost;

end;