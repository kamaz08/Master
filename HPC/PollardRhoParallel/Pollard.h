#pragma once
#include "stdafx.h"
using namespace NTL;

struct Score {
	ZZ X;
	ZZ A;
	ZZ B;
	inline bool operator< (const Score& t) const {
		return X < t.X;
	}
	inline bool operator== (const Score& t) const {
		return X == t.X;
	}
};

class Pollard {
private:
	ZZ _p, _g, _h, _q;
	std::set<Score>* _scoreSet;
	void new_xab(ZZ& x, ZZ& a, ZZ& b);
	ZZ solv_mods(ZZ a, ZZ b);
public:
	Pollard(ZZ p, ZZ g, ZZ h, ZZ q, std::set<Score>* scoreSet) 
		: _p(p), _g(g), _h(h), _q(q){
		_scoreSet = scoreSet;
	}
	void Finder(ZZ a, ZZ b);
	void Finder();
};