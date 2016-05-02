#include <iostream>
#include "concretecreator.cpp"
#include "concreteproduct.cpp"

using namespace std;

int main()
{
    Creator *creator = new ConcreteCreator();
    Product *product =  creator->FactoryMethod();
    cout << product->to_str() <<endl;
    delete product;
    delete creator;
    return 0;
}
