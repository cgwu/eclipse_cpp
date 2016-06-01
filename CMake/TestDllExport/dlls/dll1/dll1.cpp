#include "dll1.h"
#include <iostream>
using namespace std;

dll1::dll1() {}
dll1::~dll1() {}

const char* dll1::getText()
{
    cout << "dll1 get text() called." << endl;
	return "Hello World";
}

void FN_DLL1_FOO()
{
	cout << "dll1 foo() called." << endl;
}
