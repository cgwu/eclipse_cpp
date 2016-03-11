/*
 * pyinit.hpp
 *
 *  Created on: 2016Äê3ÔÂ11ÈÕ
 *      Author: cg
 */

#include <boost/python.hpp>
#include <boost/noncopyable.hpp>
#include <cstdlib>
using namespace boost::python;

class pyinit: boost::noncopyable{
public:
    pyinit(int initsigs = 1){
        assert(initsigs == 0 || initsigs == 1);
        Py_InitializeEx(initsigs);
        //Py_Initialize();
    }
    ~pyinit(){
        Py_Finalize();
    }

    bool isInitialized(){
        return Py_IsInitialized();
    }
    const char* version(){
        return Py_GetVersion();
    }
};
