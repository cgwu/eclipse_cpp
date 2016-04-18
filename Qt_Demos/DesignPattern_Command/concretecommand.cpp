#ifndef CONCRETECOMMAND_HPP
#define CONCRETECOMMAND_HPP
#include "Receiver.cpp"
#include "command.cpp"
class ConcreteCommand : public Command
{
public:
        ConcreteCommand() {}
        ~ConcreteCommand() {}
        ConcreteCommand(Receiver * receiver): _receiver(receiver) {}
        virtual void Execute(){
                _receiver->Action();
        }
private:
        Receiver * _receiver;
};
#endif

