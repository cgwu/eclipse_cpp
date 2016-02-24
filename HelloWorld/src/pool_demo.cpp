/*
 * pool_demo.cpp
 *
 *  Created on: 2016Äê2ÔÂ24ÈÕ
 *      Author: cg
 */


#include "stdcpp.hpp"
#include <boost/pool/object_pool.hpp>
#include <boost/pool/singleton_pool.hpp>
using namespace boost;

struct foo_class{
	int a,b,c;
	foo_class(int x = 1, int y=2, int z=3):a(x),b(y),c(z) {
		cout <<"foo_class@" << this << " construct." << endl;
	}
	void sayhi(){
		cout << this << "=>foo_class say hi." << endl;
	}
	~foo_class()
	{
		cout <<"foo_class@" << this << " destruct." << endl;
	}
};
void test_object_pool(){
	object_pool<foo_class> pl;
	foo_class *p1 = pl.construct(11,22,33);
	foo_class *p2 = pl.construct(11,22,33);
	cout << "test_object_pool() end" << endl;
}

typedef singleton_pool<foo_class, sizeof(foo_class)> single_foo;
void test_singleton_pool(){
	cout << "test_singleton_pool begin" << endl;
	foo_class *p1 = (foo_class*)single_foo::malloc();
	p1->sayhi();
	foo_class *p2 = (foo_class*)single_foo::malloc();
	p2->sayhi();
	single_foo::purge_memory();
}
int main_pool_demo(int argc, char* argv[]){
	cout <<"------------" << endl;
	test_object_pool();
	cout <<"------------" << endl;
	test_singleton_pool();
	cout << "end" << endl;
	return 0;
}
