#ifndef STRATEGYA_CPP
#define STRATEGYA_CPP
#include "Strategy.cpp"
#include <iostream>
using namespace std;

class StrategyA : public Strategy
{
public:
    virtual void Operation(){
        cout << "StrategyA Operation is called." << endl;
    }
};
#endif


