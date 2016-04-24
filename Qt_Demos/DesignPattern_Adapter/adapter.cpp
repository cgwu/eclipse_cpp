#ifndef ADAPTER_CPP
#define ADAPTER_CPP
#include "adaptee.cpp"
#include "target.cpp"

class Adapter : public Target
{
public:
    Adapter(Adaptee * adaptee) : _adaptee(adaptee) {}
    virtual void Request()
    {
        _adaptee->SpecificRequest();
    }
private:
    Adaptee * _adaptee;
};

#endif
