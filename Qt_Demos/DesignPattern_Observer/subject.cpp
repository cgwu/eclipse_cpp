#ifndef SUBJECT_CPP
#define SUBJECT_CPP
#include <iostream>
#include <list>
#include "observer.cpp"
using namespace std;


class Subject
{
public:
    virtual ~Subject()
    {
        cout << "Subject.list@" << this << " destructed." << endl;
    }
    void Attach(Observer * observer)
    {
        _list.push_back(observer);
    }
    void Detach(Observer * observer)
    {
        _list.remove(observer);
    }
    void Nofity()
    {
//        list<Observer>::iterator it;
//         for( it = _list.begin(); it != _list.end(); ++it ) {
//           (*it).Update(this);
//         }

        for(Observer* ob: _list)
        {
            ob->Update(this);
        }
    }
private:
    list<Observer*> _list;
};

#endif
