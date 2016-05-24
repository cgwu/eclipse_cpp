#include <iostream>

using namespace std;
struct Copyable{
	Copyable(){}
	Copyable(const Copyable &o) {
		cout << "Copied" << endl;
	}
	Copyable(const Copyable &&o) {
		cout << "Moved" << endl;
	}
};

Copyable ReturnRvalue() { return Copyable(); }
void AcceptVal(Copyable) {}
void AcceptRef(const Copyable &) {}
void AcceptRvalueRef(const Copyable && s) {
	Copyable news = std::move(s);
	// ...
}

int main(int argc, char *argv[])
{
	cout << "Pass by value:" << endl;
	AcceptVal(ReturnRvalue());
	cout << "Pass by reference:" << endl;
	AcceptRef(ReturnRvalue());
	cout << "Pass by Rvalue:" << endl;
	AcceptRvalueRef(ReturnRvalue());
	return 0;
}

// g++ rvalue_demo.cpp -std=c++11 -fno-elide-constructors
