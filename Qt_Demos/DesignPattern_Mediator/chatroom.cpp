

#ifndef CHAT_ROOM_CPP
#define CHAT_ROOM_CPP

#include <iostream>
using namespace std;

class ChatRoom{
public:
    static void SendMessage(char * name, char * msg)
    {
        cout << name << " says:" << msg << endl;
    }
};

#endif
