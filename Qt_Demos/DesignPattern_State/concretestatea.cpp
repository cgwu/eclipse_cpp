#ifndef CONCRETE_STATE_A
#define CONCRETE_STATE_A
#include "state.cpp"
#include <iostream>
using namespace std;

class ConcreteStateA : public State
{
public:
    virtual void Handle()
    {
        cout << "Concrete State A Handle()." << endl;
    }
};

#endif
