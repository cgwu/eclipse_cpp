using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ServiceStack.Redis;

namespace TestRedisCli
{
    public class Todo
    {
        public long Id { get; set; }
        public string Content { get; set; }
        public override string ToString()
        {
            return string.Format($"Id={Id},Content={Content}", Id, Content);
        }
    }

    public class Test_ServiceStackRedis
    {
        public static void TestRedis()
        {
            // 参考： https://github.com/ServiceStack/ServiceStack.Redis

            // CREATE A WINDSOR CONTAINER OBJECT AND REGISTER THE INTERFACES, AND THEIR CONCRETE IMPLEMENTATIONS.
            const string redisConnectionString = "redis://192.168.5.102:6379";
            var container = new WindsorContainer();
            //container.Register(Component.For<IRedisClientsManager>().ImplementedBy<RedisManagerPool>());
            container.Register(
                Component.For<IRedisClientsManager>().ImplementedBy<RedisManagerPool>()
                .DependsOn(Dependency.OnValue("host", redisConnectionString))  // 默认为singleton: .LifestyleSingleton()
            );
            var clientsManager = container.Resolve<IRedisClientsManager>();
            var clientsManager2 = container.Resolve<IRedisClientsManager>();    // test is singleton
            Console.WriteLine("redisClientManagers are equal:" + object.ReferenceEquals(clientsManager, clientsManager2));
            using (IRedisClient redis = clientsManager.GetClient())
            {
                // 1. 字符串
                redis.SetValue("hello", "china中国");
                var result = redis.GetValue("hello");
                Console.WriteLine(result);

                long ret = redis.IncrementValue("counter");
                Console.WriteLine("ret of Inc:" + ret);


                //Access Typed API
                var redisTodos = redis.As<Todo>();

                redisTodos.Store(new Todo
                {
                    Id = redisTodos.GetNextSequence(),
                    Content = "Learn Redis",
                });

                var todo = redisTodos.GetById(1);
                Console.WriteLine(todo);

                //Access Native Client
                var redisNative = (IRedisNativeClient)redis;

                //redisNative.Incr("counter");
                //List<string> days = redisNative.("days", 0, -1);
                redisNative.RPush("list-key", Encoding.UTF8.GetBytes("新的item ##" + ret));


            }

            //container.Register(Component.For<Main>());
            //container.Register(Component.For<IDependency1>().ImplementedBy<Dependency1>());
            //container.Register(Component.For<IDependency2>().ImplementedBy<Dependency2>());

            // CREATE THE MAIN OBJECT AND INVOKE ITS METHOD(S) AS DESIRED.
            //var mainThing = container.Resolve<Main>();
            //mainThing.DoSomething();
        }
    }
}
