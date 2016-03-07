/*
 * signals2_demo.cpp
 *
 *  Created on: 2016��3��3��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/signals2.hpp>
#include <boost/smart_ptr.hpp>
#include <boost/ref.hpp>
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
	sig.connect(&slots2, at_front);
	sig();
}

template<int N>
struct slots
{
	void operator() () {
		cout << "slot" << N << " called." << endl;
	}
};
template<int N>
bool operator== (const slots<N>&, const slots<N>&){
	return true;
}

void test_slots_template(){
	slots<1234>()();			//�����slot1234 called.  slots<1234>()����һ����������
	signal<void()> sig;
	sig.connect(slots<1>(), at_back);
	sig.connect(slots<100>(), at_front);

	sig.connect(5, slots<51>(), at_back);	//���5
	sig.connect(5, slots<55>(), at_front);

	sig.connect(3, slots<30>(), at_back);
	sig.connect(3, slots<33>(), at_front);

	sig.connect(10,slots<10>());
	sig();
}
void test_slots_connect_disconnect(){
	signal<void(void)> sig;
	assert(sig.empty());
	sig.connect(0,slots<10>());
	sig.connect(0,slots<20>());
	sig.connect(1,slots<30>());
	assert(sig.num_slots()==3);
	sig.disconnect(0);		//�Ͽ���0���ۣ���2��
//	assert(sig.num_slots()==1);
//	{
//		//error: 'boost::signals2::scoped_connection::scoped_connection(const boost::signals2::scoped_connection&)' is private
//		scoped_connection sc = sig.connect(0, slots<22>());
//		assert(sig.num_slots() == 2);
//		sig();
//	}
	assert(sig.num_slots() == 1);
	sig.disconnect(slots<30>());
	assert(sig.empty());
	cout <<"success test_slots_connect_disconnect." << endl;
}

template<int N>
struct slots_n
{
	int operator() (int num) {
		cout << "slot" << N << " called." << endl;
		return N * num;
	}
};

void test_slots_n_template(){
	signal<int(int)> sig;
	connection c1 = sig.connect(slots_n<10>());
	connection c2 = sig.connect(slots_n<20>());
	connection c3 = sig.connect(slots_n<50>());
	// sig�����ز������ĩβslos<50>()�ļ�����������һ��optional����
	// �����ý����ò�����*�����ֵ
	cout << "begin block..." << endl;
	{
		shared_connection_block block(c2);
		assert(c2.blocked());
		sig(3);
	}
	cout << "end block..." << endl;
	cout << *sig(2) << endl;
}

void test_auto_slot(){
	using namespace boost;
	typedef signal<int(int)> signal_t;
	signal_t sig;
	sig.connect(slots_n<10>());		//����һ����ͨ��slot
	shared_ptr<slots_n<20> > p(new slots_n<20>);
	// ע��slot_type���÷�
	sig.connect(signal_t::slot_type(boost::ref(*p)).track(p));
	assert(sig.num_slots() == 2);
	sig(2);
	cout << "after shared_ptr reset." << endl;
	p.reset();
	assert(sig.num_slots() == 1);
	sig(1);
}



}

int main(int argc, char* argv[]){
	test_slot::test_slots();
	cout << "-------------" << endl;
	test_slot::test_slots_template();
	cout << "------test_slots_n_template-------" << endl;
	test_slot::test_slots_n_template();
	cout << "-------test_slots_connect_disconnect------" << endl;
	test_slot::test_slots_connect_disconnect();
	cout << "------test_auto_slot-------" << endl;
	test_slot::test_auto_slot();

	return 0;
}
