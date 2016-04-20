#include <iostream>
#include "state.cpp"
#include "context.cpp"
#include "concretestatea.cpp"
#include "concretestateb.cpp"
using namespace std;

int main()
{
    Context ctx;
    State * sa = new ConcreteStateA();
    State * sb = new ConcreteStateB();
    ctx.SetState(sa);
    ctx.Request();
    ctx.SetState(sb);
    ctx.Request();
    delete sa;
    delete sb;
    return 0;
}

