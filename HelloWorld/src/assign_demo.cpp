/*
 * assign_demo.cpp
 *
 *  Created on: 2016��2��25��
 *      Author: cg
 */

#include <deque>
#include "stdcpp.hpp"
#include <boost/assign.hpp>
using namespace boost;

/*
 * assign �������˸�ֵ������ operator+= �����Ų������������Ų�������
 * ʹ�ü�෽��ķ�ʽ��STL���ڸ�ֵ���ʼ����
 */

int main_assign_demo(int argc, char* argv[]){
	using namespace boost::assign;
	vector<int> v;
	v += 1,2,3,4,5,6*6;
	cout << v[5] << endl;
	push_back(v) (11) (22) (33) (44) (55);		// push_back ��������
	cout << v[10] << endl;

	list<string> l;
	push_front(l) ("cpp") ("java") ("c#") ("pythono");		// push_front��������
	assert(l.size() == 4);

	set<string> s;
	s += "cpp", "java", "c#", "python";
	set<string>::iterator pos = s.find("python");
	cout << *pos << endl;
	insert(s) ("new lang") ("abc");
	assert(s.size() == 6);				// insert��������

	map<int,string> m;
	m += make_pair(1,"one"), make_pair(2,"two");
	cout << m[2] << endl;
	insert(m) (11, "eleven") (12, "twelve");
	assert(m.size() == 4);

	cout << "-------��ʼ��������:list_of(), map_list_of()/pair_list_of(), tuple_list_of()----" << endl;
	vector<int> v1 = list_of(1).repeat(3,33) (2) (3) (4) (5);
	cout << v1[3] << ",idx->4:" << v1[4] << endl;

	deque<string> dq1 = (list_of("power") ("bomb"), "phazon", "suit");	// ע�����ŵ�ʹ��
	assert(dq1.size() == 4);
	cout << dq1[3] << endl;

	map<int,int> m1 = map_list_of(1,2) (3,4) (5,6);
	cout << m1[5] << endl;

	return 0;
}
