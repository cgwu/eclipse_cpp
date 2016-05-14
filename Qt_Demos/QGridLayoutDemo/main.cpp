#include "mainwindow.h"
#include <QApplication>
#include <QtGui>
#include <QtCore>
#include <QGridLayout>
#include <QLabel>
#include <QLineEdit>
#include <QPushButton>

int main(int argc, char *argv[])
{
    QApplication a(argc, argv);
    //MainWindow w;
    QWidget w;
    QGridLayout *layout = new QGridLayout;
    QLabel *label = new QLabel("Name:");
    QLineEdit *txtName = new QLineEdit();
    layout->addWidget(label,0,0);
    layout->addWidget(txtName,0, 1);

    QLabel *labelPwd = new QLabel("Password:");
    QLineEdit *txtPwd = new QLineEdit();
    layout->addWidget(labelPwd,1,0);
    layout->addWidget(txtPwd,1, 1);

    QPushButton *button = new QPushButton("Login");
    layout->addWidget(button,2,0,1,2);

    w.setLayout(layout);
    w.show();

    return a.exec();
}
