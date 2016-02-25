/*
 * operators_demo.cpp
 *
 *  Created on: 2016年2月25日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/operators.hpp>
using namespace boost;
/*
 * C++操作符重载：
 * equality_comparable : 要求实现==, 自动实现!=
 * less_than_comparable :要求实现<, 自动实现> <= >=
 * addable 要求提供+=, 自动实现+
 * subtractable
 * incrementable
 * decrementable
 * equivalent 等价语义，要求实现<, 自动实现==
 */


class point_lt : boost::less_than_comparable<point_lt>
{
public:
	int x,y,z;
	point_lt(int a=0,int b=0,int c=0) : x(a),y(b),z(c) {
		//cout << "point_lt@" << this << " ctor"<< endl;
	}
	~point_lt(){
		//cout << "point_lt@" << this << " dtor"<< endl;
	}
	void print() const{
		cout << "(" << x <<"," << y <<"," << z << ")" << endl;
	}
	friend bool operator<(const point_lt& l, const point_lt& r){
		return (l.x + l.y + l.z) < (r.x + r.y + r.z);
	}
};


int main(int argc, char* argv[]){
	point_lt p0, p1(1,2,3), p2(3,0,5), p3(3,2,1);
	cout << boolalpha << (p0 < p1) << endl;
	cout << boolalpha << (p1 <= p3) << endl;
	cout << boolalpha << (p2 > p1) << endl;
	cout << boolalpha << (p0 >= p1) << endl;
	return 0;
}
