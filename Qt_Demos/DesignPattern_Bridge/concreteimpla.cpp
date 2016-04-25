#ifndef ConcreteImplA_cpp
#define ConcreteImplA_cpp
#include <iostream>
#include "implementor.cpp"
using namespace std;

class ConcreteImplA : public Implementor
{
public:
    virtual void OperationImpl()
    {
        cout << "ConcreteImplA OperationImpl() called. new added." << endl;
    }
};

#endif
