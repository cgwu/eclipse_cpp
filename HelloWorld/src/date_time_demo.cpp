/*
 * date_time_demo.cpp
 *
 *  Created on: 2016年2月23日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/date_time/gregorian/gregorian.hpp>
#include <boost/date_time/posix_time/posix_time.hpp>
#include <boost/thread.hpp>

void test_gregorian(){
	using namespace boost::gregorian;
	date d1 = from_string("1988-01-17");
	date d2(from_string("2005/01/02"));
	date d3 = from_undelimited_string("20110304");

	cout << to_simple_string(d1) << endl;
	cout << to_iso_string(d2) << endl;
	cout << to_iso_extended_string(d3) << endl;
}

void test_posix_time(){
	using namespace boost::posix_time;
	time_duration td(1,10,30,1000);
	cout << to_simple_string(td) << endl;
	cout << to_iso_string(td) << endl;
}

using namespace boost::posix_time;
template<typename Clock = microsec_clock>
class basic_ptimer{
public:
	basic_ptimer(){
		restart();
	}
	void restart(){
		_start_time = Clock::local_time();
	}
	void elapsed() const{
		cout << "ptimer精确计时:" << Clock::local_time() - _start_time << endl;
	}
	~basic_ptimer(){
		elapsed();
	}
private:
	ptime _start_time;
};


void test_ptimer(){
	typedef basic_ptimer<microsec_clock> ptimer;
	ptimer t;
	//boost::this_thread::sleep(boost::posix_time::seconds(2));
	boost::this_thread::sleep(boost::posix_time::milliseconds(2450));
	//boost::thread::sleep(boost::get_system_time() + boost::posix_time::milliseconds(243));
}
int main_date_time_demo()
{
	test_gregorian();
	test_posix_time();
	test_ptimer();

	return 0;
}

