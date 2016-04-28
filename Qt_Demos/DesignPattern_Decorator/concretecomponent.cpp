#ifndef ConcreteComponent_cpp
#define ConcreteComponent_cpp
#include <iostream>
#include "component.cpp"
using namespace std;
class ConcreteComponent : public Component
{
public:
    virtual void Operation(){
        cout << "ConcreteComponent.Operation()" << endl;
    }
};

#endif
