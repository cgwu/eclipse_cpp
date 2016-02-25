/*
 * optional_demo.cpp
 *
 *  Created on: 2016��2��25��
 *      Author: cg
 */


#include "stdcpp.hpp"
#include <boost/optional.hpp>
#include <boost/typeof/typeof.hpp>
#include <boost/utility/in_place_factory.hpp>
using namespace boost;

optional<double> calc(int x){
	return optional<double>( x != 0, 1.0 /  x );	//�������캯��
}

int main_optional_demo(int argc, char* argv[]){
	optional<int> op0;			// δ��ʼ
	optional<int> op1(none);	// ��ֵ
//	optional<int> op1(12);
	assert(op0 == op1);
	assert(op1.get_value_or(253) == 253);	// ����ֵ��ȡֵ������ʹ��Ĭ��ֵ253
	op1 = 13;		//��������ֵ
//	cout << op1 << endl;		// ERROR!
	cout << op1.get_value_or(253) << endl;
	assert(op1.get_value_or(253) == 13);

	optional<string> ops("test");
	cout << *ops << endl;

	vector<int> v(10);
	optional<vector<int>& > opv(v);
	assert(opv);
	opv->push_back(5);		// ʹ�ü�ͷ��������������
	assert(opv->size() ==  11);
	opv = none;
	assert(!opv);

	optional<double> d1 = calc(2);
	cout << *d1 << endl;			// ��optional����ָ��ʹ��
	cout << "d1.get_value_or: " << d1.get_value_or(3.14) << endl;		//Ҳ��ʹ�ó�Ա����ȡֵ
	optional<double> d2 = calc(0);
	assert(!d2);

	BOOST_AUTO(x, make_optional(5));
	assert(*x ==  5);
	BOOST_AUTO(y, make_optional<double>( *x > 10, 1.0));
	assert(!y);

	// �͵ش���string���󣬲���Ҫ��ʱ����
	optional<string> ops1(in_place("test in_place_factory"));
	cout << *ops1 << endl;

	//�͵ش���std::vector���󣬲���Ҫ��ʱ����vector
	optional<vector<int> > opp(in_place(10,3));
	assert(opp->size() ==  10);
	assert((*opp)[0] == 3);
	cout << opp->at(9) << endl;
	cout << "end" << endl;
	return 0;
}
