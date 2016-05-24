/*
������ʽ���£�
[capture](parameters)->return-type {body}

capture���ͣ�
[] ����ȡ�κα���
[&} ��ȡ�ⲿ�����������б���������Ϊ�����ں�������ʹ��
[=] ��ȡ�ⲿ�����������б�����������һ���ں�������ʹ��
[=, &foo] ��ȡ�ⲿ�����������б�����������һ���ں�������ʹ�ã����Ƕ�foo����ʹ������
[bar] ��ȡbar�������ҿ���һ���ں�������ʹ�ã�ͬʱ����ȡ��������
[x, &y] x��ֵ���ݣ�y�����ô���
[this] ��ȡ��ǰ���е�thisָ�롣����Ѿ�ʹ����&����=��Ĭ����Ӵ�ѡ�

lambda������������ʲô�أ�����std:function��
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
			{"Hello�й�",110},
			{"����",345},
			{"Japan",000}
		};
	for(auto item: m1){
		cout<<"Key:"<<item.first<<",Value:"<<item.second<<endl;
	}
	
	// unique_ptr����ת������
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
	�й�)" << endl;
	
	return 0;
}
