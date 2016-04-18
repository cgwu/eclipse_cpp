#include <iostream>
#include "concretehandler1.hpp"
#include "concretehandler2.hpp"
using namespace std;

int main()
{
    Request req(1);
    Handler *handler2 = new ConcreteHandler2(NULL);
    Handler *handler1 = new ConcreteHandler1(handler2);

    handler1->HandleRequest(&req);
    cout << "-----------------" << endl;
    req.Code = 2;
    handler1->HandleRequest(&req);

    delete handler1;
    delete handler2;
    return 0;
}

