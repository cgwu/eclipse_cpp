#ifndef REAL_OBJECT_CPP
#define REAL_OBJECT_CPP
#include <iostream>
#include "subject.cpp"
using namespace std;

class RealObject : public Subject
{
public:
    void Request()
    {
        cout << "Real Object Request called()." << endl;
    }
};

#endif

