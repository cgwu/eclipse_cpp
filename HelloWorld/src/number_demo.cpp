/*
 * number_demo.cpp
 *
 *  Created on: 2016Äê2ÔÂ28ÈÕ
 *      Author: cg
 */


#include "stdcpp.hpp"
#include <boost/rational.hpp>
#include <boost/crc.hpp>
#include <ctime>
#include <boost/random.hpp>
using namespace boost;

void test_crc_checksum(){
	crc_32_type crc32;
	cout << hex;
	cout <<crc32.checksum() << endl;
	crc32.process_byte('a');
	cout << crc32.checksum() << endl;
	crc32.process_bytes("1234567890", 10);
	cout << crc32.checksum() << endl;
	char szCh[] = "1234567890";
	crc32.reset();
	crc32.process_block(szCh, szCh+10);
	cout << crc32.checksum() << endl;
	cout << dec;
}
void test_random(){
	mt19937 rng(time(0));
	for(int i=0;i<20;++i){
		cout << rng() << endl;
	}
}
int main_number_dmeo (int argc, char* argv[]){
	rational<int> a(9,6);
	cout << a << endl;
	++a;
	cout << a << endl;
	cout << "-------------" << endl;
	test_crc_checksum();
	cout << "-------------" << endl;
	test_random();
	return 0;
}
