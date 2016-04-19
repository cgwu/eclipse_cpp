

#ifndef CONCRETE_VISITOR2_CPP
#define CONCRETE_VISITOR2_CPP
#include "visitor.cpp"
#include <iostream>
using namespace std;

class ConcreteVisitor2 : public Visitor
{
public:
    virtual void VisitConcreteElement1(ConcreteElement1 *element)
    {
        cout << "Concrete Visitor 2 is visiting Concrete Element 1" << endl;
    }

    virtual void VisitConcreteElement2(ConcreteElement2 *element)
    {
        cout << "Concrete Visitor 2 is visiting Concrete Element 2" << endl;
    }
};

#endif

