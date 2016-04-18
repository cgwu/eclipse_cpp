
#ifndef CONCRETE_HANDLER1_HPP
#define CONCRETE_HANDLER1_HPP
#include "request.hpp"
#include "handler.hpp"
#include <iostream>
using namespace std;

class ConcreteHandler1 :  public Handler
{
public:
    ConcreteHandler1(Handler * successor) : Handler(successor){}
    virtual ~ConcreteHandler1(){}

    void HandleRequest(Request * request){
        if(request->Code == 1){
            cout << "Concrete Handler 1 Handles This Request!" << endl;
        }
        else if(_successor) _successor->HandleRequest(request);
    }

};
#endif
