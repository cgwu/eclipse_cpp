/*
 * python_global_interpreter_lock.hpp
 *
 *  Created on: 2016Äê3ÔÂ11ÈÕ
 *      Author: cg
 */

struct python_global_interpreter_lock{
	python_global_interpreter_lock(){
		gil_state = PyGILState_Ensure();
	}
	~python_global_interpreter_lock(){
			PyGILState_Release(gil_state);
	}
private:
	PyGILState_STATE gil_state;
};
