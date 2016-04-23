#ifndef COMPONENT_CPP
#define COMPONENT_CPP

class Component
{
public:
    virtual ~Component(){}
    virtual void Operation()=0;
    virtual void Add(Component *)=0;
    virtual void Remove(Component *)=0;
};

#endif
