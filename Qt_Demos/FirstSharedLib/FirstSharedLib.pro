#-------------------------------------------------
#
# Project created by QtCreator 2016-03-14T16:50:52
#
#-------------------------------------------------

QT       -= gui

TARGET = FirstSharedLib
TEMPLATE = lib

DEFINES += FIRSTSHAREDLIB_LIBRARY

SOURCES += foo.cpp

HEADERS += foo.h\
        firstsharedlib_global.h

unix {
    target.path = /usr/lib
    INSTALLS += target
}
