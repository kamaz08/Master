// PollardRhoParallel.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "Pollard.h"


using namespace std;
using namespace NTL;



int main()
{
	int nthreads = 8;

	ZZ* aTab = new ZZ[nthreads];
	ZZ* bTab = new ZZ[nthreads];
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
		bits = bits > 64 ? 4 : bits << 1;
	}
	std::set<Score>* score = new std::set<Score>();
	std::cout << p << endl << g << endl << q << endl << h << endl;





	int i;
#pragma omp parallel private(i) shared(p,g,h,q,score) num_threads(nthreads) 
	{
#pragma omp for 
		for (i = 0; i < nthreads; i++) {
			Pollard *polard = new Pollard(p, g, h, q, score);
			polard->Finder(aTab[i], bTab[i]);
			//polard->Finder();
		}
	}
	getchar();	getchar();	getchar();
	return 0;
}


