#ifndef CONTEXT_CPP
#define CONTEXT_CPP
#include "state.cpp"
class Context
{
public:
    void Request()
    {
        _state->Handle();
    }
    void SetState(State *state)
    {
        _state = state;
    }

private:
    State * _state;
};

#endif
