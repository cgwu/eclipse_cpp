#ifndef CONTEXT_CPP
#define CONTEXT_CPP
#include "strategy.cpp"
class Context
{
public:
    Context(Strategy *strategy): _strategy(strategy) { }
    void SetStrategy(Strategy *strategy)
    {
        _strategy = strategy;
    }

    void Operation()
    {
        _strategy->Operation();
    }
private:
    Strategy * _strategy;
};

#endif
