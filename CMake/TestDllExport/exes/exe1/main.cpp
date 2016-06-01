#include "../../dlls/dll1/dll1.h"
#include <iostream>
#include <stdio.h>
using namespace std;

int main()
{
    dll1 lDll1;
    int a = 0;
    ++a;
    cout << "a=" << a << endl;
    std::cout << lDll1.getText()<< endl;
    cout << "---------------" << endl;
    FN_DLL1_FOO();
    cout << "Press ENTER to exit." << endl;
	getchar();
    return 0;
}
