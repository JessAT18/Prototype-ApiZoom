using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.CreateMeeting
{
    public class CreateMeetingResponseModel
    {
        //REF: https://marketplace.zoom.us/docs/api-reference/zoom-api/meetings/meetingcreate
        public string created_at { get; set; }
        public int duration { get; set; }
        public string host_id { get; set; }
        public ulong id { get; set; }
        public string join_url { get; set; }
        
    }
}
