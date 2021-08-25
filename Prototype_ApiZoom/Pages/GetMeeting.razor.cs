using Entities.Authentication;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_ApiZoom.Pages
{
    public partial class GetMeeting
    {
        public static AuthenticationModel authenticationInfo = new AuthenticationModel
        {
            APIKey = "YWOqpImNTEybugrfqSJK1w",
            APISecret = "sxdMvePBljYusrryQQxts6EE1qrdXSdzlDSa",
            userId = "pruebazoomapi@gmail.com"
        };
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/" + authenticationInfo.userId + "/meetings";
        public string JSONResponse { get; set; }
        public int numericStatusCode1 { get; set; }
        public int numericStatusCode2 { get; set; }

        private void Authentication()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            byte[] symmetricKey = Encoding.ASCII.GetBytes(authenticationInfo.APISecret);

            //Create the Token Descriptor and change it to Token String for the Authorization Header
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = authenticationInfo.APIKey,
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256),
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            authenticationInfo.tokenString = tokenHandler.WriteToken(token);
        }
        private void GetAllMeetings()
        {
            Authentication();
            //Create the request
            var client = new RestClient(ClientURL);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("userId", authenticationInfo.userId);

            //request.AddJsonBody(new { userId = "jessica.aquino.torrez@gmail.com" });
            request.AddHeader("authorization", String.Format("Bearer {0}", authenticationInfo.tokenString));

            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            numericStatusCode1 = (int)statusCode;
            //var jObject = JObject.Parse(restResponse.Content); 
            JSONResponse = restResponse.Content;
            //JSONResponse = (string)jObject.ToString(Formatting.Indented);
            //JSONResponse = jObject.ToString(Formatting.Indented);
        }
        private void GetAMeeting(ulong meetingId = 77429197863)
        {
            Authentication();

            //Create the request
            var client = new RestClient("https://api.zoom.us/v2/meetings/{meetingId}");
            var request = new RestRequest(Method.GET);

            request.AddUrlSegment("meetingId", meetingId);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("authorization", String.Format("Bearer {0}", authenticationInfo.tokenString));

            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            numericStatusCode2 = (int)statusCode;
            //var jObject = JObject.Parse(restResponse.Content); 
            JSONResponse = restResponse.Content;
            //JSONResponse = (string)jObject.ToString(Formatting.Indented);
            //JSONResponse = jObject.ToString(Formatting.Indented);
        }
    }
}
