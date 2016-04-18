
#ifndef CONCRETE_HANDLER2_HPP
#define CONCRETE_HANDLER2_HPP
#include "request.hpp"
#include "handler.hpp"
#include <iostream>
using namespace std;

class ConcreteHandler2 :  public Handler
{
public:
    ConcreteHandler2(ConcreteHandler2 * successor) : Handler(successor){}
    virtual ~ConcreteHandler2(){}

    void HandleRequest(Request * request){
        if(request->Code == 2){
            cout << "Concrete Handler 2 Handles This Request!" << endl;
        }
        else if(_successor) _successor->HandleRequest(request);
    }
};
#endif
