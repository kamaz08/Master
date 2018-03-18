#pragma once
#include "stdafx.h"

using namespace NTL;
class Pollard {
private:
	ZZ _p, _g, _h, _q;
	ZZ func(ZZ x, ZZ c) {
		return (x ^ 2 + c) % _n;
	}
	ZZ NWD(ZZ a, ZZ b) {
		return GCD(a, b);
	}

public:
	Pollard(ZZ p, ZZ g, ZZ h, ZZ q):_p(p), _g(g), _h(h), _q(q) {}
	ZZ Calculate() {
		ZZ d = (ZZ)1;
		ZZ c = (ZZ)1;
		ZZ a =(ZZ)1, b=(ZZ)1;
		ZZ temp;

		while (d == 1 || (d == _p && c < 1000)) {


			a = func(a, c);
			b = func(func(b, c), c);
			temp = 
			d = NWD(a < b ? a - b : b - a, temp);
			if (d == _n) c++;
			if (d != 1 && d != _n) return d;
		}
		return _n;
	}

};