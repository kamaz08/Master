/*
Kamil Sikorski
221481
*/


set RodzajRopy;
set PolProdukt;
set Produkt;

param KosztRopy{r in RodzajRopy} >= 0;
param KosztReformowania >=0; 
param KosztKrakowania >=0;
param WydajnoscReformowania{r in RodzajRopy, pp in PolProdukt} >= 0;
param WydajnoscKrakowania{ppOld in PolProdukt, ppNew in PolProdukt} >= 0; 
param KosztProduktuReformowania{p in Produkt, pp in PolProdukt} >= 0;
param KosztProduktuKrakowanie{p in Produkt, pp in PolProdukt} >= 0;
param MinimumProduktow{p in Produkt} >= 0;
param ZawartoscSiarkiReformowania{r in RodzajRopy} >= 0;
param ZawartoscSiarkiKrakowanie{r in RodzajRopy} >= 0;
param MaxSiarki >= 0;



var iloscRopy{r in RodzajRopy} >= 0;
var iloscPolProduktowZRopy{r in RodzajRopy, pp in PolProdukt} >= 0;
var iloscPolProduktowNaProdukt{r in RodzajRopy, pp in PolProdukt, p in Produkt} >=0;
var iloscPolProduktowNaKrakowanie{r in RodzajRopy, pp in PolProdukt} >= 0;

minimize cost: (sum{r in RodzajRopy} KosztRopy[r] * iloscRopy[r] * KosztReformowania)+
                (sum{r in RodzajRopy, pp1 in PolProdukt}iloscPolProduktowNaKrakowanie[r,pp1] * KosztKrakowania);


s.t. IlePolProduktowZRopy{r in RodzajRopy, pp in PolProdukt} : 
    iloscRopy[r] * WydajnoscReformowania[r,pp] = iloscPolProduktowZRopy[r,pp];

s.t. IlePolProduktowNaProdukt{r in RodzajRopy, pp in PolProdukt}: 
    iloscPolProduktowZRopy[r,pp] - iloscPolProduktowNaKrakowanie[r, pp] 
    = (sum{p in Produkt} iloscPolProduktowNaProdukt[r,pp,p] * KosztProduktuReformowania[p,pp]);
/*  
s.t. IlePolProduktowNaProdukt2{pp in PolProdukt, r in RodzajRopy}: 
    sum{p in Produkt} iloscPolProduktowNaProdukt[r,pp,p] 
    = iloscPolProduktowZRopy[r,pp] - iloscPolProduktowNaKrakowanie[r, pp];
*/
s.t. MinimalnaIloscProduktow{p in Produkt} : MinimumProduktow[p] <= (sum{r in RodzajRopy, pp in PolProdukt}( iloscPolProduktowNaProdukt[r,pp,p] * KosztProduktuReformowania[p,pp]
    + (sum{ppold in PolProdukt} WydajnoscKrakowania[ppold,pp] * iloscPolProduktowNaKrakowanie[r,ppold]* KosztProduktuKrakowanie[p,pp])));

s.t. MaksymalnaIloscSiarki : sum{r in RodzajRopy, pp in PolProdukt}
    (ZawartoscSiarkiReformowania[r] * iloscPolProduktowNaProdukt[r,pp,'DomowePaliwaOlejowe'] + 
    ZawartoscSiarkiKrakowanie[r] * iloscPolProduktowNaKrakowanie[r,pp] * WydajnoscKrakowania[pp,'Olej'] ) <= sum{r in RodzajRopy, pp in PolProdukt}
    (iloscPolProduktowNaProdukt[r,pp,'DomowePaliwaOlejowe'] + iloscPolProduktowNaKrakowanie[r,pp] * WydajnoscKrakowania[pp,'Olej'] ) * MaxSiarki;


solve;
printf "\n\n" ;
printf 'Minimum Cost = %.2f\n', cost;


for{r in RodzajRopy}{
    printf "%s %f\n", r, iloscRopy[r];
    for{pp in PolProdukt}{
        printf "\t%s %f\n", pp, iloscPolProduktowZRopy[r,pp];
        for{p in Produkt}{
            printf "\t\t %s %f \n", p,  iloscPolProduktowNaProdukt[r, pp,p];
        }
        for{pp2 in PolProdukt}{
            printf "\t\t\t %s %s %f \n", pp, pp2, (iloscPolProduktowNaKrakowanie[r,pp] * WydajnoscKrakowania[pp,pp2]) ;
        }
    }
}
/*
for{p in Produkt, pp in PolProdukt, pp2 in PolProdukt, r in RodzajRopy}{
    printf "%s %s %s %s %f %f %f %f \n", p, pp, pp2, r, iloscPolProduktowNaProdukt[r, pp,p],  WydajnoscKrakowania[pp2,pp], iloscPolProduktowNaKrakowanie[r,pp] ,(WydajnoscKrakowania[pp,pp2] * iloscPolProduktowNaKrakowanie[r,pp]);
}

*/


end;



