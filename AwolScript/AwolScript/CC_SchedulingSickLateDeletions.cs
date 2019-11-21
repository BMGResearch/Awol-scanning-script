namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CC_SchedulingSickLateDeletions
    {
        [StringLength(255)]
        public string Interviewer { get; set; }

        public DateTime? Date { get; set; }

        [Column("Shift-Start")]
        public DateTime? Shift_Start { get; set; }

        [Column("Shift-End")]
        public DateTime? Shift_End { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        public double? TotalHours { get; set; }

        public string Comments { get; set; }

        [StringLength(255)]
        public string Manager { get; set; }

        public DateTime? EntryDate { get; set; }

        [Key]
        [Column(Order = 0)]
        public bool IntSign { get; set; }

        [Key]
        [Column(Order = 1)]
        public bool ManSign { get; set; }

        public DateTime? IntSignDate { get; set; }

        public string IntComments { get; set; }

        public DateTime? ManSignDate { get; set; }

        public string PreventativeMeasure { get; set; }

        [Column("Manager Signed")]
        [StringLength(255)]
        public string Manager_Signed { get; set; }

        [StringLength(255)]
        public string ManagerUpdate { get; set; }

        public int? SpareN { get; set; }

        [StringLength(255)]
        public string SpareT { get; set; }

        public string SpareM { get; set; }

        public DateTime? SpareD { get; set; }

        [Key]
        [Column(Order = 2)]
        public bool Approve { get; set; }

        [Key]
        [Column(Order = 3)]
        public bool Deny { get; set; }

        [Column("Last Updated")]
        public DateTime? Last_Updated { get; set; }

        [StringLength(255)]
        public string LastUpdatePerson { get; set; }

        [Key]
        [Column(Order = 4)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ID { get; set; }

        public bool? Deletion { get; set; }
    }
}
