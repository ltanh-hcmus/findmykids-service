using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;

namespace FindMyKids.EventProcessor.Location.Redis
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

        // This is a hack required to get injection working
        // because Steeltoe's redis connector injected the concrete class as binding
        // and not the interface.
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
}