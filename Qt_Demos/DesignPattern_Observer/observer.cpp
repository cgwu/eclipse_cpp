#ifndef OBSERVER_CPP
#define OBSERVER_CPP

class Subject;
class Observer
{
public:
    virtual ~Observer(){}
    virtual void Update(Subject * sub)=0;
};
#endif
