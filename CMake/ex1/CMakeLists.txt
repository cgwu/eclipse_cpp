cmake_minimum_required(VERSION 3.5)

#开启C++11编绎选项
#add_compile_options(-std=c++11)

project(ex1)
add_executable(ex1 main.c test_thread.cpp)
#自动引用 需要的编绎器功能
target_compile_features(ex1 PRIVATE cxx_range_for)

message("Your C++ compiler supports these C++ features:")
foreach(i ${CMAKE_CXX_COMPILE_FEATURES})
  message("${i}")
endforeach()
