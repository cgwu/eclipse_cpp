#include <stdio.h>
#define LOG(...) {\
	fprintf(stderr, "[%s] Line %d:\t", __FILE__, __LINE__);\
	fprintf(stderr, __VA_ARGS__);\
	fprintf(stderr, "\n" ); \
}
int main()
{
	LOG("msg=%s","Hello");
	LOG("中国");
	return 0;
}