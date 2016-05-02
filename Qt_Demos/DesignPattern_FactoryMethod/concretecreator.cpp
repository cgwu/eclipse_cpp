#ifndef CONCRETE_CREATOR_CPP
#define CONCRETE_CREATOR_CPP
#include "creator.cpp"
#include "concreteproduct.cpp"

class ConcreteCreator : public Creator
{
public:
    virtual Product* FactoryMethod()
    {
        return new ConcreteProduct();
    }
};

#endif
