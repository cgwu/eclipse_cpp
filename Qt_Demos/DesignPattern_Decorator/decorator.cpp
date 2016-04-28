#ifndef Decorator_cpp
#define Decorator_cpp

#include "Component.cpp"
class Decorator : public Component
{
public:
    Decorator(Component * comp) : _comp(comp) { }
    virtual void Operation()
    {
        _comp->Operation();
    }

private:
    Component * _comp;
};

#endif
