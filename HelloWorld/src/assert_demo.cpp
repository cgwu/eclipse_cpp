/*
 * assert_demo.cpp
 *
 *  Created on: 2016��2��27��
 *      Author: cg
 */

#include "stdcpp.hpp"

#define BOOST_DISABLE_ASSERTS		// ����BOOST���ԣ��鲻�����c assert.
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
