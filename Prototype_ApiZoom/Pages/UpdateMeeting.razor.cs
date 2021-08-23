using Entities.CreateMeeting.Authentication;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype_ApiZoom.Pages
{
    public partial class UpdateMeeting
    {
        public AuthenticationModel authenticationInfo = new AuthenticationModel
        {
            APIKey = "1tTFpf0JQjCpjWlqdVmQZw",
            APISecret = "ERNhIO5sZOnrL1lh4C2LjLZ9O4xWu8tuN9bL",
            userId = "jessica.aquino.torrez@gmail.com"
        };
        public string ClientURL { get; set; } = "https://api.zoom.us/v2/users/jessica.aquino.torrez@gmail.com/meetings";
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
    }
}
