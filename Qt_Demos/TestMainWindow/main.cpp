#include "mainwindow.h"
#include <QApplication>
#include <QLabel>
#include <QPushButton>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    MainWindow w;
    w.show();

//    QLabel * label = new QLabel("你好，Qt!<h2><i>你好</i>，<font color=red>Qt!</font></h2>");
//    label->show();

//    QPushButton *button = new QPushButton("Quit");
//    QObject::connect(button, SIGNAL(clicked()),
//                     &app, SLOT(quit()));
//    button->show();

    return app.exec();
}
