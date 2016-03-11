/*
 * thread_future_demo.cpp
 *
 *  Created on: 2016年3月10日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/thread.hpp>
#include <boost/array.hpp>
#include <boost/foreach.hpp>
using namespace boost;

namespace test_future{
	int fab(int n){
		if(n==0 || n==1) return 1;
		else return fab(n-2) + fab(n-1);
	}

	void test_packaged_task(){
		packaged_task<int> pt(bind(fab,10));	// packaged_task只接受无参函数
		unique_future<int> uf = pt.get_future();
		thread(boost::move(pt));	// packaged_task是不可拷贝的
		uf.wait();		//unique_future等待计算结果
		assert(uf.is_ready() && uf.has_value());
		cout << uf.get() << endl;
	}

	void test_many_packaged_task(){
		typedef packaged_task<int> pti_t;
		typedef unique_future<int> ufi_t;
		array<pti_t, 5> ap;
		array<ufi_t, 5> au;
		for(int i=0; i<5; ++i){
			ap[i] = pti_t(bind(fab, i+10));
			au[i] = ap[i].get_future();
			thread(move(ap[i]));
		}
		wait_for_all(au.begin(), au.end());
		BOOST_FOREACH(ufi_t& uf, au){
			cout << uf.get() << endl;
		}
	}
	void fab2(int n, promise<int> *p){
		p->set_value(fab(n));
	}
	void test_promise(){
		promise<int> p;
		unique_future<int> uf = p.get_future();
		thread(fab2, 10, &p);
		uf.wait();
		cout << uf.get() << endl;
	}
	void end_thread_msg(const string& msg){
		cout << "thread " << this_thread::get_id() <<" exit:" << msg << endl;
	}
	mutex io_mu;
	void printing(){
		thread_specific_ptr<int> pi;
		pi.reset(new int());	// 与下一行的区别，int()会调用默认构造初始
//		pi.reset(new int);
		++(*pi);
		mutex::scoped_lock lock(io_mu);
		cout << "thread v=" << *pi << endl;
		this_thread::at_thread_exit(bind(end_thread_msg, "线程退出了"));
	}
	void test_thread_specific(){
		thread t1(printing);
		thread t2(printing);
		this_thread::sleep(posix_time::seconds(1));
	}

}	// end namespace
int main_thread_future(int argc, char* argv[]){
	test_future::test_packaged_task();
	cout << "-----------" << endl;
	test_future::test_many_packaged_task();
	cout << "-----------" << endl;
	test_future::test_promise();
	cout << "-----------" << endl;
	test_future::test_thread_specific();

	return 0;
}
