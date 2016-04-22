#ifndef CONCRETE_OBSERVER_A_CPP
#define CONCRETE_OBSERVER_A_CPP
#include <iostream>
#include "observer.cpp"
#include "concretesubject.cpp"
using namespace std;

class ConcreteObserverA : public Observer
{
public:
    virtual void Update (Subject * subject)
    {
        ConcreteSubject * cb = dynamic_cast<ConcreteSubject*>(subject);
        StateType state = cb->GetState();
        cout << "ConcreteObserverA Get ConcreteSubject.State:" << state << endl;
    }
};

#endif
