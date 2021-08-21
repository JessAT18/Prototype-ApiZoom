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
        public string APISecret { get; set; } = "ERNhIO5sZOnrL1lh4C2LjLZ9O4xWu8tuN9bL";
        public string APIKey { get; set; } = "1tTFpf0JQjCpjWlqdVmQZw";
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/jessica.aquino.torrez@gmail.com/meetings";
        public string JSONResponse { get; set; }
        public int numericStatusCode1 { get; set; }
        public int numericStatusCode2 { get; set; }
        private void GetAllMeetings()
        {
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            //var apiSecret = "n44TnucAkdyzORP9Y1xa8EvKoWiZHlLLLeJb";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(APISecret);

            //Create the Token Descriptor and change it to Token String for the Authorization Header
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = APIKey,
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256), //No es compatible con Blazor 5.0
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //Create the request
            var client = new RestClient(ClientURL);
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;
            request.AddParameter("userId","jessica.aquino.torrez@gmail.com");

            //request.AddJsonBody(new { userId = "jessica.aquino.torrez@gmail.com" });
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));

            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            numericStatusCode1 = (int)statusCode;
            //var jObject = JObject.Parse(restResponse.Content); 
            JSONResponse = restResponse.Content;
            //JSONResponse = (string)jObject.ToString(Formatting.Indented);
            //JSONResponse = jObject.ToString(Formatting.Indented);
        }
        private void GetAMeeting()
        {

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var now = DateTime.UtcNow;
            //var apiSecret = "n44TnucAkdyzORP9Y1xa8EvKoWiZHlLLLeJb";
            byte[] symmetricKey = Encoding.ASCII.GetBytes(APISecret);

            //Create the Token Descriptor and change it to Token String for the Authorization Header
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = APIKey,
                Expires = now.AddSeconds(300),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(symmetricKey), SecurityAlgorithms.HmacSha256), //No es compatible con Blazor 5.0
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            //Create the request
            var client = new RestClient("https://api.zoom.us/v2/meetings/{meetingId}");
            var request = new RestRequest(Method.GET);
            request.RequestFormat = DataFormat.Json;

            request.AddUrlSegment("meetingId", 74053869211);
            //request.AddJsonBody(new { userId = "jessica.aquino.torrez@gmail.com" });
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));

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
