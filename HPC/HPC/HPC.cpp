// HPC.cpp : Defines the entry point for the console application.
//


#include "stdafx.h"
#include "pollard.h"

using namespace std;
using namespace NTL;


void pollard() {
	ZZ p;
	cin >> p;
	Pollard *pol = new Pollard(p);
	cout << pol->Calculate() << endl;
	getchar();
}

int main()
{
	pollard();
	ZZ p;

	getchar();
	getchar();
}