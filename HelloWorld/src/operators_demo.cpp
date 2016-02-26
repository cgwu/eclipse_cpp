/*
 * operators_demo.cpp
 *
 *  Created on: 2016��2��25��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/operators.hpp>
using namespace boost;
/*
 * C++���������أ�
 * equality_comparable : Ҫ��ʵ��==, �Զ�ʵ��!=
 * less_than_comparable :Ҫ��ʵ��<, �Զ�ʵ��> <= >=
 * addable Ҫ���ṩ+=, �Զ�ʵ��+
 * subtractable
 * incrementable
 * decrementable
 * equivalent �ȼ����壬Ҫ��ʵ��<, �Զ�ʵ��==
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


int main_operators_demo(int argc, char* argv[]){
	point_lt p0, p1(1,2,3), p2(3,0,5), p3(3,2,1);
	cout << boolalpha << (p0 < p1) << endl;
	cout << boolalpha << (p1 <= p3) << endl;
	cout << boolalpha << (p2 > p1) << endl;
	cout << boolalpha << (p0 >= p1) << endl;
	return 0;
}
