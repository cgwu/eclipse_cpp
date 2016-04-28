#ifndef concretedecoratora_cpp
#define concretedecoratora_cpp
#include <iostream>
#include "decorator.cpp"
using namespace std;

class ConcreteDecoratorA : public Decorator
{
public:
    ConcreteDecoratorA(Component * _comp) : Decorator::Decorator(_comp){}
    virtual void Operation(){
        cout << "ConcreteDecoratorA::Operation() begin:" << _state << endl;
        Decorator::Operation();
        cout << "ConcreteDecoratorA::Operation() end:" << _state << endl;
    }

    void SetState(int state){
        _state = state;
    }

private:
    int _state;
};

#endif
