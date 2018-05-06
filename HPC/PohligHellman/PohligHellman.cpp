// PohligHellman.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <NTL/ZZ_pXFactoring.h>
#include <NTL/RR.h>

#include <cstdio>
#include <omp.h>
#include <set>
#include <NTL/ZZ_pXFactoring.h>
using namespace NTL;
using namespace std;


struct wspolcz {
	ZZ a;
	int b;
	ZZ* tab;
	ZZ m;
	ZZ x;
};

int main() {
	ZZ p, p1, g, h, q, r, xxx;

	 //
	 //conv(p, 41);
	 //conv(g, 7);
	 //conv(h, 8);
	 //int number = 3;
	 //wspolcz *wspo = new wspolcz[number]; 
	 //conv(wspo[0].a,"54856744296005190376088007503238772912597637756459437722766589647168907823527");
	 //wspo[0].b = 1;
	 //wspo[0].tab = new ZZ[wspo[0].b];
	 //conv(wspo[1].a,"2");
	 //wspo[1].b = 5;
	 //wspo[1].tab = new ZZ[wspo[1].b];
	 //conv(wspo[2].a,"5");
	 //wspo[2].b = 2;
	 //wspo[2].tab = new ZZ[wspo[2].b];


	 //conv(p, 865);
	 //conv(g, 7);
	 //conv(h, 571);
	 //int number = 3;
	 //wspolcz *wspo = new wspolcz[number]; 
	 //conv(wspo[0].a,"54856744296005190376088007503238772912597637756459437722766589647168907823527");
	 //wspo[0].b = 1;
	 //wspo[0].tab = new ZZ[wspo[0].b];

	 //conv(wspo[1].a,"2");
	 //wspo[1].b = 5;
	 //wspo[1].tab = new ZZ[wspo[1].b];
	 //conv(wspo[2].a,"3");
	 //wspo[2].b = 3;
	 //wspo[2].tab = new ZZ[wspo[2].b];


	conv(p, "202327791490377140758464398193118769284439196142986126279915588021771386232677882549112708964186884026350860407958136175740881740628224988267864707597377271184961029855048667806550593922548228632023290074273408053030385254183397253857681194998317095371053635831481156636421152289204748539042513299636082690580692287478955976526962756066629702543669072286037614147742096222380077277365076765045323737035735306725391742251767963588950208828605671139168836767442083884180283012596861030842775701292515251046069035412235854318975883178452440741004087507879653243965645889159358722812471110700304226199565876267907633453380521393669611534718226262738951929641094147791792737379253856159810998191755370406537502219956395418928907442701763500540567782371422782284801389661590617038640164864787230265789768740456316098849");
	conv(g, "4");
	conv(h, "84802687867287064634545154787452509268175978707027688540493994062823554040031516055055229676615944528058804689781557334018043213771361044208869334489333684817780727741898439433462518123244341169891440787319683198903610182860942081616676433738533967217776484158975644128409160949106838889895286128889820211446431916725724688773140769906976451678925892969095299784156088425294670224005057843715971646137383220815782434295193202994427661237263371294469130862519579485887069542226665689197610931295151508986615495768411006900233291231591878520047047187331484720511499878549022249823890206286881138011599605858810946531686438554533795281439509406276335509113852745081906963134078869100403865873620444354786126563099018270001164658847022215925077210057822623991469139993930479326455683985660227167551869785162267670328");
	conv(xxx, "63734191761461011421611230889054252656622616247056804702874063456510817153650792293901046351159197971564710264265532889548339607808439260921611444969286492439069315823315157568149360120022326120023719105478315913990751272350352461092461038202504105677500832244854410151569169186640861123660400114841517403942541295909536778071433332073665516423956307448559764442170143337939658645164093437610861963361051807104241120382182503001814621586988007186329907042662483417630815819699841094079411411155745846667291723001261455515822430293836539577540737311794984646166532860548123651757035225278752309852987812528401167954409384695421437603015095834766181513149598555951836311691461175812342462989577187034881466205131018455858451168936219382464191637885016245732048560927810834439116629797577299306657621043617968329832");
	int number = 13;
	wspolcz *wspo = new wspolcz[number];

	conv(wspo[0].a, "54856744296005190376088007503238772912597637756459437722766589647168907823527");
	wspo[0].b = 1;
	wspo[0].tab = new ZZ[wspo[0].b];
	conv(wspo[1].a, "2");
	wspo[1].b = 5;
	wspo[1].tab = new ZZ[wspo[1].b];
	conv(wspo[2].a, "35750253434573");
	wspo[2].b = 3;
	wspo[2].tab = new ZZ[wspo[2].b];
	conv(wspo[3].a, "1060684038402451");
	wspo[3].b = 6;
	wspo[3].tab = new ZZ[wspo[3].b];
	conv(wspo[4].a, "141583302684311");
	wspo[4].b = 5;
	wspo[4].tab = new ZZ[wspo[4].b];
	conv(wspo[5].a, "400152393026411");
	wspo[5].b = 5;
	wspo[5].tab = new ZZ[wspo[5].b];
	conv(wspo[6].a, "949220521959739");
	wspo[6].b = 6;
	wspo[6].tab = new ZZ[wspo[6].b];
	conv(wspo[7].a, "602553390313571");
	wspo[7].b = 5;
	wspo[7].tab = new ZZ[wspo[7].b];
	conv(wspo[8].a, "1067618563710359");
	wspo[8].b = 5;
	wspo[8].tab = new ZZ[wspo[8].b];
	conv(wspo[9].a, "314912451303421");
	wspo[9].b = 3;
	wspo[9].tab = new ZZ[wspo[9].b];
	conv(wspo[10].a, "905692419448373");
	wspo[10].b = 5;
	wspo[10].tab = new ZZ[wspo[10].b];
	conv(wspo[11].a, "125779335153427");
	wspo[11].b = 3;
	wspo[11].tab = new ZZ[wspo[11].b];
	conv(wspo[12].a, "998603684763283");
	wspo[12].b = 4;
	wspo[12].tab = new ZZ[wspo[12].b];

	q = p / 2 - 1;
	p1 = p - 1;



	cout << "..." << PowerMod(g, q, p) << endl;
	//cout << endl<<"::::"<<endl<<
	for (int i = 1; i<number; i++) {
		for (int j = 0; j < wspo[i].b; j++) {
			cout << "liczenie dla " << wspo[i].a << " " << j << endl;
			ZZ sum;
			conv(sum, 0);
			ZZ pp = p1 / PowerMod(wspo[i].a, j + 1, p);
			//cout << pp << endl;
			ZZ newh = PowerMod(h, pp, p);
			
			ZZ pozbycsie = PowerMod(wspo[i].a, j, p);
			ZZ nastepny = MulMod(pozbycsie, pp, p);
			//ZZ ostatni = PowerMod(g, nastepny, p1);
	//		cout << pozbycsie << "\n kurde \n"<< nastepny << "\n ... asdf ... \n" << ostatni << endl;


			ZZ newg = PowerMod(g, nastepny, p);
			//cout << PowerMod(wspo[i].a, 0, p) << "   " << endl;
			for (int k = 0; k < j; k++) {
				sum += wspo[i].tab[k] * PowerMod(wspo[i].a, k, p);
			}
			if (sum != 0) {
				sum = MulMod(sum, pp, p);
				newh = MulMod(newh, InvMod(PowerMod(g, sum, p), p), p);
			}

			cout << newg << " ^ x ==" << newh << endl;
			cin >> wspo[i].tab[j];
			cout << wspo[i].tab[j] << endl;
			while (PowerMod(newg, wspo[i].tab[j], p) != newh)
				cin >> wspo[i].tab[j];
		}
		for (int j = 0; j < wspo[i].b; j++) {
			wspo[i].x += wspo[i].tab[j] * PowerMod(wspo[i].a, j, p);
		}
	}


	ZZ mmm = ZZ(1);

	for (int i = 1; i<number; i++) {
		mmm *= PowerMod(wspo[i].a, wspo[i].b, p);
	}

	ZZ xx = ZZ(0);

	for (int i = 1; i<number; i++) {
		ZZ teeemp = PowerMod(wspo[i].a, wspo[i].b, p);
		ZZ jezu = mmm / teeemp;
		xx += wspo[i].x * jezu *  InvMod(jezu % teeemp, teeemp);
	}
	cout << mmm << " " << xx % mmm << endl;

	ZZ x = xx % mmm;
	for (int i = 0; i < p / mmm; i++) {
		if (PowerMod(g, i * mmm + (xx % mmm), p) == h) {
			cout << "solution x = " << i * mmm + x << endl;
			//break;
		}
	}

}


