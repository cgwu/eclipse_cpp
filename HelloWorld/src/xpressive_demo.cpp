/*
 * xpressive_demo.cpp
 *
 *  Created on: 2016Äê2ÔÂ26ÈÕ
 *      Author: cg
 */


#include "stdcpp.hpp"
#include <boost/xpressive/xpressive_dynamic.hpp>
using namespace boost::xpressive;

int main_xpressive_demo(int argc, char* argv[]){
	cregex reg = cregex::compile("a.c");
	cout << boolalpha << regex_match("abc",reg) << endl;
	cout << boolalpha << regex_match("a.c",reg) << endl;

	cout << boolalpha << regex_match("ac",reg) << endl;
	cout << boolalpha << regex_match("abd",reg) << endl;
	return 0;
}
