#ifndef ConcreteImplB_cpp
#define ConcreteImplB_cpp
#include <iostream>
#include "implementor.cpp"
using namespace std;

class ConcreteImplB : public Implementor
{
public:
    virtual void OperationImpl()
    {
        cout << "ConcreteImplB OperationImpl() called. new added." << endl;
    }
};

#endif
