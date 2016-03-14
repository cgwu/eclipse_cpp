/*
windows上面复制目标文件,自定义项目构建步骤
xcopy Debug\FirstSharedLib_Test.exe ..\Debug\ /Y
xcopy Release\FirstSharedLib_Test.exe ..\Release\ /Y
*/


#include <QCoreApplication>
#include <QDebug>
#include "../FirstSharedLib/foo.h"

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    Foo foo;
    foo.Test();

    qDebug() << "Hello中国" ;

    return a.exec();
}

