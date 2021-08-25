using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.UpdateMeeting.Request
{
    public class UpdateMeetingRequestSettingsModel
    {
        public bool host_video { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool participant_video { get; set; }
        public bool registrants_confirmation_email { get; set; }
        public bool use_pmi { get; set; }
        public bool waiting_room { get; set; }
        public bool watermark { get; set; }
        public int approval_type { get; set; }
        public string alternative_hosts { get; set; }
        //Global dial in countries...
    }
}
