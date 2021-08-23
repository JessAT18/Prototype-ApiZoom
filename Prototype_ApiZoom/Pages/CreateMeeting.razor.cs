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
using Entities.CreateMeeting.Request;
using Entities.CreateMeeting.Authentication;

namespace Prototype_ApiZoom.Pages
{
    public partial class CreateMeeting
    {
        public AuthenticationModel authenticationInfo = new AuthenticationModel
        {
            APIKey = "1tTFpf0JQjCpjWlqdVmQZw",
            APISecret = "ERNhIO5sZOnrL1lh4C2LjLZ9O4xWu8tuN9bL",
            userId = "jessica.aquino.torrez@gmail.com"
        };
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/jessica.aquino.torrez@gmail.com/meetings";

        private string Host, Join, Code;
        public string JSONResponse { get; set; }

        public CreateMeetingRequestModel meetingRequest { get; set; }

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

        private void CreateNewMeeting()
        {
            Authentication();

            //Create the request
            var client = new RestClient(ClientURL);
            var request = new RestRequest(Method.POST);
            request.RequestFormat = DataFormat.Json;

            //Charging data to our model
            meetingRequest = new CreateMeetingRequestModel
            {
                topic = "Meeting with Jessica 3",
                type = 2, //1 Instant, 2 Scheduled, 3 Recurring with no fixed time, 4 Recurring with fixed time
                start_time = "2021-08-21T17:15:00", //Z doesn't let to select a timezone. Bolivian timezone is America/Santiago
                duration = 40, //In minutes
                schedule_for = "jessica.aquino.torrez@gmail.com", //A zoom account can have other users...
                timezone = "America/Santiago",
                password = "MyPassw0rd",
                agenda = "Agenda del meeting customizable", //2000 characters
                recurrence = {
                    type = 3, //1 Daily, 2 Weekly, 3 Monthly
                    repeat_interval = 2, //Every two monts
                    //weekly_days = "a", type = 2 1-7
                    monthly_day = 21, //type = 3, 1-31
                    //monthly_week = -1, //type = 3, -1 - 4
                    //monthly_week_day = -1, //type = 3, monthly_week used, 1-7
                    end_times = 2,
                    end_date_time = "2021-10-21T17:15:00Z"
                },
                settings =
                {
                    host_video = false,
                    participant_video = false,
                    cn_meeting = false,
                    in_meeting = false,
                    join_before_host = false,
                    mute_upon_entry = true,
                    watermark = false,
                    use_pmi = false,
                    approval_type = 2, //0-2
                    registration_type = 3, //1-3
                    audio = "both", //both, telephony, voip
                    auto_recording = "none", //local, cloud, none
                    enforce_login = false,
                    enforce_login_domains = "a",
                    //alternative_hosts = "5592917843", //email addresses or IDs
                    /*global_dial_in_countries = new string[2]{
                        "a",
                        "b"
                    },*/
                    registrants_email_notification = true
                }
            };

            request.AddJsonBody(meetingRequest);

            //request.AddJsonBody(new { topic = "Meeting with me", duration = "10", start_time = "2021-03-20T05:00:00", type = "2" });
            request.AddHeader("authorization", String.Format("Bearer {0}", authenticationInfo.tokenString));

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
