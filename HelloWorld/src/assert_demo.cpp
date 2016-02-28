/*
 * assert_demo.cpp
 *
 *  Created on: 2016年2月27日
 *      Author: cg
 */

#include "stdcpp.hpp"

#define BOOST_DISABLE_ASSERTS		// 禁用BOOST断言，查不会禁用c assert.
#include <cassert>
#include <boost/assert.hpp>
//using namespace boost;
double func(int x)
{
	BOOST_ASSERT(x!=0 && "divide by zero");
	cout << "after BOOST_ASSERT" << endl;
	assert(x !=0 && "divide by zero cassert");
	cout << "after" << endl;
	return 1.0 / x;
}
int main_assert_demo(int argc, char* argv[]){
	func(0);
	return 0;
}
