#ifndef CONCRETE_SUBJECT_CPP
#define CONCRETE_SUBJECT_CPP
#include "Subject.cpp"
typedef int StateType;

class ConcreteSubject : public Subject
{
public:
    void SetState(StateType state)
    {
        _state = state;
    }
    StateType GetState() const
    {
        return _state;
    }

private:
    StateType _state;
};

#endif
