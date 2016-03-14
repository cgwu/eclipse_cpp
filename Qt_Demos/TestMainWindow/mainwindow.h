#ifndef MAINWINDOW_H
#define MAINWINDOW_H

#include <QMainWindow>
#include "mydialog2.h"
#include "finddialog.h"

namespace Ui {
class MainWindow;
}

class MainWindow : public QMainWindow
{
    Q_OBJECT

public:
    explicit MainWindow(QWidget *parent = 0);
    ~MainWindow();

private slots:
    void on_actionNew_triggered();

    void on_actionFind_triggered();

private:
    Ui::MainWindow *ui;
    MyDialog2 * m_pDlg;
    FindDialog * m_pFindDlg;
};

#endif // MAINWINDOW_H
