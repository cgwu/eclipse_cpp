/*
基本形式如下：
[capture](parameters)->return-type {body}

capture类型：
[] 不截取任何变量
[&} 截取外部作用域中所有变量，并作为引用在函数体中使用
[=] 截取外部作用域中所有变量，并拷贝一份在函数体中使用
[=, &foo] 截取外部作用域中所有变量，并拷贝一份在函数体中使用，但是对foo变量使用引用
[bar] 截取bar变量并且拷贝一份在函数体重使用，同时不截取其他变量
[x, &y] x按值传递，y按引用传递
[this] 截取当前类中的this指针。如果已经使用了&或者=就默认添加此选项。

lambda函数的类型是什么呢，答案是std:function。
*/

#include <vector>
#include <iostream>
#include <algorithm>
#include <map>
#include <memory>

using namespace std;

void PrintVector(vector<int> &c){
	cout << "c: ";
	std::for_each(c.begin(), c.end(), [](int x){ cout << x << ' '; });
	cout << endl;
}

class Foo{
public:
	Foo(){ cout<< "Foo construct."<< this  <<endl;}
	Foo(int x){ 
		val = x;
		cout<< "Foo construct."<< this  <<endl;
	}
	~Foo(){ cout<< "Foo destruct."<< this <<endl; }
	int val;
	void bar(){ cout<< "Foo.bar() called."<< val <<endl; }
};

int main()
{
	vector<int> c = {1, 2, 3, 4, 5, 6, 7};
	
	PrintVector(c);
	
	int x = 5;
	decltype(x) x2 = 8;
	cout << "decltype:" << x2 << endl;
	//auto ret = remove_if(c.begin(), c.end(), [x](int i){ return i < x; } );
	//cout << "ret of remove_if:" << *ret << endl;
	
	//c.erase(remove_if(c.begin(), c.end(), [x](int i){ return i<x; }), c.end());
	c.erase(remove_if(c.begin(), c.end(), [x](int i){ return i % 2; }), c.end());
	
	PrintVector(c);

	for_each(c.begin(),c.end(), [](int &x){ x *= 2; });
	PrintVector(c);
	
	cout << "------------------" << endl;
	auto func1 = [](int i) { return i+4;};
	cout << "func1:" << func1(6) << endl;
	
	cout << "------------------" << endl;
	std::function<int(int)> func2 = [](int i) { return i+4;};
	cout << "func2:" << func2(8) << endl;
	
	
	map<string,int> m1 {
			{"Hello中国",110},
			{"美国",345},
			{"Japan",000}
		};
	for(auto item: m1){
		cout<<"Key:"<<item.first<<",Value:"<<item.second<<endl;
	}
	
	// unique_ptr具有转移语义
	unique_ptr<Foo> p1(new Foo(33));
	//if(p1) p1->val = 123;
	if(p1) p1->bar();
	{
		unique_ptr<Foo> p2 = std::move(p1);
		p2->val *=2;
		p2->bar();
	}
	if(p1) p1->bar();
	else cout << "p1 is null now"<< endl;
	cout<< "******************************" <<endl;
	
	// shared_ptr
	//shared_ptr<Foo> sp1(new Foo());	
	shared_ptr<Foo> sp1 = make_shared<Foo>(8);
	//if(sp1) sp1->val = 123;
	if(sp1) sp1->bar();
	{
		shared_ptr<Foo> sp2 = sp1;
		sp2->val *=2;
		sp2->bar();
	}
	if(sp1) sp1->bar();
	
	cout<< "******************************" <<endl;
	cout << R"(Hello, 
	World
	中国)" << endl;
	
	return 0;
}
