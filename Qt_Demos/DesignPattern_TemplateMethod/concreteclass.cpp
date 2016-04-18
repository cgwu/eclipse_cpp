
#ifndef CONCRETE_CLASS_CPP
#define CONCRETE_CLASS_CPP
#include "abstractclass.cpp"
#include <iostream>
using namespace std;
class ConcreteClass : public AbstractClass
{
public:
    virtual void PrimitiveOperation1()
    {
        cout << "Concrete Class Operation 1 called." << endl;
    }

    virtual void PrimitiveOperation2()
    {
        cout << "Concrete Class Operation 2 called." << endl;
    }
};

#endif
