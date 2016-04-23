#ifndef STRATEGYB_CPP
#define STRATEGYB_CPP
#include <iostream>
#include "strategy.cpp"
using namespace std;

class StrategyB : public Strategy
{
public:
    virtual void Operation(){
        cout << "StrategyB Operation is called." << endl;
    }
};
#endif


