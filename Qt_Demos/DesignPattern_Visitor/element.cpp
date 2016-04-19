#ifndef ELEMENT_CPP
#define ELEMENT_CPP
#include "visitor.cpp"

class Element
{
public:
    virtual void Accept(Visitor *visitor) = 0;
};

#endif
