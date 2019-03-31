/*
Kamil Sikorski
221481
*/

set RodzajRopy;
set PolProdukt;
set Produkt;

param KosztRopy{r in RodzajRopy} >= 0;
param KosztDestylacji >=0; 
param KosztKrakowania >=0;
param WydajnoscDestylacji{pp in PolProdukt , r in RodzajRopy} >= 0.0;
param WydajnoscKrakowania{ppto in PolProdukt, ppFrom in PolProdukt} >= 0; 
param KosztProduktuDestylacja{pp in PolProdukt, p in Produkt} >= 0;
param KosztProduktuKrakowanie{pp in PolProdukt, p in Produkt} >= 0;
param MinimumProduktu{p in Produkt} >= 0;
param ZawartoscSiarkiDestylacja{r in RodzajRopy} >= 0;
param ZawartoscSiarkiKrakowanie{r in RodzajRopy} >= 0;
param MaxSiarki >= 0;

var iloscRopy{r in RodzajRopy} >= 0;
var iloscKrakowania{r in RodzajRopy, pp in PolProdukt} >= 0;
var iloscPolProduktuNaProdukt{r in RodzajRopy, p in Produkt, pp in PolProdukt} >= 0;

/*koszty destylacji */
minimize cost: (sum{r in RodzajRopy} (KosztRopy[r] * iloscRopy[r] * KosztDestylacji) + sum{r in RodzajRopy, pp in PolProdukt }(iloscKrakowania[r,pp] * KosztKrakowania));

s.t. OgarniczenieKrakowania{r in RodzajRopy, pp in PolProdukt} 
    : iloscKrakowania[r, pp] <=  iloscRopy[r] * WydajnoscDestylacji[pp,r];

s.t. SprawdzMinimalneZapotrzebowanie{p in Produkt} : MinimumProduktu[p] <= sum{r in RodzajRopy, ppfrom in PolProdukt} 
        ((iloscRopy[r] * WydajnoscDestylacji[ppfrom, r] - iloscKrakowania[r, ppfrom]) * KosztProduktuDestylacja[ppfrom, p] 
        + iloscKrakowania[r, ppfrom] * sum{ppTo in PolProdukt } WydajnoscKrakowania[ppTo, ppfrom] * KosztProduktuKrakowanie[ppTo,p]);


/*
s.t. OgraniczenieZawartosciSiarki : MaxSiarki *
 sum{r in RodzajRopy, pp in PolProdukt} (
     ((iloscRopy[r] * WydajnoscDestylacji[pp,r] - iloscKrakowania[r,pp]) * KosztProduktuDestylacja[pp,'DomowePaliwaOlejowe']) + 
     iloscKrakowania[r,pp] * sum{ppfromkrak in PolProdukt} WydajnoscKrakowania[ppfromkrak,pp] * KosztProduktuKrakowanie[ppfromkrak,'DomowePaliwaOlejowe']) 
     >=
     sum{r in RodzajRopy, pp in PolProdukt} (
     ((iloscRopy[r] * WydajnoscDestylacji[pp,r] - iloscKrakowania[r,pp]) * KosztProduktuDestylacja[pp,'DomowePaliwaOlejowe']) * ZawartoscSiarkiDestylacja[r] + 
     iloscKrakowania[r,pp] * sum{ppfromkrak in PolProdukt} WydajnoscKrakowania[ppfromkrak,pp] * KosztProduktuKrakowanie[ppfromkrak,'DomowePaliwaOlejowe'] * ZawartoscSiarkiKrakowanie[r]); 
*/


solve;

printf "\n\n" ;
printf 'Minimum Cost = %.2f\n', cost;


for{r in RodzajRopy}{
	for{iloscRopy[r]}{
			printf "%s %f \n", r, iloscRopy[r];
		}
}

for{r in RodzajRopy, pp in PolProdukt}{
	for{{0}:iloscKrakowania[r,pp]}{
            for{pp2 in PolProdukt}{
			printf "%s %s  %s %f %f \n", r, pp, pp2, iloscKrakowania[r, pp], (iloscKrakowania[r,pp] * WydajnoscKrakowania[pp2,pp]);
            }
		}
}

end;
