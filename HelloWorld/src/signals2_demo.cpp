/*
 * signals2_demo.cpp
 *
 *  Created on: 2016Äê3ÔÂ3ÈÕ
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/signals2.hpp>
using namespace boost::signals2;

namespace test_slot{
void slots1(){
	cout << "slot1 called" << endl;
}
void slots2(){
	cout << "slot2 called" << endl;
}
void test_slots(){
	signal<void()> sig;
	sig.connect(&slots1);
	sig.connect(&slots2);
	sig();
}
}

int main(int argc, char* argv[]){
	test_slot::test_slots();
	return 0;
}
