#include <QCoreApplication>
#include <QFile>
#include <QString>
#include <QDebug>
#include <QTextStream>

void Read(QString filename)
{
    QFile file(filename);
    if(!file.open(QFile::ReadOnly | QFile::Text))
    {
        qDebug() << "Could not open file for reading...";
        return;
    }
    QTextStream in(&file);
    QString text = in.readAll();
    qDebug() << text;
    file.close();
}


int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);

//    Read("D:\\Workspace\\tmp.js");	// 物理路径
    Read(":/files/tmp_beautify.js");	// 资源路径

    return a.exec();
}

