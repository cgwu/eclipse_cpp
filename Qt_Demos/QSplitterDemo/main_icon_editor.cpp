
#include <QApplication>
#include <QImage>
#include <QLabel>
#include <QPixmap>
#include <QScrollArea>

int main(int argc, char *argv[])
{
    QApplication app(argc, argv);

    QScrollArea scrollArea;
    QImage img;
    img.load(":/images/happyface.jpg");
    QLabel lbl;
    lbl.setPixmap(QPixmap::fromImage(img));
    //lbl.show();
    scrollArea.setWidget(&lbl);
    scrollArea.viewport()->setBackgroundRole(QPalette::Dark);
    scrollArea.viewport()->setAutoFillBackground(true);
    scrollArea.setWindowTitle(QObject::tr("Icon Editor"));
    scrollArea.show();

    return app.exec();
}
