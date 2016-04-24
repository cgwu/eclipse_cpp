#include <iostream>
#include "adapter.cpp"
#include "adaptee.cpp"
using namespace std;

int main()
{
    Adaptee * adaptee = new Adaptee();
    Target * target = new Adapter(adaptee);
    target->Request();
    delete adaptee;
    delete target;
    return 0;
}

