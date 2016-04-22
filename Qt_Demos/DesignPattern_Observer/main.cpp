#include <iostream>
#include "concretesubject.cpp"
#include "concreteobservera.cpp"
#include "concreteobserverb.cpp"
using namespace std;

int main()
{
    ConcreteSubject * subject = new ConcreteSubject();

    Observer * observer = new ConcreteObserverA();
    Observer * sb = new ConcreteObserverB();
    subject->Attach(observer);
    subject->Nofity();

    subject->SetState(110);
    subject->Nofity();

    cout << "Attach sb" << endl;
    subject->Attach(sb);
    subject->Nofity();

    cout << "Detach sb" << endl;
    subject->Detach(sb);
    subject->Nofity();

    delete subject;
    delete observer;
    return 0;
}
