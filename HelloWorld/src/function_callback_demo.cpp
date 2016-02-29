/*
 * function_callback_demo.cpp
 *
 *  Created on: 2016年2月29日
 *      Author: cg
 */

#include <cassert>
#include <cmath>
#include <typeinfo>
#include "stdcpp.hpp"
#include <boost/utility/result_of.hpp>
#include <boost/typeof/typeof.hpp>
#include <boost/ref.hpp>
using namespace boost;

void test_result_of(){
	typedef double (*Func) (double d);
	Func func = sqrt;
//	BOOST_AUTO( x , func(5.0));
	result_of<Func(double)>::type x = func(5.0);
	cout << typeid(x).name() << ":" << x << endl;
}

template<typename T, typename T1>
typename result_of<T(T1)>::type call_func(T t, T1 t1)	// 最前面的typename不可少，否则编译器会认为type是result_of静态成员变量
{
	return t(t1);
}
void test_call_func(){
	typedef double (*Func) (double d);
	Func func = sqrt;
	BOOST_AUTO( x , call_func(func, 4.0));
	cout << typeid(x).name() << ":" << x << endl;
}
void test_reference_wrapper(){
	int x = 10;
	reference_wrapper<int>rx(x);
	assert(x == rx);
	(int&)rx = 100;
	assert(x == 100);

	reference_wrapper<int>rx2(rx);
	cout << rx2.get() << endl;

	string str;
	reference_wrapper<string> rws(str);
	*rws.get_pointer() = "test reference_wrapper";
	cout << rws.get().size() << endl;

	double x1 = 2.71828;
	BOOST_AUTO(rw, cref(x1));
	cout << typeid(rw).name() << endl;
	cout << typeid(rws).name() << endl;
}
int main(int argc, char* argv[]){
	test_result_of();
	test_call_func();
	test_reference_wrapper();
	return 0;
}
