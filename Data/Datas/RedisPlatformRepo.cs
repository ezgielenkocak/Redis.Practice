using Castle.Core.Configuration;
using Model.Models;
using StackExchange.Redis;
using JsonSerializer = System.Text.Json.JsonSerializer;
using Microsoft.Extensions.Configuration;
using IConfiguration = Microsoft.Extensions.Configuration.IConfiguration;

namespace Data.Datas
{
    public class RedisPlatformRepo:IRedisPlatformRepo
    {
       
        public RedisPlatformRepo(IConfiguration configuration)
        {
            ConfigurationOptions options = new()
            {

                AbortOnConnectFail = false,
                ConnectTimeout = 300000,
            };
           
        }
        public RedisPlatformRepo()
        {

        }

        public bool CreatePlatform(Platform platform)
        {

            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:1453");
            IDatabase db = redis.GetDatabase();
            if (platform==null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            var serialPlatform = JsonSerializer.Serialize(platform);
            db.StringSet(platform.Id, serialPlatform);
            db.SetAdd("PlatformsSet", serialPlatform);
            return true;
        }

        public IEnumerable<Platform> GetAllPlatform()
        {
    
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost:1453");
            IDatabase db = redis.GetDatabase();

            var completeSet = db.SetMembers("PlatformsSet");
            if (completeSet.Length >0)
            {
                var obj = Array.ConvertAll(completeSet, val => JsonSerializer.Deserialize<Platform>(val)).ToList();
                return obj;
            }
            return null;
        }

        public Platform? GetPlatformById(string id)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:1453");
            IDatabase db = redis.GetDatabase();

            var platform = db.StringGet(id);

            if (!string.IsNullOrEmpty(platform))
            {
                return JsonSerializer.Deserialize<Platform>(platform); //geri dönmek için nesneyi deserialize hale getirdim
            }
            return null;
        }

        public bool CreatePlatformWithHash(Platform platform)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1.1453");
            IDatabase db=redis.GetDatabase();
            if (platform==null)
            {
                throw new ArgumentNullException(nameof(platform));
            }
            var serialPlatform = JsonSerializer.Serialize(platform);
            db.HashSet("hashplatform", new HashEntry[] { new HashEntry(platform.Id, platform.Name) });
            return true;

        }

        public IEnumerable<Platform?>? GetAllPlatformHash()
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:1453");
        
            IDatabase db=redis.GetDatabase();
            var completeHash = db.SetMembers("hashplatform");
            if (completeHash.Length>0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<Platform>(val)).ToList();
                return obj;
            }
            return null; 
        }
    }
}
