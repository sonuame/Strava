using System;
using System.Collections.Generic;
using Strava.NET.Api;
using Strava.NET.Client;
using Strava.NET.Model;
using Newtonsoft.Json;

namespace StravaClient
{
    public class TokenModel
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_at { get; set; }
    }
    class Program
    {
        static TokenModel token;
        static void Main(string[] args)
        {
            token = RenewToken();
            Configuration.ApiKey.Add("access_token", token.access_token);
            Configuration.ApiKey.Add("refresh_token", token.refresh_token);
            //Configuration.DefaultApiClient.AddDefaultHeader("Authorization", "Bearer " + Configuration.ApiKey["access_token"]);
            GetActivity("Mobility Workout");
        }

        static TokenModel RenewToken()
        {
            Dictionary<string, string> values = new Dictionary<string, string>();
            values.Add("grant_type", "refresh_token");
            values.Add("client_id", "5520");
            values.Add("client_secret", "c5d3cbf06f8ea359f29f41967c5538d5dbe41306");
            values.Add("refresh_token", "7039eb70dfab3517b12134c4f8ae2c69d5986386");
            Configuration.DefaultApiClient.RestClient.BaseUrl = "https://www.strava.com/oauth";
            RestSharp.RestResponse result = Configuration.DefaultApiClient.CallApi("token", RestSharp.Method.POST, values,
                "grant_type=refresh_token&client_id=5520&client_secret=c5d3cbf06f8ea359f29f41967c5538d5dbe41306&refresh_token=7039eb70dfab3517b12134c4f8ae2c69d5986386",
                values,
                values, new Dictionary<string, RestSharp.FileParameter>(), new string[0] { }) as RestSharp.RestResponse;
            if(result.StatusCode == System.Net.HttpStatusCode.OK)
            {
                Configuration.DefaultApiClient.RestClient.BaseUrl = Configuration.DefaultApiClient.BasePath;
                return JsonConvert.DeserializeObject<TokenModel>(result.Content);
            }
            return new TokenModel();
        }


        static void GetActivity(string ActivityName)
        {
            var apiInstance = new ClubsApi();
            

            try
            {
                // List Athlete Activities
                var clubs = apiInstance.GetLoggedInAthleteClubs(null, null);
                
            }
            catch (Exception e)
            {
               
            }
        }

    }
}
