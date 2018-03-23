#pragma once
#include "stdafx.h"

using namespace NTL;


class Pollard {
private:
	ZZ _p, _g, _h, _q;

	void new_xab(ZZ& x, ZZ& a, ZZ& b) {
		switch (x % 3) {
		case 0: x = x*x     % _p;  a = a * 2 % _q;  b = b * 2 % _q;  break;
		case 1: x = x*_g % _p;  a = (a + 1) % _q;                  break;
		case 2: x = x*_h  % _p;                  b = (b + 1) % _q;  break;
		}
	}

	ZZ solv_mods(ZZ a, ZZ b) {
		ZZ d;
		if (InvModStatus(d, a, _q) > 0) {
			ZZ tmp;
			if (InvModStatus(tmp, d, _q)) return solv_mods(a / d, b / d);
			else {
				a = MulMod(a, tmp, _q);
				b = MulMod(b, tmp, _q);
				return solv_mods(a, b);
			}
		}
		else return MulMod(d, b, _q);
	}


public:
	Pollard(ZZ p, ZZ g, ZZ h, ZZ q) {
		_p = p;	_g = g; _h = h;	_q = q;
	}

	void Finder() {
		ZZ x = ZZ(1), a = ZZ(0), b = ZZ(0);
		ZZ X = ZZ(x), A = ZZ(a), B = ZZ(b);

		for (ZZ i = ZZ(1); i < _p; ++i) {
			new_xab(x, a, b);
			new_xab(X, A, B);
			new_xab(X, A, B);
			//std::cout << x << "\t" << X << "\t" << a << "\t" << A << "\t" << b << "\t" << B << std::endl;
			if (x == X) {
				ZZ d;
				ZZ L;
				ZZ P;
				SubMod(L, B, b, _q);
				SubMod(P, a, A, _q);
				std::cout << L << " * x = " << P << " mod(" << _q << ")" << std::endl;
				std::cout << "x = " << solv_mods(L, P) << std::endl;
			}
		}
	}


};