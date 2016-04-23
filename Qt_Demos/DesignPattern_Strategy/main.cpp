#include <iostream>
#include "context.cpp"
#include "strategya.cpp"
#include "strategyb.cpp"
using namespace std;

int main()
{
    Strategy *sa = new StrategyA();
    Strategy *sb = new StrategyB();
    Context ctx(sa);
    ctx.Operation();
    ctx.SetStrategy(sb);
    ctx.Operation();

    delete sa;
    delete sb;

    return 0;
}

