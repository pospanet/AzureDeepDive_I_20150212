using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace RedisCache
{
	class Program
	{
		static void Main(string[] args)
		{
			ConfigurationOptions options = new ConfigurationOptions();
			options.EndPoints.Add("devcamp.redis.cache.windows.net");
			options.Ssl = true;
			options.Password = "pwd=";
			ConnectionMultiplexer connection = ConnectionMultiplexer.Connect(options);
			IDatabase cache = connection.GetDatabase();
			const string Key = "Key";
			string value = "value";
			cache.StringSet(Key, value);
			value = cache.StringGet(Key);
		}
	}
}
