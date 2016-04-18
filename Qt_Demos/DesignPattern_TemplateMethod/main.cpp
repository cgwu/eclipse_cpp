#include <iostream>
#include "concreteclass.cpp"
using namespace std;

int main()
{
    AbstractClass *ac = new ConcreteClass();
    ac->TemplateMethod();

    return 0;
}

