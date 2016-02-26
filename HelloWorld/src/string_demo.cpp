/*
 * string_demo.cpp
 *
 *  Created on: 2016年2月26日
 *      Author: cg
 */


#include "stdcpp.hpp"
#include <boost/lexical_cast.hpp>
#include <boost/format.hpp>
#include <boost/algorithm/string.hpp>
#include <boost/tokenizer.hpp>
#include <boost/typeof/typeof.hpp>
using namespace boost;

void test_lexical_cast(){
	int x = lexical_cast<int>("100");
	long y = lexical_cast<long>("2000");
	float pi = lexical_cast<float>("3.1415926e5");
	double e = lexical_cast<double>("2.71828e10");
	cout << x << endl << y << endl
		 << pi << endl << e << endl;
	string str = lexical_cast<string>(456);
	cout << str << endl;
	cout << lexical_cast<string>(0.618) << endl;
	cout << lexical_cast<string>(0x12) << endl;
}
void test_string_format()
{
	cout << format("%s:%d+%d=%d\n") % "sum" % 1 % 2 % (1+2);
	format fmt("((%1% + %2%) * %2% = %3%)\n");
	fmt %2 % 5;
	fmt % ((2+5)*5);
	cout << fmt.str() << endl;
}
void test_string_algorithm(){
	string str("readme.txt");
	if(ends_with(str, "txt")){
		cout << to_upper_copy(str) + " UPPER" << endl;
	}

	replace_first(str, "readme", "followme");
	cout << str << endl;

	string msg("     I Don't Know.\n   ");
	cout << to_upper_copy(msg);
	cout << "trim left" << endl;
	trim_left(msg);
	cout << msg;
	cout << "trim right" << endl;
	trim_right(msg);	//换行符也会去掉
	cout << msg;
	cout << "trim" << endl;
	trim(msg);		// trim_copy, trim_copy_if
	cout << msg;
	cout << "to_lower" << endl;
	to_lower(msg);
	cout << msg << endl;
}
void test_string_tokenizer(){
	string str("Link     raise the master-sword 中国");
	tokenizer<> tok(str);		//使用缺少模板参数创建分词对象
	//可以像遍历一个容器一样使用for循环获得分词结果
	for(BOOST_AUTO(pos, tok.begin()); pos != tok.end(); ++pos){
		cout << "[" << *pos << "]" << endl;
	}
}
int main_string_demo (int argc, char* argv[]){
	test_lexical_cast();
	cout << "------------" << endl;
	test_string_format();
	cout << "------------" << endl;
	test_string_algorithm();
	cout << "------------" << endl;
	test_string_tokenizer();
	return 0;
}
