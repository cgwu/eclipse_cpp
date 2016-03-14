#include "mainwindow.h"
#include "ui_mainwindow.h"
#include "mydialog2.h"
#include <QDebug>

MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    m_pFindDlg = NULL;
    m_pDlg  = NULL;
    ui->setupUi(this);
    setCentralWidget(ui->plainTextEdit);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_actionNew_triggered()
{
//    qDebug() << "action new trigged";
//    MyDialog2 dlg;
//    dlg.setModal(false);
//    dlg.exec();
    m_pDlg = new MyDialog2(this);
    m_pDlg->show();
}

void MainWindow::on_actionFind_triggered()
{
    m_pFindDlg = new FindDialog(this);
    m_pFindDlg->show();
}
