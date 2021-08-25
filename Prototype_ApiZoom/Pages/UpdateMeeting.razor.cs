using Entities.Authentication;
using Entities.UpdateMeeting.Request;
using Microsoft.IdentityModel.Tokens;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_ApiZoom.Pages
{
    public partial class UpdateMeeting
    {
        public static AuthenticationModel authenticationInfo = new AuthenticationModel
        {
            APIKey = "YWOqpImNTEybugrfqSJK1w",
            APISecret = "sxdMvePBljYusrryQQxts6EE1qrdXSdzlDSa",
            userId = "pruebazoomapi@gmail.com"
        };
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/" + authenticationInfo.userId +"/meetings";
        public string JSONResponse { get; set; }
        public int numericStatusCode1 { get; set; }

        public UpdateMeetingRequestModel meetingRequest { get; set; }

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
        private void UpdateAMeeting(ulong meetingId = 77429197863)
        {
            Authentication();

            //Create the request
            var client = new RestClient("https://api.zoom.us/v2/meetings/{meetingId}");
            var request = new RestRequest(Method.PATCH);

            request.AddUrlSegment("meetingId", meetingId);
            request.RequestFormat = DataFormat.Json;

            meetingRequest = new UpdateMeetingRequestModel {
                topic = "Meeting with Jessica Aquino",
                type = 2,
                start_time = "2021-10-23T23:15:00",
                duration = 60,
                timezone = "America/La_Paz",
                password = "Passw0rd",
                agenda = "My meeting",
                settings = new UpdateMeetingRequestSettingsModel {
                    host_video = false,
                    in_meeting = false,
                    join_before_host = true,
                    mute_upon_entry = false,
                    participant_video = false,
                    registrants_confirmation_email = true,
                    use_pmi = false,
                    waiting_room = false,
                    watermark = false,
                    approval_type = 0,
                    //alternative_hosts = "",
                }
            };
            request.AddJsonBody(meetingRequest);

            request.AddHeader("authorization", String.Format("Bearer {0}", authenticationInfo.tokenString));

            IRestResponse restResponse = client.Execute(request);
            HttpStatusCode statusCode = restResponse.StatusCode;
            numericStatusCode1 = (int)statusCode;
            //var jObject = JObject.Parse(restResponse.Content); 
            JSONResponse = restResponse.Content;
            //JSONResponse = (string)jObject.ToString(Formatting.Indented);
            //JSONResponse = jObject.ToString(Formatting.Indented);
        }
    }
}
