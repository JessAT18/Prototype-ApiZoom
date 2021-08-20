using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class CreateMeetingRequestModel
    {
        //REF: https://marketplace.zoom.us/docs/api-reference/zoom-api/meetings/meetingcreate
        /*[Key]
        public int MeetingId { get; set; }
        */
        [Required]
        [StringLength(200)]
        public string topic { get; set; }
        [Required]
        public int type { get; set; }
        [Required]
        public DateTime start_time { get; set; }
        [Required]
        public int duration { get; set; }
        [StringLength(200)]
        public string schedule_for { get; set; }
        [StringLength(200)]
        public string timezone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string password { get; set; }
        [StringLength(200)]
        public string agenda { get; set; }

        //recurrence
        public int r_type { get; set; }
        public int repeat_interval { get; set; }
        [StringLength(255)]
        public string weekly_days { get; set; }
        public int monthly_day { get; set; }
        public int monthly_week { get; set; }
        public int monthly_week_day { get; set; }
        public int end_times { get; set; }
        public DateTime end_date_time { get; set; }

        //Settings
        public bool host_video { get; set; }
        public bool participant_video { get; set; }
        public bool cn_meeting { get; set; }
        public bool in_meeting { get; set; }
        public bool join_before_host { get; set; }
        public bool mute_upon_entry { get; set; }
        public bool watermark { get; set; }
        public bool use_pmi { get; set; }
        public int approval_type { get; set; }
        public int registration_type { get; set; }
        [StringLength(100)]
        public string audio { get; set; }
        public string auto_recording { get; set; }
        public bool enforce_login { get; set; }

        public string enforce_login_domains { get; set; }
        public string alternative_hosts { get; set; }
        public string global_dial_in_countries { get; set; }


        public bool registrants_email_notification { get; set; }
    }
}
