#ifndef STATE_CPP
#define STATE_CPP
class State
{
public:
    virtual ~State(){}
    virtual void Handle()=0;
};

#endif
