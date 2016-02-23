//============================================================================
// Name        : HelloWorld.cpp
// Author      : cg
// Version     :
// Copyright   : Your copyright notice
// Description : Hello World in C++, Ansi-style
//============================================================================

#include <iostream>
#include <cstdio>

#include <boost/typeof/typeof.hpp>

//#include <boost/log/core.hpp>
#include <boost/log/trivial.hpp>
//#include <boost/log/expressions.hpp>
//#include <boost/log/sinks/text_file_backend.hpp>
//#include <boost/log/utility/setup/file.hpp>
//#include <boost/log/utility/setup/common_attributes.hpp>
//#include <boost/log/sources/severity_logger.hpp>
//#include <boost/log/sources/record_ostream.hpp>
#include <boost/filesystem.hpp>
#include <boost/log/sources/logger.hpp>
#include <boost/log/sources/record_ostream.hpp>
#include <boost/log/sources/global_logger_storage.hpp>
#include <boost/log/utility/setup/file.hpp>
#include <boost/log/utility/setup/common_attributes.hpp>
#include <boost/log/sinks/text_ostream_backend.hpp>
#include <boost/log/attributes/named_scope.hpp>
#include <boost/log/expressions.hpp>
#include <boost/log/support/date_time.hpp>
#include <boost/log/detail/format.hpp>
#include <boost/log/detail/thread_id.hpp>

using namespace std;
namespace logging = boost::log;
namespace sinks = boost::log::sinks;
namespace src = boost::log::sources;
namespace expr = boost::log::expressions;
namespace attrs = boost::log::attributes;
namespace keywords = boost::log::keywords;

void init()
{
//	logging::add_file_log("log/sample.log");
	///* keywords::format = "%TimeStamp% [%Severity%]: %Message%" */
	BOOST_AUTO(pSink,
			logging::add_file_log
	(
		keywords::open_mode = std::ios::app,
		keywords::file_name = "log/%Y-%m-%d_%N.log ",
		keywords::rotation_size = 10 * 1024 * 1024,
		keywords::time_based_rotation = sinks::file::rotation_at_time_point(0, 0, 0),
		keywords::format =
			  (
				expr::stream
				<<expr::format_date_time< boost::posix_time::ptime >("TimeStamp", "%Y-%m-%d %H:%M:%S.%f")
				<< " <" << expr::attr< boost::log::aux::thread::id >("ThreadID")
				<< "> [" << logging::trivial::severity
				<< "] " << expr::smessage
			  )
	) );
	 // �����д��� ������ʵʱ�İ���־д��ȥ, ���ǵȴ�����������,���߳��������˳�ʱд��,�������ĺô��Ǽ���IO����.���Ч��,  ���������ﲻ��Ҫ��. ��Ϊ�ҵĳ�����ܻ��쳣�˳�.
	pSink->locked_backend()->auto_flush(true);//ʹ��־ʵʱ����
	//pSink->imbue(std::locale("zh_CN.UTF-8")); // ���ػ�
    logging::core::get()->set_filter
    (
        logging::trivial::severity >= logging::trivial::warning
    );
}
int main_log_demo() {
	/*
	cout << "!!!Hello World!!!�й�" << endl;
	int a = 1, b = 2, c = 0;
	c = a + b;
	cout << c << endl;
	*/

	init();

	/*
 	// ʹ�ö�̬����Ҫ���� -DBOOST_LOG_DYN_LINK
	BOOST_LOG_TRIVIAL(trace) << "A trace severity message";
    BOOST_LOG_TRIVIAL(debug) << "A debug severity message";
    BOOST_LOG_TRIVIAL(info) << "An informational severity message";
    BOOST_LOG_TRIVIAL(warning) << "A warning severity message";
    BOOST_LOG_TRIVIAL(error) << "An error severity message";
    BOOST_LOG_TRIVIAL(fatal) << "A fatal severity message����һ����������!";
	*/

	logging::add_common_attributes();
	using namespace logging::trivial;
	src::severity_logger< severity_level > lg;
	BOOST_LOG_SEV(lg, trace) << "A trace severity message";
	BOOST_LOG_SEV(lg, debug) << "A debug severity message";
	BOOST_LOG_SEV(lg, info) << "An informational severity message";
	BOOST_LOG_SEV(lg, warning) << "A warning severity message";
	BOOST_LOG_SEV(lg, error) << "An error severity message";
	BOOST_LOG_SEV(lg, fatal) << "A fatal severity message,����������Ϣ!";

	//getchar();
	return 0;
}
