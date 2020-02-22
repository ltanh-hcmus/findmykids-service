using System;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using FindMyKids.LocationReporter.Models;

namespace FindMyKids.LocationReporter.Services
{
    public class HttpFamilyServiceClient : IFamilyServiceClient
    {        
        private readonly ILogger logger;

        private HttpClient httpClient;

        public HttpFamilyServiceClient(
            IOptions<FamilyServiceOptions> serviceOptions,
            ILogger<HttpFamilyServiceClient> logger)
        {
            this.logger = logger;
               
            var url = serviceOptions.Value.Url;

            logger.LogInformation("Family Service HTTP client using URL {0}", url);

            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(url);
        }
        public Guid GetFamilyForMember(Guid memberId)
        {                            
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = httpClient.GetAsync(String.Format("/members/{0}/family", memberId)).Result;

            FamilyIDResponse familyIdResponse;
            if (response.IsSuccessStatusCode) {
                string json = response.Content.ReadAsStringAsync().Result;
                familyIdResponse = JsonConvert.DeserializeObject<FamilyIDResponse>(json);
                return familyIdResponse.FamilyID;
            }
            else {
                return Guid.Empty;
            }            
        }
    }

    public class FamilyIDResponse
    {
        public Guid FamilyID { get; set; }
    }
}