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
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("192.168.5.102:6379");
            IDatabase db = redis.GetDatabase();
            string value = "abcdefg中国";
            db.StringSet("mykey", value);

            value = db.StringGet("mykey");
            Console.WriteLine(value); // writes: "abcdefg"

            TestRedisAsync(db);
        }


        public async static Task TestRedisAsync(IDatabase db)
        {
            string value = "abcdefg世界Async";
            await db.StringSetAsync("mykeyasync", value);
            value = await db.StringGetAsync("mykeyasync");
            Console.WriteLine(value); // writes: "abcdefg"
        }

    }
}
