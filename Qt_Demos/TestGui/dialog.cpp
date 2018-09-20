#include "dialog.h"
#include "ui_dialog.h"
#include <QMessageBox>

Dialog::Dialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::Dialog)
{
    ui->setupUi(this);
    ui->pushButton->setText("关闭");

    connect(ui->horizontalSlider, SIGNAL(valueChanged(int)),
            ui->progressBar_2, SLOT(setValue(int)));


//    disconnect(ui->horizontalSlider_2, SIGNAL(valueChanged(int)),
//            ui->progressBar_2, SLOT(setValue(int)));
}

Dialog::~Dialog()
{
    delete ui;
}

void Dialog::on_btnShowMsg_clicked()
{
   QMessageBox::information(this, "title", "hello");
}
