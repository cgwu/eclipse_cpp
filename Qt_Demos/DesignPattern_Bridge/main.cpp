#include <iostream>
#include "refinedabstraction.cpp"
#include "concreteimpla.cpp"
#include "concreteimplb.cpp"

using namespace std;

int main()
{
    Implementor * impla = new ConcreteImplA();
    Implementor * implb = new ConcreteImplB();
    RefinedAbstraction ra;
    ra.SetImpl(impla);
    ra.OperationEx();
    cout << "---------------" << endl;
    ra.SetImpl(implb);
    ra.OperationEx();
    delete impla;
    delete implb;

    return 0;
}

