
#ifndef CONCRETE_VISITOR1_CPP
#define CONCRETE_VISITOR1_CPP
#include "visitor.cpp"
#include <iostream>
using namespace std;

class ConcreteVisitor1 : public Visitor
{
public:
    virtual void VisitConcreteElement1(ConcreteElement1 *element)
    {
        cout << "Concrete Visitor 1 is visiting Concrete Element 1" << endl;
    }

    virtual void VisitConcreteElement2(ConcreteElement2 *element)
    {
        cout << "Concrete Visitor 1 is visiting Concrete Element 2" << endl;
    }
};

#endif

