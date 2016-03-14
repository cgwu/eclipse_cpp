#include "finddialog.h"
#include "ui_finddialog.h"
#include <QMessageBox>
#include <QDebug>

FindDialog::FindDialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::FindDialog)
{
    ui->setupUi(this);
}

FindDialog::~FindDialog()
{
    delete ui;
}

void FindDialog::on_btnFind_clicked()
{
    qDebug() << "find button clicked" ;
    QMessageBox::information(this, "Title", "你点击了我");
}
