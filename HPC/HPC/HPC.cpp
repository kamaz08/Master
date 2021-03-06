// HPC.cpp : Defines the entry point for the console application.
//


#include "stdafx.h"
#include "pollard.h"

using namespace std;
using namespace NTL;

void test(ZZ& a, int i) {
	a *= (i + 1);
}

void test(ZZ& a) {
	cout << "hehehe" << endl;
	cout << a << endl;
	cout << a % 3 << endl;
	//a = a % ZZ(100);
}

int main()
{
	//int p = 24, g = 4, q = 8, h = 11;
	//ZZ p = ZZ(24), g = ZZ(4), q = ZZ(8), h = ZZ(11);
	ZZ p = conv<ZZ>(23), g = conv<ZZ>(4), h = conv<ZZ>(8), q = conv<ZZ>(11);
	cin >> p;
	cin >> g;
	cin >> h;
	cin >> q;
	std::cout << p << endl << g << endl << q << endl << h << endl;

	Pollard *polard = new Pollard(p, g, h, q);
	polard->Finder();

	getchar();
	return 0;
}