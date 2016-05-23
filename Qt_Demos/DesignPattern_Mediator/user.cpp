#ifndef USER_CPP
#define USER_CPP

#include "chatroom.cpp"

class User{
public:
    char * name;
    void SendMessage(char * msg){
        ChatRoom::SendMessage(name, msg);
    }
};

#endif
