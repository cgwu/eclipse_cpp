
class Enemy:
    cnt = 0     # 类变量

    def __init__(self, x=4):
        self.life = x       # 实例变量

    def attack(self):
        Enemy.cnt += 1
        print(Enemy.cnt, 'attack me #################')
        self.life -= 1

    def check_life(self):
        if self.life <= 0:
            print("I'm dead.")
        else:
            print(str(self.life), "life left")


class JapanEnemy(Enemy):
    def check_life(self):
        super().check_life()
        print("check japan enemy life done")


enemy1 = Enemy()
enemy2 = JapanEnemy()
enemy1.attack()
enemy1.check_life()
enemy2.check_life()
enemy2.attack()
enemy2.attack()
enemy2.check_life()
print("------------------")
enemy2.check_life()

