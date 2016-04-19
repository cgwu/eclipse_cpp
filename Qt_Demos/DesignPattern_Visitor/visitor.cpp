#ifndef VISITOR_CPP
#define VISITOR_CPP

class ConcreteElement1;
class ConcreteElement2;

class Visitor
{
public:
    virtual ~Visitor(){}
    virtual void VisitConcreteElement1(ConcreteElement1 *element)=0;
    virtual void VisitConcreteElement2(ConcreteElement2 *element)=0;
};

#endif

