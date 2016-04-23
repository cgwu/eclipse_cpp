#ifndef STRATEGY_CPP
#define STRATEGY_CPP
class Strategy
{
public:
    virtual ~Strategy(){}
    virtual void Operation() = 0;
};
#endif

