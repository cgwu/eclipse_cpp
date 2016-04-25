#ifndef REFINED_ABSTRACTION_CPP
#define REFINED_ABSTRACTION_CPP
#include "abstraction.cpp"
class RefinedAbstraction : public Abstraction
{
public:
    void OperationEx()
    {
        this->Operation();
        this->Operation();
        this->Operation();
    }
};

#endif
