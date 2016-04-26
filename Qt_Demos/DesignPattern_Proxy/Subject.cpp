#ifndef SUBJECT_CPP
#define SUBJECT_CPP

class Subject
{
public:
    virtual ~Subject(){}
    virtual void Request() = 0;
};

#endif
