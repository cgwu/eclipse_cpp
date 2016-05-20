#include <QCoreApplication>
#include <QDebug>
#include <QDir>

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    QDir dir("d:/迅雷下载");
    qDebug() << dir.exists() ;
    foreach(QFileInfo info, dir.entryInfoList() ){
        qDebug() << info.absoluteFilePath() ;
    }
    qDebug() << "--------------------";
    foreach(QString info, dir.entryList() ){
        qDebug() << info;
    }
    qDebug() << "--------------------";
    QString path("x:/a/b/c");
    QDir pathDir(path);
    if(pathDir.exists()){
        qDebug() << "Path already exits";
    }
    else {
        pathDir.mkpath(path);
        qDebug() << "make path complete.";
    }

    return a.exec();
}

