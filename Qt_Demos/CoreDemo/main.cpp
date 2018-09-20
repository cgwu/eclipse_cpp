#include <QCoreApplication>
#include <QDebug>

void TestString()
{
    QString str1 = "Welcome 欢迎";
    str1 = str1 + " to you";
    qDebug() << str1;
    QString str2 = "Hello, ";
    str2 += "World!";
    qDebug() << str2;
    str2.append("完结");
    qDebug() << str2;
    QString str;
    str.sprintf("%s", "Welcome");
    qDebug() << str;
    str.sprintf("%s", " to you!");
    qDebug() << str;
    str.sprintf("%s %s %d %f", "欢迎", "You", 7673, 3.1429283);
    qDebug() << str;
    str = QString("     %1 was 出生于 %2.    ").arg("张望三abc").arg(1988);
    qDebug() << str;
    QString strTrim = str.trimmed();
    qDebug() << strTrim;
    QString strSimplify = str.simplified();
    qDebug() << strSimplify;
    qDebug() << str1.startsWith("WelCome",Qt::CaseInsensitive);
    qDebug() << str1.endsWith("you");
    qDebug() << str1.contains("To");
}


int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
//    qDebug() << "Hello中国";
    TestString();

    return a.exec();
}

