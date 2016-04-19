#include <iostream>
#include "visitor.cpp"
#include "element.cpp"
#include "concretevisitor1.cpp"
#include "concretevisitor2.cpp"
#include "concretelement1.cpp"
#include "concretelement2.cpp"

using namespace std;

int main()
{
    Visitor *v1 = new ConcreteVisitor1;
    Visitor *v2 = new ConcreteVisitor2;
    Element *e1 = new ConcreteElement1;
    Element *e2 = new ConcreteElement2;
    e1->Accept(v1);
    e1->Accept(v2);
    e2->Accept(v1);
    e2->Accept(v2);
    delete e1;
    delete e2;
    delete v1;
    delete v2;
    return 0;
}

