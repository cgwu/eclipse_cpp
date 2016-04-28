#ifndef COMPONENT_CPP
#define COMPONENT_CPP

class Component
{
public:
    virtual ~Component(){}
    virtual void Operation() = 0;
};

#endif
