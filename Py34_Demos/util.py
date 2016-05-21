import urllib.request


def foo():
    print("foo func called.")


def download(url, filename):
    urllib.request.urlretrieve(url, filename)
    print("download complete.")


def write_file():
    fw = open('some.txt', 'w')
    fw.write('hello world\n')
    fw.write('中国 Hello World\n')
    fw.close()


def read_file():
    fr = open('some.txt', 'r')
    content = fr.read()
    print(content)
    fr.close()
