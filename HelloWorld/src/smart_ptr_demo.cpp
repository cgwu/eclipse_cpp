/*
 * smart_ptr_demo.cpp
 *
 *  Created on: 2016年2月23日
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/smart_ptr.hpp>
#include <boost/make_shared.hpp>
using namespace boost;
struct posix_file{
	posix_file(){
		cout << "default posix file ctor." << this << endl;
	}
	posix_file(const char * file_name){
		cout << "open file:" << file_name << endl;
	}
	~posix_file(){
		cout << "close file:" << this << endl;
	}
};
void process_file(shared_ptr<posix_file> pf){
	cout << "shared_ptr use_count in function:" << pf.use_count()<<endl;
}

void any_function(void* p)
{
	cout << "some operate" << endl;
}
int main(int argc, char* argv[]){
	scoped_ptr<string> sp(new string("hello中国"));
	cout << *sp << endl;
	cout << sp->size() << endl;

//	{
	scoped_ptr<posix_file> spf(new posix_file("/tmp/a.txt"));
//	}
	spf.reset();
	assert(spf == 0);
	if(!spf){
		cout << "spf is null ptr" << endl;
	}
	cout << "------scoped_array----------" << endl;

	{
		posix_file * parr = new posix_file[4];
		scoped_array<posix_file> sap(parr);
	}
	cout << "-------shared_ptr---------" << endl;
	{
		shared_ptr<posix_file> p1(new posix_file);
		cout << "shared_ptr use_count:" << p1.use_count()<<endl;
		shared_ptr<posix_file> p2(p1);
		cout << "shared_ptr use_count:" << p1.use_count()<<endl;
		p2.reset();
		cout << "shared_ptr use_count:" << p1.use_count()<<endl;
		vector<shared_ptr<posix_file> > vf;
		vf.push_back(p1);
		cout << "shared_ptr use_count after push vector:" << p1.use_count()<<endl;
		list<shared_ptr<posix_file> > lf;
		lf.push_back(p1);
		cout << "shared_ptr use_count after push list:" << p1.use_count()<<endl;
		process_file(p1);
		cout << "shared_ptr use_count after call function:" << p1.use_count()<<endl;
	}
	cout << "-------make_shared---------" << endl;
	{
	shared_ptr<posix_file> sp1 = make_shared<posix_file>();
	shared_ptr<posix_file> sp2 = make_shared<posix_file>("c:/helo.txt");
	}
	cout << "-------在容器里面---------" << endl;
	{
		typedef vector<shared_ptr<int> > vs;
		vs v(10);
		int i=0;
		for (vs::iterator pos = v.begin(); pos != v.end(); ++pos) {
			*pos = make_shared<int>(++i);
			cout << **pos << ", ";
		}
		cout << endl;
		cout << "v[9]:" << *v[9] << endl;
	}
	cout << "定制删除器" << endl;
	{
		shared_ptr<void> p((void*)0, any_function);
	}
	cout << "end" << endl;
	return 0;
}
