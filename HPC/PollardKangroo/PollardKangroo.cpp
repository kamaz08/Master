#include"stdafx.h"

using namespace std;
using namespace NTL;
ZZ test;
class Kang {
protected:
	ZZ * dist, *jump;
	ZZ p, g, h, q, md;
	long tl;
public:
	ZZ x, d;
	bool wild;

	Kang(ZZ p, ZZ g, ZZ h, ZZ q, ZZ *dist, ZZ *jump, long tl, bool wild) : p(p), g(g), h(h), q(q), tl(tl), dist(dist), jump(jump), wild(wild) {}

	long Hash(ZZ x) {
		return x % tl;
	}

	void Jump() {
		long h = Hash(x);
		x = (x*jump[h]) % p;
		d =  AddMod(d,dist[h], q);
	}

	bool IsDis() {
		return  NumBits(x) <= 40;
	}
	virtual void Init(ZZ md, ZZ i) = 0;
	virtual void FindOrAdd(unordered_set<unsigned long> & tamed_set, unordered_map<unsigned long, ZZ> & tamed_d,
		unordered_set<unsigned long> & wild_set, unordered_map<unsigned long, ZZ> & wild_d, int & running) = 0;
};

class Wild : public Kang {
public:
	Wild(ZZ p, ZZ g, ZZ h, ZZ q, ZZ *dist, ZZ *jump, long tl) : Kang(p, g, h, q, dist, jump, tl, true) {}
	void Init(ZZ md1, ZZ i) {
		d = i;
		//md = md1;
		x = (h*PowerMod(g, d, p)) % p;
	}

	virtual void FindOrAdd(unordered_set<unsigned long> & tamed_set, unordered_map<unsigned long, ZZ> & tamed_d,
		unordered_set<unsigned long> & wild_set, unordered_map<unsigned long, ZZ> & wild_d, int & running) {
		unsigned long xt = conv<unsigned long>(x);
		if (tamed_set.find(xt) != tamed_set.end()) {
#pragma omp critical(wild_d)
			{
#pragma omp critical(tamed_d)
				{
					if (running) {
						cout << "colision at " << x << " " << d << " " << wild_d[xt] << endl;
						ZZ w = ((AddMod(tamed_d[xt], test, p) - d) % q);
						cout << "x = " << w << endl;
						cout << "g ^ x = " << PowerMod(g, w, p) << endl;
						running = 0;
					}
				}
			}
		}
		else {
			if (wild_set.find(xt) == wild_set.end()) {
#pragma omp critical(wild_d)
				{
					wild_set.insert(xt);
					wild_d[xt] = d;
				}
			}
			else {
				//cout << "..." << endl;
			}
		}
	}
};

class Tamed : public Kang {
public:
	Tamed(ZZ p, ZZ g, ZZ h, ZZ q, ZZ *dist, ZZ *jump, long tl) : Kang(p, g, h, q, dist, jump, tl, false) {}
	void Init(ZZ md1, ZZ i) {
		d = i;
		//md = md1;
		x = PowerMod(g, d, p);
		//#pragma omp critical(hue)
		//		{
		//			cout<<"fuck  " << omp_get_thread_num() << " " << " " << x << endl;
		//		}
	}

	virtual void FindOrAdd(unordered_set<unsigned long> & tamed_set, unordered_map<unsigned long, ZZ> & tamed_d,
		unordered_set<unsigned long> & wild_set, unordered_map<unsigned long, ZZ> & wild_d, int & running) {

		unsigned long xt = conv<unsigned long>(x);
		if (wild_set.find(xt) != wild_set.end()) {
#pragma omp critical(wild_d)
			{
#pragma omp critical(tamed_d)
				{
					if (running) {
						cout << "colision at " << x << " " << d << " " << wild_d[xt] << endl;
						ZZ w =  (SubMod( AddMod(d, test, q), wild_d[xt], q));

						cout << "x = " << w << endl;
						cout << "g ^ x = " << PowerMod(g, w, p) << endl;
						running = 0;
					}
				}
			}
		}
		else {
			if (tamed_set.find(xt) == tamed_set.end()) {
#pragma omp critical(tamed_d)
				{
					tamed_set.insert(xt);
					tamed_d[xt] = d;
				}
			}
			else {
				//cout << "..." << endl;
			}
		}
	}
};


ZZ a;
int main(int argc, char *argv[]) {

	if (argc < 7) {
		std::cout << "Usage: p g h q a b" << std::endl;
		return 0;
	}

	std::unordered_set<unsigned long> tamed_set;
	std::unordered_map<unsigned long, ZZ> tamed_d;
	std::unordered_set<unsigned long> wild_set;
	std::unordered_map<unsigned long, ZZ> wild_d;
	int m = 4;
	ZZ p, g, h, q, a, b, Beta, V, r;
	ZZ * dist, *jump;
	p = conv<ZZ>(argv[1]);
	g = conv<ZZ>(argv[2]);
	h = conv<ZZ>(argv[3]);
	q = conv<ZZ>(argv[4]);
	a = conv<ZZ>(argv[5]);
	b = conv<ZZ>(argv[6]);

	//h = MulMod(h,  InvMod(PowerMod(g, a, p), p), p) ;
	test = ZZ(0);

	//h = 
//	cout << h << endl;
	Beta = conv<ZZ>((m * SqrRoot(conv<RR>(b - a))) / conv<RR>(4)) % p;
	r = NumBits(Beta);
	V = Beta / m / 2;
	dist = new ZZ[conv<long>(r)];
	jump = new ZZ[conv<long>(r)];
	long rl = conv<long>(r);

	for (long i = 0; i < rl; ++i) {
		dist[i] = PowerMod(ZZ(2), ZZ(i), p);
		jump[i] = PowerMod(g, dist[i], p);
	}

	ZZ midle = ZZ(0); // (a + b) / 2;
	Kang * kang;
	int index = 0;
	int running = 1;
	ZZ i;
#pragma omp parallel private(kang,index) shared(p,g,q,h,rl,V, midle, m,dist,jump,running, tamed_set, tamed_d, wild_set, wild_d, i, a, test)  num_threads(m)
	{
		if (omp_get_thread_num() < (m / 2)) {
			kang = new Tamed(p, g, h, q, dist, jump, rl);
			index = omp_get_thread_num();
		}
		else {
			kang = new Wild(p, g, h, q, dist, jump, rl);
			index = omp_get_thread_num() - (m / 2);
		};
		kang->Init(midle, index + V);
//#pragma omp critical(hue)
//		{
//			cout << index * V << " " << kang->wild << endl;
//		}
		for (i = index; i < p && running; ++i) {
			do {
				kang->Jump();
			} while ((!kang->IsDis()));

			//#pragma omp critical(hue)
			//			{
			//			cout << omp_get_thread_num()<< " " << NumBits(kang->x)<< " " << kang->wild << " " << kang->x << endl;
			//			}
			kang->FindOrAdd(tamed_set, tamed_d, wild_set, wild_d, running);
		}
	}

	//getchar(); getchar();
	return 0;
}