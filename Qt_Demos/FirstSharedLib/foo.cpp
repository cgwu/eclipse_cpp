#include "foo.h"
#include <QDebug>

Foo::Foo()
{
}

void Foo::Test()
{
    qDebug() << "This is a message from DLL Test!";
}
