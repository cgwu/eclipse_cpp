#ifndef CONCRETE_STATE_B
#define CONCRETE_STATE_B
#include "state.cpp"
#include <iostream>
using namespace std;

class ConcreteStateB : public State
{
public:
    virtual void Handle()
    {
        cout << "Concrete State B Handle()." << endl;
    }
};

#endif
