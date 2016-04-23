#ifndef LEAF_CPP
#define LEAF_CPP
#include <iostream>
#include "component.cpp"
using namespace std;

class Leaf : public Component
{
public:
    virtual void Operation()
    {
        cout << hex <<this << ": leaf operation()." << endl;
    }
    virtual void Add(Component *)
    {

    }
    virtual void Remove(Component *)
    {

    }
    //virtual ~Leaf(){}
};

#endif
