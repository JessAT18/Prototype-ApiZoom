using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities
{
    public class MeetingEntity
    {
        [Key]
        public int MeetingId { get; set; }
        [Required]
        [StringLength(200)]
        public string Topic { get; set; }
        [Required]
        public int type { get; set; }
        [Required]
        public DateTime StartTime { get; set; }
        [Required]
        public int Duration { get; set; }
        [StringLength(200)]
        public string ScheduleFor { get; set; }
        [StringLength(200)]
        public string Timezone { get; set; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(255, ErrorMessage = "Must be between 5 and 255 characters", MinimumLength = 5)]
        public string Password { get; set; }
        [StringLength(200)]
        public string Agenda { get; set; }

        //Recurrence
        public int RType { get; set; }
        public int RepeatInterval { get; set; }
        [StringLength(255)]
        public string WeeklyDays { get; set; }
        public int MonthlyDay { get; set; }
        public int MonthlyWeek { get; set; }
        public int MonthlyWeekDay { get; set; }
        public int EndTimes { get; set; }
        public DateTime EndDateTime { get; set; }

        //Settings
        public bool HostVideo { get; set; }
        public bool ParticipantVideo { get; set; }
        public bool cnMeeting { get; set; }
        public bool inMeeting { get; set; }
        public bool JoinBeforeHost { get; set; }
        public bool muteUponEntry { get; set; }
        public bool WaterMark { get; set; }
        public bool UsePMI { get; set; }
        public int ApprovalType { get; set; }
        public int RegistrationType { get; set; }
        [StringLength(100)]
        public string Audio { get; set; }
        public string AutoRecording { get; set; }
        public bool EnforceLogin { get; set; }

        public string EnforceLoginDomains { get; set; }
        public string AlternativeHosts { get; set; }
        public string GlobalDialInCountries { get; set; }


        public bool RegEmailNotification { get; set; }
    }
}


