#include "mainwindow.h"
#include "ui_mainwindow.h"
#include <QMessageBox>


MainWindow::MainWindow(QWidget *parent) :
    QMainWindow(parent),
    ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_action_New_Window_triggered()
{
    //QMessageBox::information(this,"title","hello text",QMessageBox::Yes | QMessageBox::No);
    //QMessageBox::information(this,"标题","内容");

    /*
    MyDialog dlg;
    dlg.setModal(true);
    dlg.exec();
    */
    dlg = new MyDialog(this);
    dlg->show();
}
