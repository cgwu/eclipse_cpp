/*
 * python_interpreter.hpp
 * Thread-aware Interpreter
 *  Created on: 2016Äê3ÔÂ11ÈÕ
 *      Author: cg
 */
#include <Python.h>

struct python_interpreter{
	python_interpreter(bool signals_handled = false){
		Py_InitializeEx(signals_handled);
		thread_state = PyEval_SaveThread();
	}
	~python_interpreter(){
		PyEval_RestoreThread(thread_state);
		Py_Finalize();
	}
private:
	PyThreadState * thread_state;
};
