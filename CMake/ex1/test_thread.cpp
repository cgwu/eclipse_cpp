/*
 * test_thread.cpp
 *
 *  Created on: 2016Äê5ÔÂ25ÈÕ
 *      Author: geeks2016
 */

#include <iostream>
#include <pthread.h>
#include "util.cpp"

using namespace std;

static long long total = 0;
pthread_mutex_t m = PTHREAD_MUTEX_INITIALIZER;

void* func(void*){
	long long i;
	for(i=0; i<= 100LL; ++i){
		pthread_mutex_lock(&m);
		total += i;
		pthread_mutex_unlock(&m);
	}
	return NULL;
}
int main(int argc, char *argv[])
{
	pthread_t thread1, thread2;
	if(pthread_create(&thread1, NULL, &func, NULL)){
		throw;
	}
	if(pthread_create(&thread2, NULL, &func, NULL)){
		throw;
	}
	pthread_join(thread1,NULL);
	pthread_join(thread2,NULL);

	cout << total << endl;
	auto i = 1234;
	cout << i << endl;
	decltype(i) d = 2469;
	cout << d << endl;
	cout << "------²âÊÔinclude cpp -------" << endl;
	foo();
	cin.get();

	return 0;
}
//g++ thread_demo.cpp -std=c++11 -lpthread

