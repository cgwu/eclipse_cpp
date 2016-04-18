
#include "Receiver.cpp"
#include "command.cpp"
#include "ConcreteCommand.cpp"
#include "Invoker.cpp"

//int main(int argc, char *argv[])
int main()
{
        Receiver recv;
        Command *cmd = new ConcreteCommand(&recv);
        Invoker invoker(cmd);
        invoker.DoSth();
        delete cmd;
        return 0;
}
