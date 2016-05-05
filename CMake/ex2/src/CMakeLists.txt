
cmake_minimum_required(VERSION 2.6)
project(ex2)
add_executable(ex2 main.c calc.c)

# 语法：WIN32以WinMain作为程序进入点WinMain,如果
# 没有特别指定连结成视窗程式会发生
# _mainCRTStartup连接错误。
# add_executable(<name> [WIN32] [MACOSX_BUNDLE] 
#                       [EXCLUDE_FROM_ALL] 
#                       source1 source2 ... sourceN)

message("CMAKE_SOURCE_DIR=${CMAKE_SOURCE_DIR}")
message("CMAKE_BINARY_DIR=${CMAKE_BINARY_DIR}")

