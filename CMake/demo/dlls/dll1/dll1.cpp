#include "dll1.h"
#include "../../libssrc/lib1/lib1.h"

dll1::dll1() {}
dll1::~dll1() {}

const char* dll1::getText()
{
    lib1 lLib1;
    return lLib1.getText();
}
