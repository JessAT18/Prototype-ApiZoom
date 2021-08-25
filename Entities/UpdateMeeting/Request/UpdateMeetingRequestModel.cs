using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.UpdateMeeting.Request
{
    public class UpdateMeetingRequestModel
    {
        public UpdateMeetingRequestModel()
        {
            settings = new UpdateMeetingRequestSettingsModel();
        }
        public string topic { get; set; }
        public int type { get; set; }
        public string start_time { get; set; }
        public int duration { get; set; }
        public string timezone { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public UpdateMeetingRequestSettingsModel settings { get; set; }
    }
}
