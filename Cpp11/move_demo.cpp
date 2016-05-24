/*
std::move强制转化为右值.
*/
#include <iostream>
using namespace std;
class Moveable{
public:
	Moveable(): i(new int(3)) {}
	~Moveable() { delete i; }
	Moveable(const Moveable & m): i(new int(*m.i)) {
		cout << "Copied" << endl;
	}
	Moveable(Moveable && m): i(m.i) {
		m.i = nullptr;
		cout << "Moved" << endl;
	}
	int *i;
};
int main(int argc, char *argv[])
{
	Moveable a;
	Moveable c(move(a));
	//cout << *a.i << endl;
	cout << *c.i << endl;
	return 0;
}
// g++ -std=c++11 move_demo.cpp -fno-elide-constructors
