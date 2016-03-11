/*
 * thread_demo.cpp
 *
 *  Created on: 2016��3��7��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/thread.hpp>
#include <boost/date_time.hpp>
using namespace boost;

namespace test_thread{
/*
 * Ϊ�˸��ñ���ʱ����߳���غ��壬thread�����¶�����һ���µ�ʱ������
 * system_time,����posix_time::ptime��ͬ���:
 * typedef boost::posix_time::ptime system_time;
 */
void test_sleep(){
	cout << "begin sleep 5 seconds" << endl;
	this_thread::sleep(posix_time::seconds(5));
	cout << "end sleep 5 seconds" << endl;
}
/*
 * thread�ṩ�Ļ�����:
 * mutex		: ��ռʽ������
 * try_mutex
 * timed_mutex	: ��ռʽ�Ļ��������ṩ��ʱ��������
 * recursive_mutex
 * recursive_try_mutex
 * recursive_timed_mutex
 * shared_mutex (multiple-reader/single-writer �͵Ĺ����������ֳƶ�д��)
 *
 * mutex��ʹ���ڲ����Ͷ���scoped_lock��scoped_try_lock����������lock_guard����
 * boost/detail/atomic_count.hpp����һ��ԭ�Ӽ�����.
 */
mutex io_mu;
class rw_data
{
private:
	int m_x;
	shared_mutex rw_mu;
public:
	rw_data():m_x(0){}
	void write(){
		unique_lock<shared_mutex> ul(rw_mu); // write lock
		++m_x;
	}
	void read(int *x){
		shared_lock<shared_mutex> sl(rw_mu); // read lock
		*x = m_x;
	}
};
void writer(rw_data &d){
	for(int i=0;i < 20; ++i){
		this_thread::sleep(posix_time::milliseconds(10));
		d.write();
	}
}
void reader(rw_data &d){
	int x;
	for(int i=0;i < 10; ++i){
		this_thread::sleep(posix_time::milliseconds(5));
		d.read(&x);
		mutex::scoped_lock lock(io_mu);
		cout <<"#" << this_thread::get_id() << ", reader:" << x << endl;
	}
}
void test_read_write(){
	rw_data d;
	thread_group pool;
	pool.create_thread(bind(reader, ref(d)));
	pool.create_thread(bind(reader, ref(d)));
	pool.create_thread(bind(reader, ref(d)));
	pool.create_thread(bind(reader, ref(d)));

	pool.create_thread(bind(writer, ref(d)));
	pool.create_thread(bind(writer, ref(d)));
	pool.join_all();
}
typedef mutex mutex_t;
template<typename T>
class basic_atom: noncopyable
{
private:
	T n;

	mutex_t mu;
public:
	basic_atom(T x = T()) : n(x) {}
	T operator++ ()
	{
		mutex_t::scoped_lock lock(mu);
		return ++n;
	}
	operator T() {return n;}
};
typedef basic_atom<int> atom_int;

void printing(atom_int& x, const string& str){
	for(int i=0; i<5; ++i){
		mutex::scoped_lock lock(io_mu);
		cout << str << ++x << endl;
//		this_thread::yield();
	}
}
void test_atom_int(){
	atom_int x;
	thread t1(printing, ref(x), "#hello");
	thread t2(printing, ref(x), "@boost");
//	this_thread::sleep(posix_time::seconds(2));

//	t1.timed_join(posix_time::seconds(1));
//	t2.join();

	t1.detach();
	t2.join();
}
/*
 * ʹ��bind��function
 * thread t3(bind(printing, ref(x), "thread");
 * function<void()> f = bind(printing, 5, "mutex");
 * thread(f);
 * �����̵߳ĺ�����
 * yield(), sleep(), hardware_concurrency()
 * thread_group
 */
void test_thread_group(){
	thread_group tg;
	atom_int x;
	tg.create_thread(bind(printing, ref(x), "@C++"));
	tg.create_thread(bind(printing, ref(x), "#BOOST"));
	tg.join_all();
}
}
int main_thread_demo(int argc, char* argv[]){
//	test_thread::test_sleep();
	test_thread::test_atom_int();
	cout << "---------- thread_group--------" << endl;
	test_thread::test_thread_group();
	cout << "---------- test_read_write--------" << endl;
	test_thread::test_read_write();
	return 0;
}
