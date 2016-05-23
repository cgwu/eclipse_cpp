#include <iostream>
#include "user.cpp"

using namespace std;

int main()
{
    auto foo = 12345;
    cout << "foo int:" << foo << endl;
    cout << "Hello World!中文" << endl;
    cout << "---------------" << endl;

    User u1,u2;
    u1.name = "张三";
    u2.name = "李四";
    u1.SendMessage("Hello, lisi");
    u2.SendMessage("hi你好，张三");

    return 0;
}

