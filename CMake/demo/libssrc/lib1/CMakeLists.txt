project(lib1)

# add headers and sources here
set (${PROJECT_NAME}_headers ./lib1.h)
set (${PROJECT_NAME}_sources lib1.cpp)

# create binaries
add_library (${PROJECT_NAME} STATIC ${${PROJECT_NAME}_headers} ${${PROJECT_NAME}_sources})
install(TARGETS ${PROJECT_NAME}
    RUNTIME DESTINATION bin
    LIBRARY DESTINATION lib
    ARCHIVE DESTINATION lib/static)

