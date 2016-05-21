import util

'''
print("Hello中国")
for x in range(1,10):
    print(x)
'''


def sum_args(*args):
    total = 0
    for x in args:
        total += x
    return total

print(sum_args(1))
print(sum_args(1, 2, 3, 4))


# keyword arguments
def dumb_sentence(name='sam', action='do'):
    print(name, action, sep='')


dumb_sentence()
dumb_sentence(action='eat')
dumb_sentence(action='sleep', name='张三')

util.foo()

# util.download('http://www.baidu.com/img/bd_logo1.png','baidulogo.png')

util.write_file()
util.read_file()
