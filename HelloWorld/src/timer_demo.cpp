/*
 * timer_demo.cpp
 *
 *  Created on: 2016��2��23��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/timer.hpp>
#include <boost/progress.hpp>
#include <boost/thread.hpp>
using namespace boost;
int main_timer_demo()
{
	timer t;	// ����һ����ʱ��������ʼ��ʱ
	cout << "max timespan:" << t.elapsed_max() / 3600 << "h" << endl;
	cout << "min timespan:" << t.elapsed_min() << "s" << endl;
	cout << "now time elapsed:" << t.elapsed() << "s" << endl;

//	{
//		boost::progress_timer t;
//	    boost::thread::sleep(boost::get_system_time() + boost::posix_time::milliseconds(2134));
//	}
//
//	{
//		boost::progress_timer t;
//	    boost::thread::sleep(boost::get_system_time() + boost::posix_time::milliseconds(1234));
//	}

	/*
	0%   10   20   30   40   50   60   70   80   90   100%
	|----|----|----|----|----|----|----|----|----|----|
	***************************************************
	*/
	const int size = 100;
	progress_display pd(size);	//��ʾ����
	for ( int i = 1; i <= size; ++ i) {
		boost::thread::sleep(boost::get_system_time() + boost::posix_time::milliseconds(100));
		++pd;
	}

	return 0;
}
