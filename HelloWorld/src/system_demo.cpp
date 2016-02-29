/*
 * system_demo.cpp
 *
 *  Created on: 2016��2��29��
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
		ios_all_saver ifs(cout);		//��������״̬
		ofstream fs(filename.c_str());
		cout.rdbuf(fs.rdbuf());			// ��׼����ض����ļ���
		cout << "fatal msg, ��������!" << endl;
	}		// �뿪�����򣬵��±������������Զ��ָ�����״̬
	cout << "log finish" << endl;
}
/* system��ʹ����������������OS�ײ�Ĵ������ʹ�����Ϣ��ʹ����OS�ĳ������������ֵ.
 * ����Ϊ���������Ѿ���filesystem,asio�ȿ���ã����ұ�����ΪC++0x TR2.
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
	path p(str+13, str+14);	//ȡ�ַ����е�һ��б��
	cout << p << endl;
	assert(!p.empty());
	p /= "etc";		//ʹ�� operator/=׷��·��
	string filename("xinetd.conf");
	p.append(filename.begin(), filename.end());
	cout << p << endl;
	cout << system_complete(p) << endl;		// ����·���ڵ�ǰ�ļ�ϵͳ�е�����·��

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
	path p("d:/�ഺ��������׷Ѱʲô.txt");
	newfs::ifstream ifs(p.string().c_str());
	assert(ifs.is_open());
	cout << ifs.rdbuf();
}
int test_program_options(int ac, char* av[]){
	namespace po = boost::program_options;
	try {

	        po::options_description desc("Allowed options");
	        desc.add_options()
	            ("help,h", "produce help message���ǰ�����Ϣ")
	            ("compression,c", po::value<double>(), "set compression level")
				("verbose,v", "�����ϸ��Ϣ")
	        ;

	        po::variables_map vm;
	        po::store(po::parse_command_line(ac, av, desc), vm);
	        po::notify(vm);

	        if (vm.count("help")) {
	            cout << desc << "\n";
	            return 0;
	        }
	        if (vm.count("verbose")) {
				cout << "verbose������" << "\n";
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
