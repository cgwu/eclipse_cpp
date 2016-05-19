#include "mywidget.h"
#include <QApplication>
#include <QTextEdit>
#include <QSplitter>

int main_main(int argc, char *argv[])
{
    QApplication a(argc, argv);

//    MyWidget w;
//    w.show();

    QTextEdit *editor1 = new QTextEdit;
    QTextEdit *editor2 = new QTextEdit;
    QTextEdit *editor3 = new QTextEdit;
    QSplitter splitter(Qt::Horizontal);
    splitter.addWidget(editor1);
    splitter.addWidget(editor2);
    splitter.addWidget(editor3);
    splitter.show();

    return a.exec();
}
