#ifndef CONCRETE_ELEMENT2_CPP
#define CONCRETE_ELEMENT2_CPP
#include "element.cpp"

class ConcreteElement2 : public Element
{
public:
    virtual void Accept(Visitor *visitor)
    {
        visitor->VisitConcreteElement2(this);
    }
};

#endif
