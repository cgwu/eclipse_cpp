#ifndef CONCRETE_PRODUCT_CPP
#define CONCRETE_PRODUCT_CPP
#include "product.cpp"

class ConcreteProduct : public Product
{
public:
    string to_str()
    {
        return "ConcreteProduct.to_str() called.";
    }
};

#endif
