using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace FindMyKids.RealityService.Location.Redis
{
    public class RedisLocationCache : ILocationCache
    {
        private ILogger logger;

        private IConnectionMultiplexer connection;

        public RedisLocationCache(ILogger<RedisLocationCache> logger,
            IConnectionMultiplexer connectionMultiplexer)
        {
            this.logger = logger;
            this.connection = connectionMultiplexer;

            logger.LogInformation($"Using redis location cache - {connectionMultiplexer.Configuration}");
        }

        public RedisLocationCache(ILogger<RedisLocationCache> logger,
            ConnectionMultiplexer connectionMultiplexer) : this(logger, (IConnectionMultiplexer)connectionMultiplexer)
        {

        }

        public IList<MemberLocation> GetMemberLocations(Guid familyId)
        {
            IDatabase db = connection.GetDatabase();

            RedisValue[] vals = db.HashValues(familyId.ToString());

            return ConvertRedisValsToLocationList(vals);
        }

        public void Put(Guid familyId, MemberLocation memberLocation)
        {
            IDatabase db = connection.GetDatabase();

            db.HashSet(familyId.ToString(), memberLocation.MemberID.ToString(), memberLocation.ToJsonString());
        }

        public MemberLocation Get(Guid familyId, Guid memberId)
        {
            IDatabase db = connection.GetDatabase();

            var value = (string)db.HashGet(familyId.ToString(), memberId.ToString());
            MemberLocation ml = MemberLocation.FromJsonString(value);
            return ml;
        }

        private IList<MemberLocation> ConvertRedisValsToLocationList(RedisValue[] vals)
        {
            List<MemberLocation> memberLocations = new List<MemberLocation>();

            for (int x = 0; x < vals.Length; x++)
            {
                string val = (string)vals[x];
                MemberLocation ml = MemberLocation.FromJsonString(val);
                memberLocations.Add(ml);
            }

            return memberLocations;
        }
    }

    public static class RedisExtensions
    {
        public static IServiceCollection AddRedisConnectionMultiplexer(this IServiceCollection services,
            IConfiguration config)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var redisConfig = config.GetSection("redis:configstring").Value;

            services.AddSingleton(typeof(IConnectionMultiplexer), ConnectionMultiplexer.ConnectAsync(redisConfig).Result);
            return services;
        }
    }
}