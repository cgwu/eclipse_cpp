#include "../../dlls/dll1/dll1.h"
#include <iostream>
#include <stdio.h>
int main()
{
    dll1 lDll1;
    std::cout << lDll1.getText();
	getchar();
    return 0;
}
