/*
 * system_demo.cpp
 *
 *  Created on: 2016年2月29日
 *      Author: cg
 */

#include <fstream>
#include "stdcpp.hpp"
#include <boost/io/ios_state.hpp>

#define BOOST_SYSTEM_NO_LIB
#include <boost/system/system_error.hpp>
#include <boost/filesystem.hpp>
#include <boost/filesystem/fstream.hpp>
#include <boost/program_options.hpp>

void test_cout_redirect(){
	using namespace boost::io;
	string filename ("log.txt");
	cout <<"log start" << endl;
	if(!filename.empty()){
		ios_all_saver ifs(cout);		//保存流的状态
		ofstream fs(filename.c_str());
		cout.rdbuf(fs.rdbuf());			// 标准输出重定向到文件流
		cout << "fatal msg, 致命错误!" << endl;
	}		// 离开作用域，导致保存器析构，自动恢复流的状态
	cout << "log finish" << endl;
}
/* system库使用轻量级对象封闭了OS底层的错误代码和错误信息，使调用OS的程序可以容易移值.
 * 它作为基础部件已经被filesystem,asio等库调用，并且被接受为C++0x TR2.
 */
void test_system(){
	using namespace boost::system;
	try{
		throw system_error(error_code(0, system_category()));
	}
	catch(system_error& e){
		cout << e.what();
	}
}

void test_filesystem(){
	using namespace boost::filesystem;
	char str[] = "the path is (/root).";
	path p(str+13, str+14);	//取字符串中的一个斜杠
	cout << p << endl;
	assert(!p.empty());
	p /= "etc";		//使用 operator/=追加路径
	string filename("xinetd.conf");
	p.append(filename.begin(), filename.end());
	cout << p << endl;
	cout << system_complete(p) << endl;		// 返回路径在当前文件系统中的完整路径

	cout << "----p.string(): " << p.string() << endl;
	cout << p.parent_path() << endl;
	cout << p.root_path() << endl;
	cout << p.stem() << endl;
	cout << p.filename() << endl;
	cout << p.extension() << endl;
}
void test_fstream(){
	using namespace boost::filesystem;
	namespace newfs = boost::filesystem;
	path p("d:/青春，该用来追寻什么.txt");
	newfs::ifstream ifs(p.string().c_str());
	assert(ifs.is_open());
	cout << ifs.rdbuf();
}
int test_program_options(int ac, char* av[]){
	namespace po = boost::program_options;
	try {

	        po::options_description desc("Allowed options");
	        desc.add_options()
	            ("help,h", "produce help message这是帮助信息")
	            ("compression,c", po::value<double>(), "set compression level")
				("verbose,v", "输出详细信息")
	        ;

	        po::variables_map vm;
	        po::store(po::parse_command_line(ac, av, desc), vm);
	        po::notify(vm);

	        if (vm.count("help")) {
	            cout << desc << "\n";
	            return 0;
	        }
	        if (vm.count("verbose")) {
				cout << "verbose被设置" << "\n";
				return 0;
			}

	        if (vm.count("compression")) {
	            cout << "Compression level was set to "
	                 << vm["compression"].as<double>() << ".\n";
	        } else {
	            cout << "Compression level was not set.\n";
	        }
	    }
	    catch(exception& e) {
	        cerr << "error: " << e.what() << "\n";
	        return 1;
	    }
	    catch(...) {
	        cerr << "Exception of unknown type!\n";
	    }
	    return 0;
}
int main_system_demo(int argc, char* argv[]){
	test_cout_redirect();
	cout << "-----------" << endl;
	test_system();
	cout << "-----------" << endl;
	test_filesystem();
	cout << "-----test_fstream------" << endl;
	//test_fstream();
	cout << "-----program_options------" << endl;
	test_program_options(argc, argv);

	return 0;
}
