#include "stdafx.h"
#include "Pollard.h"

void Pollard::new_xab(ZZ& x, ZZ& a, ZZ& b) {
	switch (x % 3) {
	case 0: x = MulMod(x, x, _p);		 a = MulMod(a, 2, _q);	  b = MulMod(b, 2, _q);	 break;
	case 1: x = MulMod(x, _g, _p);		 a = AddMod(a, 1, _q);							 break;
	case 2: x = MulMod(x, _h, _p);								  b = AddMod(b, 1, _q);  break;
	}
}
ZZ Pollard::solv_mods(ZZ a, ZZ b) {
	ZZ d(0);
	if (InvModStatus(d, a, _q) > 0) {
		return ZZ(0);
	}
	else return MulMod(d, b, _q);
}
void Pollard::Check(ZZ x) {
	std::cout << PowerMod(_g, x, _p) << std::endl;
}

void Pollard::Finder(ZZ a, ZZ b, ZZ x) {
	x = MulMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p);
	ZZ j = ZZ(40), k = ZZ(1);
	for (ZZ i = ZZ(1); i < _q; ++i) {
		new_xab(x, a, b);
		while (!(NumBits(x) < j)) {
			new_xab(x, a, b);
			k++;
			if (k == 1000) {
				k = 0;
				j += 1;
			}
		}


#pragma omp critical
		{
//			std::cout << "fuck " << omp_get_thread_num() << std::endl;
			//Sleep(5000);
			Score* temp = new Score();
			temp->X = ZZ(x); temp->A = ZZ(a); temp->B = ZZ(b);
			if (MulMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p) != x) {
				std::cout << "a " << a << "\tb " << b << "\tx " << x << "\tX " << MulMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p) << std::endl;

				std::cout << "haha" << std::endl;
			}
			//std::cout << "ja" << omp_get_thread_num() << std::endl;
			int a0 = omp_get_thread_num();
			std::set<Score>::iterator it = _scoreSet->find(*temp);
			if (it != _scoreSet->end()) {
				//Sleep(100);
				ZZ A = (*it).A;
				ZZ B = (*it).B;
				ZZ X = (*it).X;
				//std::cout << A << " " << a << std::endl;
				if (temp->A != A || temp->B != B) {
					ZZ d, L, P;
					std::cout << "aa" << PowerMod(_g, temp->A, _p) << " AA" << PowerMod(_g, A, _p) << std::endl;
					std::cout << "BB" << PowerMod(_h, B, _p) << " bb" << PowerMod(_h, temp->B, _p) << std::endl;
					std::cout << "xx" << MulMod(PowerMod(_g, temp->A, _p), PowerMod(_h, temp->B, _p), _p) << " XX" << MulMod(PowerMod(_g, A, _p), PowerMod(_h, B, _p), _p) << std::endl;
					std::cout << "a " << a << "\tb " << temp->A << "\tA " << A << "\tB " << B << "\tx " << x << "\tX " << X << std::endl;

					SubMod(L, B, b, _q);
					SubMod(P, a, A, _q);
					ZZ temp = solv_mods(L, P);
					if (temp != ZZ(0)) {
						std::cout << "a " << a << "\tb " << b << "\tA " << A << "\tB " << b << "\tx " << x << "\tX " << X << std::endl;

						std::cout << L << " * x = " << P << " mod(" << _q << ")" << std::endl;
						std::cout << "x = " << temp << std::endl;
						Check(temp);


						getchar();
					}
					else {
						RandomBits(a, NumBits(_q) >> 2);
						RandomBits(b, NumBits(_q) >> 2);
						x = MulMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p);
					}
				}
				else {
					RandomBits(a, NumBits(_q) >> 2);
					RandomBits(b, NumBits(_q) >> 2);
					x = MulMod(PowerMod(_g, a, _p), PowerMod(_h, b, _p), _p);

				}
			}
			else {
				_scoreSet->insert(*temp);
			}
		}
	}
}