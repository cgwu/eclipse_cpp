SET(LIBHELLO_SRC hello.c)
ADD_LIBRARY(hello_static STATIC ${LIBHELLO_SRC})
# 设置目标文件输出目录
SET(LIBRARY_OUTPUT_PATH ${PROJECT_BINARY_DIR})
SET_TARGET_PROPERTIES(hello_static PROPERTIES OUTPUT_NAME "hello")
GET_TARGET_PROPERTY(OUTPUT_VALUE hello_static OUTPUT_NAME)
MESSAGE(STATUS "This is the hello_static OUTPUT_NAME:${OUTPUT_VALUE}")

