/*
 * singleton_demo.cpp
 *
 *  Created on: 2016年2月25日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/serialization/singleton.hpp>
using boost::serialization::singleton;

//#include <boost/pool/singleton_pool.hpp>
//using boost::details::pool:singleton_default;		// 已不存在

class point
{
public:
	int x,y,z;
	point(int a=0,int b=0,int c=0) : x(a),y(b),z(c) {
		cout << "point@" << this << " ctor"<< endl;
	}
	~point(){
		cout << "point@" << this << " dtor"<< endl;
	}
	void print() const{
		cout << "(" << x <<"," << y <<"," << z << ")" << endl;
	}
};
//使用方法2:
class point_ex : public singleton<point_ex>
{
public:
	int x,y,z;
	point_ex(int a=0,int b=0,int c=0) : x(a),y(b),z(c) {
		cout << "point_ex@" << this << " ctor"<< endl;
	}
	~point_ex(){
		cout << "point_ex@" << this << " dtor"<< endl;
	}
	void print() const{
		cout << "(" << x <<"," << y <<"," << z << ")" << endl;
	}
};

int main_singleton_demo(int argc, char* argv[]){
	cout << "main() start" << endl;
	typedef singleton<point> origin;
	origin::get_const_instance().print();
	origin::get_mutable_instance().print();

	origin::get_mutable_instance().x = 1;
	origin::get_mutable_instance().y = 2;
	origin::get_mutable_instance().z = 3;

	origin::get_const_instance().print();
	origin::get_mutable_instance().print();

	cout << "----------------" << endl;

	point_ex::get_const_instance().print();
	point_ex::get_mutable_instance().x = 1;
	point_ex::get_mutable_instance().y = 2;
	point_ex::get_mutable_instance().z = 3;
	point_ex::get_const_instance().print();

	cout << "main() end" << endl;
	return 0;
}



