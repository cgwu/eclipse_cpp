#ifndef CONCRETE_OBSERVER_B_CPP
#define CONCRETE_OBSERVER_B_CPP
#include <iostream>
#include "observer.cpp"
#include "concretesubject.cpp"
using namespace std;

class ConcreteObserverB : public Observer
{
public:
    virtual void Update (Subject * subject)
    {
        ConcreteSubject * cb = dynamic_cast<ConcreteSubject*>(subject);
        StateType state = cb->GetState();
        cout << "ConcreteObserverB Get ConcreteSubject.State:" << state << endl;
    }
};

#endif
