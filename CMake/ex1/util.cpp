/*
 * util.cpp
 *
 *  Created on: 2016��5��25��
 *      Author: geeks2016
 */

//_Pragma("once");  // ��C++11��_Pragma�Ǹ����������ȼ��� #progma once
#pragma once

#include <cstdio>
// �ɱ䳤�����궨���Լ� __VA_ARGS__
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



