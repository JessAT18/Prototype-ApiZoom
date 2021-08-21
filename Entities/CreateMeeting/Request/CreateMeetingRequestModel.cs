using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.CreateMeeting.Request
{
    public class CreateMeetingRequestModel
    {
        //REF: https://marketplace.zoom.us/docs/api-reference/zoom-api/meetings/meetingcreate
        public CreateMeetingRequestModel()
        {
            recurrence = new CreateMeetingRecurrenceModel();
            settings = new CreateMeetingSettingsModel();
        }
        public string topic { get; set; }
        public int type { get; set; }
        public string start_time { get; set; }
        public int duration { get; set; }
        public string schedule_for { get; set; }
        public string timezone { get; set; }
        public string password { get; set; }
        public string agenda { get; set; }
        public CreateMeetingRecurrenceModel recurrence { get; set; }
        public CreateMeetingSettingsModel settings { get; set; }
    }
}
