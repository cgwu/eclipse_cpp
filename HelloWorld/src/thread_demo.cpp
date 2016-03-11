/*
 * thread_demo.cpp
 *
 *  Created on: 2016年3月7日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/thread.hpp>
#include <boost/date_time.hpp>
using namespace boost;

namespace test_thread{
/*
 * 为了更好表述时间的线程相关含义，thread库重新定义了一个新的时间类型
 * system_time,它是posix_time::ptime的同义词:
 * typedef boost::posix_time::ptime system_time;
 */
void test_sleep(){
	cout << "begin sleep 5 seconds" << endl;
	this_thread::sleep(posix_time::seconds(5));
	cout << "end sleep 5 seconds" << endl;
}
/*
 * thread提供的互斥量:
 * mutex		: 独占式互斥量
 * try_mutex
 * timed_mutex	: 独占式的互斥量，提供超时锁定功能
 * recursive_mutex
 * recursive_try_mutex
 * recursive_timed_mutex
 * shared_mutex (multiple-reader/single-writer 型的共享互斥量，又称读写锁)
 *
 * mutex类使用内部类型定义scoped_lock和scoped_try_lock定义了两种lock_guard对象
 * boost/detail/atomic_count.hpp定义一个原子计数器.
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
 * 使用bind和function
 * thread t3(bind(printing, ref(x), "thread");
 * function<void()> f = bind(printing, 5, "mutex");
 * thread(f);
 * 操作线程的函数：
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
