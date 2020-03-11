using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FindMyKids.TeamService.Models
{
    public class Recaptcha
    {
        private string m_Success;
        [JsonProperty("Success")]
        public string success
        {
            get { return m_Success; }
            set { m_Success = value; }
        }

        private List<string> m_ErrorCodes;
        [JsonProperty("error-codes")]
        public List<string> ErrorCodes
        {
            get { return m_ErrorCodes; }
            set { m_ErrorCodes = value; }
        }
        public static string Validate(string EncodeResponse)
        {
            var client = new System.Net.WebClient();
            string PrivateKey = "6LfEGOAUAAAAANn6kdOI8H48baxsnAsVTu31h1xd";
            var reply = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}",PrivateKey,EncodeResponse));
            var captchaResponse = JsonConvert.DeserializeObject<Recaptcha>(reply);
            return captchaResponse.success;
        }

    }
}
