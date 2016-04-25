#ifndef IMPLEMENTOR_CPP
#define IMPLEMENTOR_CPP

class Implementor
{
public:
    virtual ~Implementor(){}
    virtual void OperationImpl() = 0;
};

#endif
