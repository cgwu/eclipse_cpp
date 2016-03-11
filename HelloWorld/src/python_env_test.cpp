/*
 * python_env_test.cpp
 *
 *  Created on: 2016Äê3ÔÂ11ÈÕ
 *      Author: cg
 */

#include <Python.h>
#include "stdcpp.hpp"
#include "python_interpreter.hpp"
#include "python_global_interpreter_lock.hpp"
#include <boost/python.hpp>
namespace bp = boost::python;

namespace python_env_test{
	void test_env(){
		Py_Initialize();
		bp::object sys = bp::import("sys");
		bp::object version = sys.attr("version");
		cout << bp::extract<string>(version)() << endl;
//		bp::object path = sys.attr("path");
//		cout << bp::extract<>(path)() << endl;
		Py_Finalize();
	}

	void test_python_interpreter(){
		python_interpreter inter;
		python_global_interpreter_lock lock_inter;
		PyRun_SimpleString("for x in range(1,10): print(x);\n");
		PyRun_SimpleString("print(\"Hello: Python interpreter.\")");
	}
}

int main(int argc, char* argv[]){
	python_env_test::test_env();
	cout << "-------------" << endl;
	python_env_test::test_python_interpreter();
	return 0;
}
