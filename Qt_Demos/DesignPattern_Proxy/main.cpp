#include <iostream>
#include "realobject.cpp"
#include "proxy.cpp"
using namespace std;

int main()
{
    Proxy *proxy = new Proxy();
    Subject * sub = new RealObject();
    proxy->SetObject(sub);

    proxy->Request();

    delete sub;
    delete proxy;
    return 0;
}

