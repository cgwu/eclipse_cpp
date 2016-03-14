#include <QCoreApplication>
#include <QDebug>
#include <QDir>

namespace test_QDir{
    void test_dir(){
        QDir dir("D:/");
        qDebug() << dir.exists() ;
        QStringList entries = dir.entryList();
        for(int i=0; i< entries.size(); ++i){
            qDebug() << entries[i] ;
        }
        qDebug() << "----------------" ;
        foreach(QString entry, entries){        // foreach : Qt的宏定义
            qDebug() << entry ;
        }
        qDebug() << "----------------" ;
//        if(dir.mkpath("E:/a/b/c")) qDebug() << "create path successfully!";

    }
}

int main(int argc, char *argv[])
{
    QCoreApplication a(argc, argv);
    qDebug() << "Hello中国";
    QString str = "This is a QString." ;
    qDebug() << str << endl;
    qDebug() << "----------------" ;

    test_QDir::test_dir();

    return a.exec();
}

