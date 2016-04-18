
#ifndef HANDLER_HPP
#define HANDLER_HPP
class Handler{
public:
    Handler(Handler * successor): _successor(successor) {}
    virtual ~Handler(){}
    virtual void HandleRequest(Request * request) = 0;
protected:
    Handler * _successor;
};
#endif
