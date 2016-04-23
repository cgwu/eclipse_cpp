#include <iostream>
#include "composite.cpp"
#include "leaf.cpp"
using namespace std;

int main()
{
    Component * root = new Composite();
    Component * leaf01 = new Leaf();
    Component * leaf02 = new Leaf();
    Component * comp01 = new Composite();
    Component * leaf11 = new Leaf();
    Component * leaf12 = new Leaf();

    root->Add(leaf01);
    root->Add(leaf02);
    root->Add(comp01);
    comp01->Add(leaf11);
    comp01->Add(leaf12);

    root->Operation();

    delete root;
    delete leaf01;
    delete leaf02;
    delete comp01;
    return 0;
}

