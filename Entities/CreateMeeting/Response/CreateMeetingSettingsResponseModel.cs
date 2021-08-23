using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.CreateMeeting.Response
{
    public class CreateMeetingSettingsResponseModel
    {
        public string alternative_hosts { get; set; }
        public int approval_type { get; set; }
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool close_registration { get; set; }
        public bool cn_meeting { get; set; }
        public bool enforce_login { get; set; }
        public string enforce_login_domains { get; set; }
        public string[] global_dial_in_countries { get; set; }
    }
}
