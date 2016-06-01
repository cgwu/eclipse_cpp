#ifndef DLL1_H
#define DLL1_H

#define FN_DLL1_FOO d_1

#ifdef WIN32
    #define FNDT_DLL_EXPORT __declspec (dllexport)
	#pragma message ("WIN32被定义")	// MSVC,GCC
	//#warning "WIN32被定义"
#else
    #define FNDT_DLL_EXPORT
	#pragma message ("WIN32未被定义")
#endif

class FNDT_DLL_EXPORT dll1
{
public:
    dll1();
    ~dll1();
    const char* getText();
private:

};

FNDT_DLL_EXPORT void FN_DLL1_FOO();
//void f() __attribute__ ((weak, alias ("dll1_foo")));

#endif
