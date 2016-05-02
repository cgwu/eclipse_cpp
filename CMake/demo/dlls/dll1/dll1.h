#ifndef DLL1_H
#define DLL1_H

#ifdef WIN32
    #define FNDT_DLL_EXPORT __declspec (dllexport)
#else
    #define FNDT_DLL_EXPORT
#endif

class FNDT_DLL_EXPORT dll1
{
public:
    dll1();
    ~dll1();
    const char* getText();
private:

};
#endif
