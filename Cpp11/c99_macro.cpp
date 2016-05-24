#include <climits>
#include <cassert>
#include <iostream>
#include "util_wrap.h"
using namespace std;

// __func__Ԥ�����ʶ��,�������ں���������
const char* hello() { return __func__; }
const char* world() { return __func__; }

int positive(const int n)
{
	static_assert(sizeof(n)>0, "ֵ�������0");	// ��̬���Ա����ǳ������ʽ
	return n;
}
struct TestStruct{
	TestStruct() : name(__func__) {}
	const char *name;
};

void Throw() { throw 1;}
void NoBlockThrow() { Throw(); }
void BlockThrow() noexcept { Throw(); }		// ���������׳��쳣�������쳣����̷�������ֹ.

void test_throw()
{
	try{
		Throw();
	}
	catch(...){
		cout << "Found throw." << endl;
	}
	try{
		NoBlockThrow();
	}
	catch(...){
		cout << "Throw is not blocked." << endl;
	}
	try{
		//BlockThrow();
	}
	catch(...){
		cout << "Found throw 1." << endl;
	}
	cout << "test_throw() complete." << endl;
}

int main(int argc, char *argv[])
{
	cout << "Standard Clib: " << __STDC_HOSTED__ << endl;
	cout << "Standard C: " << __STDC__ << endl;
	//cout << "C Standard version:" << __STDC__VERSION__ << endl;
	//cout << "ISO/IEC " << __STDC_ISO_10646__ << endl;
	
	cout << hello() << ", " << world() << endl;
	
	TestStruct ts;
	cout << ts.name << endl;
	foo();
	LOG("This is my log message! �����ҵ���־��Ϣ");
	cout << "sizeof long:" << sizeof(long) << endl;
	cout << "sizeof longlong:" << sizeof(long long) << endl;
	
	long long ll_min = LLONG_MIN;
	long long ll_max = LLONG_MAX;
	unsigned long long ull_max = ULLONG_MAX;
	printf("%lld\n", ll_min);
	printf("%lld\n", ll_max);
	printf("%llu\n", ull_max);
	
	cout << "__cplusplus:" << __cplusplus << endl;
	positive(1);
	
	test_throw();
	
	return 0;
}
