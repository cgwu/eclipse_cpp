# ref: https://zh.wikibooks.org/wiki/CMake_入門

message(hello中国)

set(var hello)
message(${var})

message(------------------)
set(foo var)
message(${foo})
message(${${foo}})

# 字串 与 串列
set(foo1 this is a list)
set(foo2 this;is;a;list)
message(${foo1})
message(${foo2})

# Quoted String
set(a alpha beta gamma)  # 串列
set(b "alpa beta gamma") # 字串
set(c "alpha
beta 
gamma"
)     # 字串
message("a=${a}")
message("b=${b}")
message("c=${c}")

# Escapes
set(bar "alpha beta gamma")
message("\${bar}: ${bar}")

# 数学计算
math(EXPR var "1 + 2 * 3")
message("var = ${var}")

# 条件叙述
if(WIN32) # AND FALSE)
    # ON, 1, YES, TRUE, Y 时为真
    message("this is WIN32")
else()
    # OFF, 0, NO, FALSE, N, NOTFOUND, *-NOTFOUND, IGNORE 时为假
    message("this is not WIN32")
endif()

# 在条件式当中即使不加${}, if也会先尝试解释成变数.
# 下面两行意义相同
# if(foo)
# if(${foo})
# 下面两行意义相同
# if(foo AND bar)
# if(${foo} AND ${bar})

set(V alpha beta gamma)
message(${V})
message(------------foreach------------)
foreach(i ${V})
    message(${i})
endforeach()

# 函数与宏
# CMake有两种设计子程序的方式
# function ... endfunction
# macro ... endmacro
# 主要差别在于function会建立local的变数，而macro则会影响global变数

# 定义名为print1的macro
macro(print1 MESSAGE)
    set(k ${MESSAGE})
    message(${MESSAGE})
endmacro(print1)

# 定义名为print2的function
function(print2 MESSAGE)
    set(k ${MESSAGE})
    message(${MESSAGE})
endfunction(print2)

message("k=${k}")
print1("from function1")
message("k=${k}")
print2("from function2")
message("k=${k}")

# 逻辑运算
if(NOT false)  # NOT, AND, OR
    message("not false is true")
endif()

# 比较运算
if("2" LESS 3)
    message("2 less than 3")
endif()

if(4 GREATER "-1")
    message("4 greater than -1")
endif()

if("5" EQUAL 5)
    message("5 equal '5'")
endif()

# 字串比较
if("abc" STRLESS "abd")
    message("abc STRLESS abd")
endif()

if("abdc" STRGREATER "abd")
    message("abdc STRGREATER abd")
endif()

if("abcd" STREQUAL "ABCD")
    message("abcd EQUAL abcd")
else()
    message("abcd NOT EQUAL ABCD")
endif()

# Regular Expression 比对
#if(variable MATCHES regex)
#if(string MATCHES regex)

# 文件相关
#if(EXISTS file-name)
#if(EXISTS directory-name)
#if(file1 IS_NEWER_THAN file2)
#if(IS_ABSOLUTE path)

# if(COMMAND command-name)  #判断给定的cmd-name是否属于 指令、function、macro
# if(DEFINED variable-name) #是否被定义过



