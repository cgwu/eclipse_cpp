/*
 * utility_demo.cpp
 *
 *  Created on: 2016年2月24日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/utility.hpp>
#include <boost/smart_ptr.hpp>
#include <boost/typeof/typeof.hpp>
using namespace boost;

class do_not_copy : noncopyable
{
};
int main_utility(int argc, char* argv[]){
	do_not_copy d1;
	//do_not_copy d2(d1);		// ERROR
	do_not_copy d3;
	//d3 = d1;					// ERROR
	//do_not_copy d4 = d1;		// ERROR

	BOOST_AUTO(x, make_shared<int>(10));		// 自动类型推断
	assert(*x == 10);
	cout << *x << endl;

	BOOST_TYPEOF(2.0*3) d = 2.0 * 3;			// 类型推断
	cout << d << endl;

	return 0;
}
