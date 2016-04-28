#ifndef concretedecoratorb_cpp
#define concretedecoratorb_cpp
#include <iostream>
#include "decorator.cpp"
using namespace std;

class ConcreteDecoratorB : public Decorator
{
public:
    ConcreteDecoratorB(Component * _comp) : Decorator::Decorator(_comp){}
    virtual void Operation(){
        cout << "ConcreteDecoratorB::Operation() begin" << endl;
        Decorator::Operation();
        cout << "ConcreteDecoratorB::Operation() end" << endl;
    }


};

#endif
