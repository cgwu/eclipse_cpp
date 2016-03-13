#include <QCoreApplication>
#include <QDebug>
//#include <iostream>

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    qDebug() << "Hello中国" << endl;
//    std::cout << "Hello 中国万岁" << std::endl;

    return a.exec();
}

