#ifndef CREATOR_CPP
#define CREATOR_CPP
#include "product.cpp"

class Creator
{
public:
    virtual ~Creator(){}
    virtual Product* FactoryMethod() = 0;
};

#endif
