#ifndef ABSTRACT_CLASS_CPP
#define ABSTRACT_CLASS_CPP
#include <iostream>
using namespace std;

class AbstractClass
{
public:
    virtual void TemplateMethod()
    {
        cout << "PrimitiveOperation1:" << endl;
        PrimitiveOperation1();
        cout << "PrimitiveOperation1 again:" << endl;
        PrimitiveOperation1();
        cout << "PrimitiveOperation2:" << endl;
        PrimitiveOperation2();
    }

    virtual void PrimitiveOperation1()=0;
    virtual void PrimitiveOperation2()=0;
};

#endif
