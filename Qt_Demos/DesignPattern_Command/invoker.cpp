#ifndef INVOKER_HPP
#define INVOKER_HPP
#include "command.cpp"
class Invoker
{
public:
        Invoker(Command * cmd): _cmd(cmd) {}
        void DoSth(){
                _cmd->Execute();
        }
private:
        Command * _cmd;
};
#endif

