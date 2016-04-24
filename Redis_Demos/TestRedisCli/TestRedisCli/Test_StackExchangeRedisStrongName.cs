using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRedisCli
{
    /// <summary>
    /// 参考： https://github.com/StackExchange/StackExchange.Redis
    /// </summary>
    public class Test_StackExchangeRedisStrongName
    {
        public static void TestRedis()
        {
            //eg: ConnectionMultiplexer.Connect("server1:6379,server2:6379");
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.5.102:6379");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg中国";
            db.StringSet("mykey", value);

            value = db.StringGet("mykey");
            Console.WriteLine(value); // writes: "abcdefg"

            TestRedisAsync(db);

            Console.WriteLine("----------------------");
            TestPubSub(redis);
        }


        public async static Task TestRedisAsync(IDatabase db)
        {
            string value = "abcdefg世界Async";
            await db.StringSetAsync("mykeyasync", value);
            value = await db.StringGetAsync("mykeyasync");
            Console.WriteLine(value); // writes: "abcdefg"
        }

        /// <summary>
        /// 测试发布订阅
        /// </summary>
        /// <param name="redis"></param>
        public static void TestPubSub(ConnectionMultiplexer redis)
        {
            ISubscriber sub = redis.GetSubscriber();
            sub.Subscribe("messages", (channel, message) => {
                Console.WriteLine("第一个Sub:"+(string)message);
            });
            sub.Subscribe("messages", (channel, message) => {
                Console.WriteLine("第二个Sub:" + (string)message);
            });

            sub.Subscribe("m1", (channel, message) => {
                Console.WriteLine("第一个Sub:" + (string)message);
            });
           
            sub.Publish("messages", "hello这是发布的消息");
            sub.Publish("m1", "m1消息来了!");
        }

    }
}
