#-------------------------------------------------
#
# Project created by QtCreator 2016-03-13T15:43:11
#
#-------------------------------------------------

QT       += core gui

greaterThan(QT_MAJOR_VERSION, 4): QT += widgets

TARGET = TestMainWindow
TEMPLATE = app


SOURCES += main.cpp\
        mainwindow.cpp \
    mydialog2.cpp \
    main_layout.cpp \
    finddialog.cpp

HEADERS  += mainwindow.h \
    mydialog2.h \
    finddialog.h

FORMS    += mainwindow.ui \
    mydialog.ui \
    mydialog2.ui \
    finddialog.ui
