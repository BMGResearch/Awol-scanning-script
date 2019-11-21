namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CC_SchedulingSickLate
    {
        [StringLength(255)]
        public string Interviewer { get; set; }

        public DateTime? Date { get; set; }

        [Column("Shift-Start")]
        public DateTime? ShiftStart { get; set; }

        [Column("Shift-End")]
        public DateTime? ShiftEnd { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public double? TotalHours { get; set; }

        public string Comments { get; set; }

        [StringLength(255)]
        public string Manager { get; set; }

        public DateTime? EntryDate { get; set; }

        public bool IntSign { get; set; }

        public bool ManSign { get; set; }

        public DateTime? IntSignDate { get; set; }

        public string IntComments { get; set; }

        public DateTime? ManSignDate { get; set; }

        public string PreventativeMeasure { get; set; }



        [StringLength(255)]
        public string ManagerUpdate { get; set; }

        public int? SpareN { get; set; }

        [StringLength(255)]
        public string SpareT { get; set; }

        public string SpareM { get; set; }

        public DateTime? SpareD { get; set; }

        public bool Approve { get; set; }

        public bool Deny { get; set; }


        [StringLength(255)]
        public string LastUpdatePerson { get; set; }

        public int ID { get; set; }

        public bool? Deletion { get; set; }
    }
}
