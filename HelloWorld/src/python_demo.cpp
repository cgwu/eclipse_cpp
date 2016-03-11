/*
 * python_demo.cpp
 *
 *  Created on: 2016Äê3ÔÂ11ÈÕ
 *      Author: cg
 */

#include "stdcpp.hpp"
#include "pyinit.hpp"

int main_python_demo(int argc, char* argv[]){
	pyinit init;
	object i(10);
	i = 10 * i;
	cout<<extract<int>(i)<<endl;
	/*
	PyRun_SimpleString("from time import time,ctime\n"
	"print 'Today is',ctime(time())\n");
	*/
	cout << init.version() << endl;
	PyRun_SimpleString("for x in range(1,10): print(x);\n");
	PyRun_SimpleString("print(\"Hello world.\")");

	return 0;
}
