/*
 * smart_ptr_demo.cpp
 *
 *  Created on: 2016��2��23��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <boost/smart_ptr.hpp>
#include <boost/enable_shared_from_this.hpp>
#include <boost/make_shared.hpp>
using namespace boost;
struct posix_file : public enable_shared_from_this<posix_file>
{
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
int main_smart_ptr_demo(int argc, char* argv[]){
	scoped_ptr<string> sp(new string("hello�й�"));
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
	cout << "-------����������---------" << endl;
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
	cout << "����ɾ����" << endl;
	{
		shared_ptr<void> p((void*)0, any_function);
	}
	cout << "------shared_array----------" << endl;
	{
		posix_file * parr = new posix_file[4];
		shared_array<posix_file> sap(parr);
		cout << "shared_array.use_count:" << sap.use_count() << endl;
		shared_array<posix_file> sp2( sap );
		cout << "shared_array.use_count:" << sap.use_count() << endl;
	}
	cout << "------weak_ptr----------" << endl;
	{
		shared_ptr<posix_file> p1 = make_shared<posix_file>("c:/test.data");
		cout << "p1.use_count:" << p1.use_count() << endl;
		weak_ptr<posix_file> wp1(p1);
		cout << "p1.use_count after weak_ptr:" << p1.use_count() << endl;
		if(!wp1.expired()){
			shared_ptr<posix_file> p2 = wp1.lock();		// ͨ��weak_ptr����һ��shared_ptr
			cout << "p1.use_count after weak_ptr.lock():" << p1.use_count() << endl;
		}
		cout << "p1.use_count after weak_ptr:" << p1.use_count() << endl;
		p1.reset();
		assert(wp1.expired());
		assert(!wp1.lock());
	}
	cout << "------enable_shared_from_this----------" << endl;
	{
		shared_ptr<posix_file> p1 = make_shared<posix_file>("c:/test.data");
		cout << "p1.use_count before shared:" << p1.use_count() << endl;
		shared_ptr<posix_file> p2 = p1->shared_from_this();
		cout << "p1.use_count after shared:" << p1.use_count() << endl;
		weak_ptr<posix_file> wp3 = p1->weak_from_this();
		cout << "p1.use_count after weak_from_this:" << p1.use_count() << endl;
	}
	cout << "end" << endl;
	return 0;
}
