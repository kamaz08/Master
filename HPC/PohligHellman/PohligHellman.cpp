// PohligHellman.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include <NTL/ZZ_pXFactoring.h>
#include <NTL/RR.h>
#include <set>
using namespace NTL;
using namespace std;
struct wspolcz {
	ZZ a;
	int b;
	ZZ* tab;
	ZZ m;
	ZZ x;
};

struct Score {
	ZZ X;
	ZZ A;
	inline bool operator< (const Score& t) const {
		return X < t.X;
	}
	inline bool operator== (const Score& t) const {
		return X == t.X;
	}
};

void setNumber(wspolcz& number, int wspol, char* pods);
ZZ  getOrd(ZZ& p, ZZ& pp, ZZ& g, ZZ& x);

int main() {
	ZZ p, p1, g, h, q, r, xxx;
	//conv(p, 41);
	//conv(g, 7);
	//conv(h, 23);
	//int number = 3;
	//wspolcz *wspo = new wspolcz[number]; 
	//conv(wspo[0].a,"54856744296005190376088007503238772912597637756459437722766589647168907823527");
	//wspo[0].b = 1;
	//wspo[0].tab = new ZZ[wspo[0].b];
	//conv(wspo[1].a,"2");
	//wspo[1].b = 3;
	//wspo[1].tab = new ZZ[wspo[1].b];
	//conv(wspo[2].a,"5");
	//wspo[2].b = 1;
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


	//conv(p, "18786194788879661663746047005075302509348784060862038128468989403816467530573906901715739452151507113610872524511607754173057008251001655406808716111576684675251046216596235328403351713375426726457153429622273779903793");
	//conv(g, "565061");
	//conv(h, "9271837272965643681231787611696453492367719053186411129859879455847682034741978922557743259762986099725768104395860331915191261050461770255672083674823217416586126016699924223394806256643746012968817031655417863945658");
	//conv(xxx, "9244648548005050119577028427992210643787607528573644958525367031828421292478021704474573269814779917394388564021352667785416048006957786100091104666166779172777649165015098516189647966842576714788857171399244194249394");
	////cout << MulMod(xxx,ZZ(1),ZZ(4)) << endl;
	//cout << PowerMod(g, (p - 1) / 2, p) << endl;
	//cout << PowerMod(g, (p - 1) / 4, p) << endl;
	//cout << PowerMod(g, (p - 1) / 8, p) << endl;
	//cout << PowerMod(g, (p - 1) / 16, p) << endl;
	//cout << endl << endl << xxx % 2 << endl << xxx % 4 << endl << xxx % 8 << endl << xxx % 16 << endl;
	//int number = 9;
	//wspolcz *wspo = new wspolcz[number];
	//setNumber(wspo[0], 1, "92003751826890977393707877940393922349207862203113773083718929814713789683027");

	//cout <<endl<< "$$$$$$"<<endl<< h << endl << g << endl<< endl;

	//setNumber(wspo[1], 4, "2");
	//setNumber(wspo[2], 4, "791677");
	//setNumber(wspo[3], 3, "901567");
	//setNumber(wspo[4], 4, "740581");
	//setNumber(wspo[5], 3, "1042577");
	//setNumber(wspo[6], 3, "77999");
	//setNumber(wspo[7], 4, "29303");
	//setNumber(wspo[8], 4, "780817");



	g = PowerMod(g, wspo[0].a, p);
	h = PowerMod(h, wspo[0].a, p);
	ZZ ord = p - 1;
	for (int i = 1; i < number; i++) {
		ZZ test = getOrd(p, ord, g, wspo[i].a);
		if (test != 0)
			ord = test;
	}
	cout << "Ord = " << ord << endl;
	cout << "Check: " << PowerMod(g, ord, p) << endl << endl;

	q = p / 2 - 1;
	p1 = ord;

	cout << "Test:" << endl << "GCD(g,p) = " << GCD(g, p) << endl;
	cout << "g^(p-1) % p = " << PowerMod(g, p1, p) << endl;
	for (int i = 1; i < number; i++) {
		for (int j = 0; j < wspo[i].b; j++) {
			cout << "Liczenie dla " << wspo[i].a << " " << j << endl;
			ZZ sum; 
			conv(sum, 0);
			ZZ dziel = PowerMod(wspo[i].a, j + 1, p);
			if (p1 % dziel != 0) {
				wspo[i].tab[j] = 0;
				continue;
			}
			ZZ pp = p1 / dziel;
			ZZ newh = PowerMod(h, pp, p);
			ZZ pozbycsie = PowerMod(wspo[i].a, j, p);
			ZZ nastepny = MulMod(pozbycsie, pp, p);
			ZZ newg = PowerMod(g, nastepny, p);
			for (int k = 0; k < j; k++) {
				sum += wspo[i].tab[k] * PowerMod(wspo[i].a, k, p);
			}
			if (sum != 0) {
				sum = MulMod(sum, pp, p);
				newh = MulMod(newh, InvMod(PowerMod(g, sum, p), p), p);
			}

			cout << newg << " ^ x\n==" << newh << endl;
			if (newh == ZZ(1)) {
				wspo[i].tab[j] = 0;
				cout << 0<< endl;
				continue;
			}
			if (newh == newg) {
				wspo[i].tab[j] = j;
				cout << 1 << endl;
				continue;
			}
			ZZ size = SqrRoot(wspo[i].a - 1) + 1;
			std::set<Score> items;
			cout << endl << " SIZE" << size <<endl;
			for (ZZ zz = ZZ(1); zz < size; zz++) {
				Score* temp = new Score();
				temp->X = PowerMod(newg, zz, p); temp->A = ZZ(zz);
				//cout << temp->X << endl;
				items.insert(*temp);
			}


			cout << endl <<" x-1"  << endl;
			Score* temp = new Score();
			temp->X = MulMod(newh, InvMod(PowerMod(newg, ord - 1, p), p), p);
			std::set<Score>::iterator it = items.find(*temp);

			if (it != items.end()) {
				ZZ hurra = (q - 1) + (*it).A;
				wspo[i].tab[j] = hurra;
				cout << "yea" << endl << hurra << endl;
				continue;
			}


			for (ZZ zz = ZZ(1); zz <= size; zz++) {
				Score* temp = new Score();
				temp->X = MulMod(newh, InvMod(PowerMod(newg,zz * size, p), p), p);
				//cout << "... " << zz << temp->X << endl;
				std::set<Score>::iterator it = items.find(*temp);

				if (it != items.end()) {
					ZZ hurra = zz * size + (*it).A;
					wspo[i].tab[j] = hurra;
					cout << "yea" << endl << hurra << endl;
				}
				delete temp;
			}
			cout << wspo[i].tab[j] << endl << PowerMod(newg, wspo[i].tab[j], p) << endl;

		}
		for (int j = 0; j < wspo[i].b; j++) {
			wspo[i].x += wspo[i].tab[j] * PowerMod(wspo[i].a, j, p);
		}
	}


	ZZ mmm = ZZ(1);

	for (int i = 1; i < number; i++) {
		mmm *= PowerMod(wspo[i].a, wspo[i].b, p);
	}

	ZZ xx = ZZ(0);

	for (int i = 1; i < number; i++) {
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


void setNumber(wspolcz& number, int wspol, char* pods) {
	conv(number.a, pods);
	number.b = wspol;
	number.tab = new ZZ[number.b];
	cout << number.a << endl;
}

ZZ getOrd(ZZ& p, ZZ& pp, ZZ& g, ZZ& x) {
	ZZ result = ZZ(0);
	for (int i = 0; i < 10; i++) {
		ZZ ptest = pp / PowerMod(x, i, p);
		ZZ test = PowerMod(g, ptest, p);
		if (test == 1)
			result = ptest;
	}
	return result;
}


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


