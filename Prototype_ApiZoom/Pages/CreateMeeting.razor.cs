using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.Net;
using System.Text;

namespace Prototype_ApiZoom.Pages
{
    public partial class CreateMeeting
    {
        public string APISecret { get; set; } = "n44TnucAkdyzORP9Y1xa8EvKoWiZHlLLLeJb";
        public string APIKey { get; set; } = "1tTFpf0JQjCpjWlqdVmQZw";
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/jessica.aquino.torrez@gmail.com/meetings";

        private string Host, Join, Code;
        public string JSONResponse { get; set; }

        private void CreateNewMeeting()
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
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddJsonBody(new { topic = "Meeting with Ussain", duration = "10", start_time = "2021-03-20T05:00:00", type = "2" });
            request.AddHeader("authorization", String.Format("Bearer {0}", tokenString));

            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            int numericStatusCode = (int)statusCode;
            var jObject = JObject.Parse(restResponse.Content); //Issues con Blazor 3.1


            Host = (string)jObject["start_url"];
            Join = (string)jObject["join_url"];
            Code = Convert.ToString(numericStatusCode);

            //JSONResponse = (string)jObject.ToString(Formatting.Indented);
            JSONResponse = jObject.ToString(Formatting.Indented);
        }

    }
}
