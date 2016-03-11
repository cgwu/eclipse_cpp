/*
 * python_module_demo.hpp
 *
 *  Created on: 2016年3月11日
 *      Author: cg
 *
 *
 * 编译命令，模块仍有问题，初始函数init function.
   g++ -o first_module.pyd python_module_demo.hpp -shared -I"E:/Lib/boost/boost_1_58_0" -I"C:/DevProg/Python34-x86/include" -L"E:/Lib/boost/boost_1_58_0/stageMinGW/lib" -L"C:/DevProg/Python34-x86/libs" -lboost_python3-mgw49-mt-1_58.dll -lpython34
 * 另外的方式：
 * 1. SWIG
 * http://www.swig.org/
 * http://www.ibm.com/developerworks/cn/aix/library/au-swig/
 * 2. Cython
 * http://cython.org/
 * Cython is an optimising static compiler for both the Python programming language and the extended Cython programming language (based on Pyrex). It makes writing C extensions for Python as easy as Python itself.
 * 3. PyCXX
 * http://cxx.sourceforge.net/
 * PyCXX is designed to make it easier to extend Python with C++
 */

#include "stdcpp.hpp"
#include <boost/python.hpp>
// 第一行必须是#include <boost/python.hpp>
// 否则会留下一点小问题
#include <vector>

extern "C" {
void PyInit_first_module() {
	cout << "first python module inited!" << endl;
}
}

// 输出字符串
char const* greet() {
	return "hello, world";
}

// 实现两个数字相加
int add(int x, int y) {
	return x + y;
}

// 打印vector的函数
void vprint() {
	std::vector<int> myvector;
	for (int i = 1; i <= 5; i++) {
		myvector.push_back(i);
	}
	std::vector<int>::iterator it;
	std::cout << "myvector contains:";
	for (it = myvector.begin(); it < myvector.end(); it++) {
		std::cout << " " << *it;
	}
	std::cout << std::endl;

}

// 定义python模块的接口文件
BOOST_PYTHON_MODULE(first_module)
{ // hello_ext为导出python模块的名字
	using namespace boost::python;
//	def("init", PyInit_first_module); // 导出函数greet
	def("greet", greet); // 导出函数greet
	def("add", add); // 导出函数add
	def("vprint", vprint); // 导出函数vprint
}
