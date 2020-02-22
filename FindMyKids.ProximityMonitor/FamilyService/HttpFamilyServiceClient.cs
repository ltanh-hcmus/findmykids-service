using System;
using System.Net.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace FindMyKids.ProximityMonitor.FamilyService
{
    public class HttpFamilyServiceClient : IFamilyServiceClient
    {
        private readonly FamilyServiceOptions familyServiceOptions;

        private readonly ILogger logger;

        private HttpClient httpClient;
        
        public HttpFamilyServiceClient(ILogger<HttpFamilyServiceClient> logger,
            IOptions<FamilyServiceOptions> serviceOptions)
        {
            this.logger = logger;               
            this.familyServiceOptions = serviceOptions.Value;
            
            logger.LogInformation("Family Service HTTP client using URL {0}",
                familyServiceOptions.Url);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(familyServiceOptions.Url);
        }

        public Family GetFamily(Guid familyId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/families/{0}", familyId)).Result;

            Family familyResponse = null;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                familyResponse = JsonConvert.DeserializeObject<Family>(json);                
            }
            return familyResponse;
        }

        public Member GetMember(Guid familyId, Guid memberId)
        {
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/families/{0}/members/{1}", familyId, memberId)).Result;

            Member memberResponse = null;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                memberResponse = JsonConvert.DeserializeObject<Member>(json);
            }
            return memberResponse;
        }
    }
}