/*
移动语义：
g++ -std=c++11 move_construct_demo.cpp -fno-elide-constructors -o copy_demo.exe
g++ -std=c++11 move_construct_demo.cpp -fno-elide-constructors -o move_demo.exe

3.3.3 左值、右值与右值引用
在C语言中，我们常常会提起左值(lvalue),右值(rvalue)。
a = b + c;	// a是左值，(b+c)是右值.
在C++中， 可以取地址的、有名字的值就是左值，返之就是右值。
在C++11中，右值由两个概念构成，一个是将亡值(xvalue, eXpiring Value),另一个是纯右值(prvalue, Pure Rvalue).

*/
#include <iostream>
using namespace std;

class HasPtrMem {
public:
	HasPtrMem(): d(new int(3)) {
		cout << "Construct: " << ++ n_cstr << endl;
	}
	HasPtrMem(const HasPtrMem & h): d(new int(*h.d)) {
		cout << "Copy construct: " << ++n_cptr << endl;
	}
	
	// 移动构造函数，接受一个所谓的“右值引用”的参数，暂时理解为临时变量的引用。
	HasPtrMem(HasPtrMem && h): d(h.d){
		h.d = nullptr;	// 原对象成员置空，重要!!
		cout << "Move construct: " << ++n_mvtr << endl;
	}
	
	~HasPtrMem(){
		delete d;
		cout << "Destruct: " << ++n_dstr << endl;
	}
	int *d;
	static int n_cstr;
	static int n_dstr;
	static int n_cptr;
	static int n_mvtr;
};

int HasPtrMem::n_cstr = 0;
int HasPtrMem::n_dstr = 0;
int HasPtrMem::n_cptr = 0;
int HasPtrMem::n_mvtr = 0;

HasPtrMem GetTemp(){
	HasPtrMem h;
	cout << "Resource from " << __func__ << ": " << hex << h.d << endl;
	return h;
}

int main()
{
	HasPtrMem a = GetTemp();
	cout << "Resource from " << __func__ << ": " << hex << a.d << endl;
	
	return 0;
}

