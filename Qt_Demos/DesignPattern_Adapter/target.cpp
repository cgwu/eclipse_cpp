#ifndef TARGET_CPP
#define TARGET_CPP

class Target
{
public:
    virtual ~Target(){}
    virtual void Request() = 0;
};

#endif
