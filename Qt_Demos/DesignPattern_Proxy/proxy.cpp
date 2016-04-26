#ifndef PROXY_CPP
#define PROXY_CPP
#include <iostream>
#include "subject.cpp"
using namespace std;

class Proxy : public Subject
{
public:
    void Request()
    {
        cout << "Before Request" << endl;
        _realObj->Request();
        cout << "After Request" << endl;
    }
    void SetObject(Subject * realObj)
    {
        _realObj = realObj;
    }
private:
    Subject * _realObj;
};

#endif
