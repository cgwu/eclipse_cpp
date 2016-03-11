/*
 * python_module_demo.hpp
 *
 *  Created on: 2016��3��11��
 *      Author: cg
 *
 *
 * �������ģ���������⣬��ʼ����init function.
   g++ -o first_module.pyd python_module_demo.hpp -shared -I"E:/Lib/boost/boost_1_58_0" -I"C:/DevProg/Python34-x86/include" -L"E:/Lib/boost/boost_1_58_0/stageMinGW/lib" -L"C:/DevProg/Python34-x86/libs" -lboost_python3-mgw49-mt-1_58.dll -lpython34
 * ����ķ�ʽ��
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
// ��һ�б�����#include <boost/python.hpp>
// ���������һ��С����
#include <vector>

extern "C" {
void PyInit_first_module() {
	cout << "first python module inited!" << endl;
}
}

// ����ַ���
char const* greet() {
	return "hello, world";
}

// ʵ�������������
int add(int x, int y) {
	return x + y;
}

// ��ӡvector�ĺ���
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

// ����pythonģ��Ľӿ��ļ�
BOOST_PYTHON_MODULE(first_module)
{ // hello_extΪ����pythonģ�������
	using namespace boost::python;
//	def("init", PyInit_first_module); // ��������greet
	def("greet", greet); // ��������greet
	def("add", add); // ��������add
	def("vprint", vprint); // ��������vprint
}
