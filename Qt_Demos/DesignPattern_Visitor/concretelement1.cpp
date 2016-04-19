#ifndef CONCRETE_ELEMENT1_CPP
#define CONCRETE_ELEMENT1_CPP
#include "element.cpp"

class ConcreteElement1 : public Element
{
public:
    virtual void Accept(Visitor *visitor)
    {
        visitor->VisitConcreteElement1(this);
    }
};

#endif
