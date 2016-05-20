#include "dialog.h"
#include "ui_dialog.h"

Dialog::Dialog(QWidget *parent) :
    QDialog(parent),
    ui(new Ui::Dialog)
{
    ui->setupUi(this);
    for(int i=0; i<9; ++i){
        ui->listWidget->addItem(QString::number(i)+ " list item");
    }
}

Dialog::~Dialog()
{
    delete ui;
}

void Dialog::on_pushButton_clicked()
{
    QListWidgetItem *item = ui->listWidget->currentItem();
    if(item){
        item->setText("更改成选中项文本");
        item->setTextColor(Qt::red);
        item->setBackgroundColor(Qt::black);
    }
}
