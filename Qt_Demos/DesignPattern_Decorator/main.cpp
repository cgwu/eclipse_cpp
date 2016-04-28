#include <iostream>
#include "concretedecoratora.cpp"
#include "concretedecoratorb.cpp"
#include "concretecomponent.cpp"
using namespace std;

int main()
{
    Component * comp = new ConcreteComponent();
    Decorator * db = new ConcreteDecoratorB(comp);
    ConcreteDecoratorA * da = new ConcreteDecoratorA(db);
    da->SetState(123);

    da->Operation();

    delete da;
    delete db;
    delete comp;
    return 0;
}
