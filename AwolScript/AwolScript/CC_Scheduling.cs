namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    [Table("CC_Scheduling")]
    public partial class CC_Scheduling
    {
        [StringLength(255)]
        public string Team { get; set; }

        [StringLength(255)]
        public string Interviewer { get; set; }

        public DateTime? Date { get; set; }

        [Column("Shift-Start")]
        public DateTime ShiftStart { get; set; }

        [Column("Shift-End")]
        public DateTime ShiftEnd { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public double? TotalHours { get; set; }

        public bool IN { get; set; }

        public string IntwrsComments { get; set; }

        public bool Approved { get; set; }

        public bool Declined { get; set; }

        public string Comments { get; set; }

        public bool Paid { get; set; }

        public DateTime? LoggedDate { get; set; }

        public DateTime? OvertimeStart { get; set; }

        public DateTime? OvertimeEnd { get; set; }

        public double? OTTotalHours { get; set; }

        public bool OTApproval { get; set; }

        public DateTime? AuthorisatedDate { get; set; }

        [Column("Autorisated By")]
        [StringLength(255)]
        public string Autorisated_By { get; set; }

        [StringLength(255)]
        public string SpareT { get; set; }

        [StringLength(255)]
        public string SpareM { get; set; }

        [StringLength(255)]
        public string SpareD { get; set; }

        public int? MaxAvail { get; set; }

        [StringLength(255)]
        public string Project1 { get; set; }

        public DateTime? Proj1Start { get; set; }

        public int? Proj1Hours { get; set; }

        [StringLength(255)]
        public string Project2 { get; set; }

        public DateTime? Proj2Start { get; set; }

        public int? Proj2Hours { get; set; }

        [StringLength(255)]
        public string Project3 { get; set; }

        public DateTime? Proj3Start { get; set; }

        public int? Proj3Hours { get; set; }

        public int ID { get; set; }
    }
}
