#include "stdafx.h"
#include "Pollard.h"

void Pollard::new_xab(ZZ& x, ZZ& a, ZZ& b) {
	switch (x % 3) {
	case 0: x = x*x % _p;	  a = a * 2 % _q;	  b = b * 2 % _q;	 break;
	case 1: x = x*_g % _p;	  a = (a + 1) % _q;		                 break;
	case 2: x = x*_h  % _p;		                  b = (b + 1) % _q;  break;
	}

//#pragma omp critical 
//	std::cout << x << '\t' << NumBits(x) << std::endl;
}
ZZ Pollard::solv_mods(ZZ a, ZZ b) {
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

void Pollard::Finder(ZZ a, ZZ b) {
	ZZ x = AddMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p);
	ZZ j = ZZ(40), k = ZZ(1);
	for (ZZ i = ZZ(1); i < _q; ++i) {
		while (!(NumBits(x) < j)) {
			new_xab(x, a, b);
			k++;
			if (k == 1000) {
				k = 0;
				j += 1;
			}
		}

		Score* temp = new Score();
		temp->X = x; temp->A = a; temp->B = b;

#pragma omp critical 
		{
			//std::cout << "ja" << omp_get_thread_num() << std::endl;
			int a0 = omp_get_thread_num();
			std::set<Score>::iterator it = _scoreSet->find(*temp);
			if (it != _scoreSet->end()) {
				ZZ A = (*it).A;
				ZZ B = (*it).B;
				ZZ X = (*it).X;
				std::cout << A << " " << a << std::endl;
				if (a != A || b != B) {
					ZZ d, L, P;
					SubMod(L, B, b, _q);
					SubMod(P, a, A, _q);
					std::cout << L << " * x = " << P << " mod(" << _q << ")" << std::endl;
					std::cout << "x = " << solv_mods(L, P) << std::endl;
				}
				else {
					RandomBits(a, NumBits(_q));
					RandomBits(b, NumBits(_q));

				}
			} else {
				_scoreSet->insert(*temp);
			}
		}
	}
}