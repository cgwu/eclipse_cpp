#pragma once
#include <iostream>
#include <tuple>
#include <bitset>

using namespace std;

/*********  Variadic Templates(数量不定的模板参数)  **********/
/*
// 一、采用参数分解递归的方式
//1.
void print() {}

//2. 与3相比，比较特化。
template <typename T, typename... Types>
void print(const T& firstArg, const Types&... args)
{
	cout << "sizeof args =(" << sizeof...(args) << "): " << firstArg << endl;
	print(args...);
}

//3. 此模板函数未调用到。
template <typename T, typename... Types>
void print(const Types&... args)
{
	cout << "#sizeof args =(" << sizeof...(args) << "): " << firstArg << endl;
	print(args...);
}
*/
/**********************/
// 一、采用模板常量递归的方式

// boost: util/printtuple.hpp
template<int IDX, int MAX, typename... Args>
struct PRINT_TUPLE
{
	static void print(ostream& os, const tuple<Args...>& t)
	{
		os << get<IDX>(t) << (IDX + 1 == MAX ? "" : ",");
		PRINT_TUPLE<IDX + 1, MAX, Args...>::print(os, t);
	}
};
// 终结条件
template<int MAX, typename... Args>
struct PRINT_TUPLE<MAX,MAX,Args...>
{
    static void print(ostream& , const tuple<Args...>& ) { }
};

// output perator for tuples
template<typename... Args>
ostream& operator<<(ostream& os, const tuple<Args...>& t)
{
    os << "[";
    PRINT_TUPLE<0,sizeof...(Args), Args...>::print(os, t);
    return os << "]";
}

void VariadicTemplates() {
	//1. Variadic Templates
    //print(7.5, "hello中国", bitset<16>(377), 42);

	auto t1 = make_tuple(17.5, "1hello中国", bitset<16>(377), 142);
    cout << t1 << endl;

	cout << endl;
}
