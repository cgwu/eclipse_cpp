project(dll1)

# add headers and sources
set (${PROJECT_NAME}_headers dll1.h)
set (${PROJECT_NAME}_sources dll1.cpp)

if(MINGW)
#    set(CMAKE_EXE_LINKER_FLAGS "-static")
#    set(CMAKE_EXE_LINKER_FLAGS_RELEASE "-s")
#	 add_compile_options(-fvisibility=hidden)
    message("MINGW±ª∂®“Â")
endif()


# create library
add_library (${PROJECT_NAME} SHARED ${${PROJECT_NAME}_headers} ${${PROJECT_NAME}_sources})

# linker
target_link_libraries(${PROJECT_NAME})

# configure install
install(TARGETS ${PROJECT_NAME}
    RUNTIME DESTINATION bin
    LIBRARY DESTINATION lib
    ARCHIVE DESTINATION lib/static)

