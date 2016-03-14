QT += core
QT -= gui

TARGET = FirstSharedLib_Test
CONFIG += console
CONFIG -= app_bundle

TEMPLATE = app

SOURCES += main.cpp






LIBS += "../Release/FirstSharedLib.dll"
