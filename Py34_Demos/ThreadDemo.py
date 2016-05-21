import threading
import time


class FooThread(threading.Thread):
    def run(self):
        for _ in range(10):
            print(threading.current_thread().getName())
            time.sleep(1)

t1 = FooThread(name='send message')
t2 = FooThread(name='receive message')
t1.start()
t2.start()
