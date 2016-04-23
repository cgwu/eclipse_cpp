#ifndef COMPOSITE_CPP
#define COMPOSITE_CPP
#include <iostream>
#include <list>
#include "component.cpp"
using namespace std;

class Composite : public Component
{
public:
    virtual void Operation()
    {
        cout << hex <<this << ": Composite operation()." << endl;
        for(Component *comp : _list)
        {
            comp->Operation();
        }
    }
    virtual void Add(Component * comp)
    {
        _list.push_back(comp);
    }
    virtual void Remove(Component * comp)
    {
        _list.remove(comp);
    }
    //virtual ~Composite(){}
private:
    list<Component*> _list;
};

#endif
