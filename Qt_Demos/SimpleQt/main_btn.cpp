
#include <QApplication>
#include <QPushButton>

int main_btn(int argc, char ** argv)
{
    QApplication app(argc, argv);
    QPushButton *button = new QPushButton("Quit");
    QObject::connect(button, SIGNAL(clicked()),
                     &app, SLOT(quit()));
    button->show();

    return app.exec();
}
