#ifndef ABSTRACTION_CPP
#define ABSTRACTION_CPP
#include "Implementor.cpp"
class Abstraction
{
public:
    virtual ~Abstraction(){}

    void SetImpl(Implementor * impl)
    {
        _impl = impl;
    }

    void Operation(){
        _impl->OperationImpl();
    }
private:
    Implementor * _impl;
};


#endif
