
#include <QApplication>
#include <QLabel>
#include <QWidget>
#include <QDialog>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);
//    QWidget *widget = new QWidget();
//    widget->show();

//    QDialog *dlg = new QDialog();


    QLabel * label = new QLabel("<h1><i>QLabel</i><font color=red>标签</font></h1>");
    label->setFixedWidth(400);
    label->setFixedHeight(300);
    label->show();
    return app.exec();
}
