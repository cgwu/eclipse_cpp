/*
 * util.cpp
 *
 *  Created on: 2016年5月25日
 *      Author: geeks2016
 */

//_Pragma("once");  // 在C++11中_Pragma是个操作符，等价于 #progma once
#pragma once

#include <cstdio>
// 可变长参数宏定义以及 __VA_ARGS__
#define PR(...) printf(__VA_ARGS__)
#define LOG(...) {\
	fprintf(stderr, "%s: Line %d:\t", __FILE__, __LINE__);\
	fprintf(stderr, __VA_ARGS__);\
	fprintf(stderr, "\n" ); \
}

#include <iostream>
using namespace std;

void foo()
{
	cout << "wrapped foo called v2. !!!!" << endl;
}



