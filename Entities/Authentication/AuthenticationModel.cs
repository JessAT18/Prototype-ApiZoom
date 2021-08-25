using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Authentication
{
    public class AuthenticationModel
    {
        public string userId { get; set; }
        public string APIKey { get; set; }
        public string APISecret { get; set; }
        public string tokenString { get; set; }
    }
}
