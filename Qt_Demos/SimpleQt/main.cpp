#include <QApplication>
#include <QLabel>

int main1(int argc, char *argv[])
{
    QApplication a(argc, argv);
    QLabel *label = new QLabel("<h1><i style='color:blue'>Hello</i> <font color='red'>中国</font>!</h1>");
    label->show();
    return a.exec();
}
