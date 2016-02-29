/*
 * container_demo.cpp
 *
 *  Created on: 2016��2��28��
 *      Author: cg
 */

#include "stdcpp.hpp"
#include <set>
#include <map>
#include <boost/array.hpp>
#include <boost/typeof/typeof.hpp>
#include <boost/foreach.hpp>
#include <boost/assign.hpp>
#include <boost/functional/hash.hpp>
// unorderedɢ����������Ҫ��������
#include <boost/unordered_set.hpp>		//unordered_set;	unordered_multiset;
#include <boost/unordered_map.hpp>		//unordered_map;	unordered_multimap;
#include <boost/circular_buffer.hpp>
#include <boost/tuple/tuple.hpp>
#include <boost/property_tree/ptree.hpp>
#include <boost/property_tree/xml_parser.hpp>
#include <boost/property_tree/json_parser.hpp>
#include <boost/property_tree/ini_parser.hpp>
using namespace boost;

// ��������������
void test_array(){
	array<int, 10> ar;	//һ����СΪ10��int����
	ar[0] = 1;
	ar.back() = 10;	//back()�������һ��Ԫ��
	assert(ar[ar.max_size()-1] == 10);

	ar.assign(777);			//��������Ԫ�ظ�ֵΪ777
	for(BOOST_AUTO(pos, ar.begin()); pos != ar.end(); ++pos){
		cout << *pos << ",";
	}
	cout << endl;

	int *p = ar.c_array();	//���ԭʼ����ָ��
	*(p+5) = 253;
	cout << ar[5] << endl;

	ar.at(8) = 666;
	for(BOOST_AUTO(pos, ar.begin()); pos != ar.end(); ++pos){
		cout << *pos << ",";
	}
	cout << endl;

	sort(ar.begin(), ar.end());
	for(BOOST_AUTO(pos, ar.begin()); pos != ar.end(); ++pos){
		cout << *pos << ",";
	}
	cout << endl;
}

template<typename T>
void test_set(){
	using namespace boost::assign;
	T s = (list_of(1), 2, 3, 4, 5);
	// http://blog.sina.com.cn/s/blog_69d9bff30101cx8f.html
	for(typename T::iterator p = s.begin(); p != s.end(); ++p ){
	//for( BOOST_AUTO( p , s.begin()); p != s.end(); ++p ){
		cout << *p << "  ";
	}
	cout << endl;
	cout << "size:" << s.size() << endl;
	s.clear();
	cout << "is empty:" << s.empty() << endl;
	s.insert(8);
	s.insert(45);
	cout << "after size:" << s.size() << endl;
	cout << "find 8:" << *s.find(8) << endl;
	s.erase(45);
}

template<typename T>
void test_map(){
	//typedef boost::unordered_map<std::string, int> map;
	//typedef map<std::string, int> map;
	typedef T map;
	map x;

//	x["one"] = 1;
//	x["two"] = 2;
//	x["three"] = 3;
//	assert(x.at("one") == 1);
//	assert(x.find("missing") == x.end());

	x[1] = "one";
	x[2] = "two";
	x[3] = "three";
	assert(x.at(1) == "one");
	assert(x.find(110) == x.end());
	//BOOST_FOREACH( map::value_type i, x)	//error: dependent-name 'map:: value_type' is parsed as a non-type, but instantiation yields a type
	BOOST_FOREACH(typename map::value_type i, x)
	{
	    std::cout<<i.first<<","<<i.second<<"\n";
	}
}

//�Զ�����:ʵ�� operator==�� hash_value���������ɷ���unordered����
struct demo_set_entry{
	int a;
	friend bool operator==(const demo_set_entry& l, const demo_set_entry& r){
		return l.a == r.a;
	}
};
size_t hash_value(const demo_set_entry & s){
	  boost::hash<int> hasher;
	  return hasher(s.a);
}


void test_circular_buffer(){
	circular_buffer<int> cb(5);
	assert(cb.empty());
	cb.push_back(1);
	cb.push_front(2);
	assert(cb.front());
	cb.insert(cb.begin(), 3);
	BOOST_FOREACH(int i, cb){
		cout << i << ",";
	}
	cout << endl;
	cb.push_back(4);
	cb.push_back(5);
	BOOST_FOREACH(int i, cb){
		cout << i << ",";
	}
	cout << endl;
	cb.push_back(6);
	BOOST_FOREACH(int i, cb){
		cout << i << ",";
	}
	cout << endl;
}
void test_tuple(){
	BOOST_AUTO(t, make_tuple(1,"str�й�", 100.0));
	cout << t.get<0>() << "," << t.get<1>() << "," << t.get<2>() << endl;
}
/*
 * property_tree�ر��ʺ���Ӧ�ó�����������ݴ������Խ��� xml, ini, json, info ���ָ�ʽ���ļ����ݣ�
 * ʹ���ܹ������Լ��������ù���Ĺ�����
 */
void test_property_tree(){
	using namespace boost::property_tree;
	ptree pt;
	read_xml("conf.xml", pt);
	cout << pt.get<string>("conf.theme") << endl;
	cout << pt.get<int>("conf.clock_style") << endl;
	cout << pt.get("conf.no_prop", 101) << endl;		//ȡ�����ڵ�����ֵ������Ĭ��
	BOOST_AUTO(urls, pt.get_child("conf.urls"));
	for(BOOST_AUTO(pos, urls.begin()); pos != urls.end(); ++ pos){
		cout << pos->second.data() << ",";
	}
	cout << endl;
	pt.put("conf.theme", "Matrix Reloaded");
	write_xml(cout, pt);
//	write_json("conf.json", pt);
//	write_ini("conf.ini", pt);
}
int main_container_demo(int argc, char* argv[]){
	using namespace boost::assign;
	test_array();
	cout << "-----unordered_set--------" << endl;
	test_set<unordered_set<int> >();	// boost���൱���������Ե�hash_set,����Ч�ʳ�����
	cout << "------set-------" << endl;
	test_set<set<int> >();				// ��׼����ʵ��ʹ�����������������Ч�ʶ�����ʱ��
	cout << "-------unordered_map------" << endl;
	test_map<boost::unordered_map<int,string> >();
	cout << "-------map------" << endl;
	test_map<map<int,string> >();
	cout << "------set-------" << endl;
	unordered_set<demo_set_entry> sd = list_of(demo_set_entry()) (demo_set_entry());
	cout << sd.size() << endl;
	demo_set_entry dse1;
	dse1.a = 123;
	sd.insert(dse1);
	cout << "after insert:" << sd.size() << endl;
	//cout << "bucket_size:" << sd.bucket_size() << ",bucket_count:" << sd.bucket_count() << endl;
	cout << "------circular_buffer-------" << endl;
	test_circular_buffer();
	cout << "------tuple-----------" << endl;
	test_tuple();
	cout << "----------test property_tree----------"<<endl;
	test_property_tree();

	return 0;
}

