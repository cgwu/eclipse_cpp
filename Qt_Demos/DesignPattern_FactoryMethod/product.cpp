#ifndef PRODUCT_CPP
#define PRODUCT_CPP
#include <string>
using namespace std;

class Product
{
public:
    virtual ~Product(){}
    virtual string to_str() = 0;
};

#endif
