/*
 * function_callback_demo.cpp
 *
 *  Created on: 2016年2月29日
 *      Author: cg
 */

#include <cassert>
#include <cstdio>
#include <cmath>
#include <typeinfo>
#include "stdcpp.hpp"
#include <boost/utility/result_of.hpp>
#include <boost/typeof/typeof.hpp>
#include <boost/ref.hpp>
#include <boost/assign.hpp>
#include <boost/bind.hpp>
#include <boost/foreach.hpp>
#include <boost/function.hpp>
using namespace boost;

void test_result_of(){
	typedef double (*Func) (double d);
	Func func = sqrt;
//	BOOST_AUTO( x , func(5.0));
	result_of<Func(double)>::type x = func(5.0);
	cout << typeid(x).name() << ":" << x << endl;
}

template<typename T, typename T1>
typename result_of<T(T1)>::type call_func(T t, T1 t1)	// 最前面的typename不可少，否则编译器会认为type是result_of静态成员变量
{
	return t(t1);
}
void test_call_func(){
	typedef double (*Func) (double d);
	Func func = sqrt;
	BOOST_AUTO( x , call_func(func, 4.0));
	cout << typeid(x).name() << ":" << x << endl;
}
void test_reference_wrapper(){
	int x = 10;
	reference_wrapper<int>rx(x);
	assert(x == rx);
	(int&)rx = 100;
	assert(x == 100);

	reference_wrapper<int>rx2(rx);
	cout << rx2.get() << endl;

	string str;
	reference_wrapper<string> rws(str);
	*rws.get_pointer() = "test reference_wrapper";
	cout << rws.get().size() << endl;

	double x1 = 2.71828;
	BOOST_AUTO(rw, cref(x1));
	cout << typeid(rw).name() << endl;
	cout << typeid(rws).name() << endl;
}

class big_class{
private:
	int x;
public:
	big_class():x(0){}
	void print(){
		cout << "big_class " << ++x << endl;
	}
};

template<typename T>
void print(T a){
	for(int i=0; i<2; ++i){
		unwrap_ref(a).print();
	}
}

void test_ref(){
	big_class c;
	BOOST_AUTO(rw, ref(c));
	c.print();
	print(c);
	print(rw);
	print(c);
	c.print();
}

struct square
{
	typedef void result_type;		//返回的结果类型定义，很重要
	void operator() (int& x){
		x = x*x;
	}
};
void test_ref_function(){
	using namespace boost::assign;
	typedef double (*pfunc)(double);
	pfunc pf = sqrt;
	cout << ref(pf)(5.0) << endl;

	square sq;
	int x = 5;
//	boost::ref(sq)(x);	// error: no match for call to '(const boost::reference_wrapper<square>) (int&)'
	sq(x);
	cout << x << endl;

//	vector<int> v = (list_of(1),2,3,4,5);
//	std::for_each(v.begin(), v.end(), boost::ref(sq));

}

namespace test_bind{
using namespace boost;
using namespace boost::assign;
int f(int a, int b){
	return a+b;
}
int g(int a, int b, int c){
	return a+b*c;
}
typedef int (*f_type) (int, int);
typedef int (*g_type) (int, int, int);

struct demo{
	int f(int a, int b){
		return a+b;
	}
};
struct point{
	int x,y;
	point(int a=0, int b=0):x(a),y(b){}
	void print(){
		cout << "(" << x << "," << y << ")" << endl;
	}
};
void print_i(int x){
	cout << x << endl;
}
struct f_obj{
	int operator() (int a, int b){
		return a+b;
	}
};
void test_bind_raw_func(){
	cout << "----绑定普通函数----" << endl;
	cout << bind(f, 1, 2)() << endl;
	cout << bind(g,1,2,3)() << endl;
	cout << bind(f, _1, 9)(10) << endl;
	cout << bind(f, _1, _2)(3,4) << endl;
	cout << bind(f, _2, _1)(5,6) << endl;
	cout << bind(g, _1, 8, _2)(1,2)<< endl;
	cout << bind(g, _3, _2, _2)(100,2,3) << endl;	// 第一个参数100被忽略
	cout << "----绑定函数指针----" << endl;
	f_type pf = f;
	g_type pg = g;
	cout << boost::bind(pf, _1, 9) (10) << endl;
	cout << bind(pg, _3, _2, _2) (1,2,3) << endl;
	cout << "----绑定成员函数----" << endl;
	demo a, &ra = a;	//类的实例和引用
	demo *p = &a;		//对象指针
	cout << bind(&demo::f, a, _1,20) (10) << endl;
	cout << bind(&demo::f, ra, _2, _1) (10,20) << endl;
	cout << bind(&demo::f, p, _1,_2) (10,20) << endl;
	cout << "----配合算法操作----" << endl;
//	vector<int> v = (list_of(1),2,3,4,5);
//	std::for_each(v.begin(), v.end(), bind(print_i, _1));
	vector<point> v(5);
	vector<int> vi(5);
	std::for_each(v.begin(), v.end(), bind(&point::print, _1));
	cout << "----绑定成员变量----" << endl;
	v[0].x = 6;
	v[4].x = 123;
	std::transform(v.begin(), v.end(), vi.begin(), bind(&point::x, _1));
	BOOST_FOREACH(int x, vi)
		cout << x << ",";
	cout << "----绑定函数对象functor ----" << endl;
	// 如果函数对象内部有类型定义result_type,则自动推导；若无，则在绑定形式上做一点改变
	// bind<result_type>(Functor, ...);
	cout << bind(std::greater<int>(), _1, 10) (11) << endl;
	cout << bind(std::greater<int>(), _1, 10) (9) << endl;
	cout << bind(std::plus<int>(), _1, _2) (11,22) << endl;
	cout << bind(std::modulus<int>(), _1, 3) (8) << endl;

	cout << bind<int>(f_obj(), _1, _2) (10, 20) << endl;
	f_obj fo;
	int ax = 123;
	cout << bind<int>(ref(fo), _1, cref(ax)) (10) << endl;
	cout << "----存储bind表达式 ----" << endl;
	BOOST_AUTO(f_gt, bind(greater<int>(), _1, _2) );
	cout << f_gt(10,20) << endl;

	BOOST_AUTO(f_pt, bind<int>(printf,"%d+%d=%d\n", _1, _2, _3) );
	f_pt(11,22, 11+22);
}
}

/*
 * function 是一个函数对象的容器，概念上像是C/C++中函数指针类型的泛化，是一种“智能函数指针”。
 * 它以对象的形式封装了原始的函数指针或函数对象，能够容纳任意符合函数签名的可调用对象。因此，
 * 它可以被用于回调机制，暂时保管函数或函数对象，在之后需要的时机再调用，使回调机制更有弹性。
 */
namespace test_function{
	using namespace boost;
	int f(int a, int b){
		return a+b;
	}

	struct demo_class{
		int add(int a, int b){
			return a+b;
		}
		int operator() (int x) const
		{
			return x * x;
		}
	};
	void test_f(){
		function<int(int,int)> func;
		assert(!func);
		func = f;
		if(func){
			cout << func(10,20) << endl;
		}
		func = 0;
		assert(func.empty());
		cout << "----成员函数----" << endl;
		function<int(demo_class&, int, int)> func1;
		func1 = bind(&demo_class::add, _1, _2, _3);
		demo_class sc;
		cout << func1(sc, 10, 20) << endl;
		cout << "----成员函数,法2----" << endl;
		function<int(int,int)> func2;
		func2 = bind(&demo_class::add, &sc, _1, _2);
		cout << func2(11,22) << endl;
	}

	template<typename T>
	struct summary{
		typedef void result_type;
		T sum;
		summary(T v = T()):sum(v){}
		void operator() (T const & x){
			sum += x;
		}
	};
	void test_state_functor(){
		using namespace boost::assign;
		vector<int> v= (list_of(1), 3, 5, 7, 9);
		summary<int> s;
		function<void(int const&)> func(ref(s));
		std::for_each(v.begin(), v.end(), func);
		cout << s.sum << endl;
	}
}
int main_function_callback_demo(int argc, char* argv[]){
	test_result_of();
	test_call_func();
	test_reference_wrapper();
	cout << "-------------" << endl;
	test_ref();
	cout << "-------------" << endl;
	test_ref_function();
	cout << "-------------" << endl;
	test_bind::test_bind_raw_func();
	cout << "-------------" << endl;
	test_function::test_f();
	test_function::test_state_functor();
	return 0;
}
