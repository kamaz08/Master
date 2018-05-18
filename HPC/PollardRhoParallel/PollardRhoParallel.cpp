// PollardRhoParallel.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Pollard.h"
#include <set>

using namespace std;
using namespace NTL;



int main()
{
	int nthreads = 8;

	ZZ* aTab = new ZZ[nthreads];
	ZZ* bTab = new ZZ[nthreads];
	ZZ* xTab = new ZZ[nthreads];
	int bits = 16;

	ZZ p = conv<ZZ>(23), g = conv<ZZ>(4), h = conv<ZZ>(8), q = conv<ZZ>(11);
	cin >> p;
	cin >> g;
	cin >> h;
	cin >> q;

	for (int i = 0; i < nthreads; i++) {
		RandomBits(aTab[i], bits);
		RandomBits(bTab[i], bits);
		aTab[i] = aTab[i] % q;
		bTab[i] = bTab[i] % q;
		bits = bits > 40 ? 4 : bits << 1;
	}
	std::set<Score>* score = new std::set<Score>();
	std::cout << p << endl << g << endl << q << endl << h << endl;




	int i;
	Pollard **polard = new Pollard*[nthreads];
	for (i = 0; i < nthreads; i++) {
		polard[i] = new Pollard(p, g, h, q, score);
	}
	
#pragma omp parallel for private(i) num_threads(nthreads)
	for (i = 0; i < nthreads; i++) {
		polard[i]->Finder(aTab[i], bTab[i], xTab[i]);
	}

	getchar();	getchar();
	return 0;
}


