namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CC_InterviewerList
    {
        [StringLength(255)]
        public string IntNameID { get; set; }

        [StringLength(255)]
        public string INTVWR_Name { get; set; }

        public double INTVWR_ID { get; set; }

        [StringLength(255)]
        public string TEAM { get; set; }

        public bool Inactive { get; set; }

        public bool Live { get; set; }

        [StringLength(255)]
        public string Password { get; set; }

        [StringLength(255)]
        public string PasswordT { get; set; }

        [StringLength(255)]
        public string Spare1 { get; set; }

        [Column(TypeName = "money")]
        public decimal? PayRateH { get; set; }

        [Column(TypeName = "money")]
        public decimal? PayRateM { get; set; }

        [Column(TypeName = "money")]
        public decimal? AgencyRateH { get; set; }

        [Column(TypeName = "money")]
        public decimal? AgencyRateM { get; set; }

        public int? SpareN1 { get; set; }

        [StringLength(255)]
        public string SpareT1 { get; set; }

        public string SpareM1 { get; set; }

        [StringLength(255)]
        public string EmployedBy { get; set; }

        public double? CoreHours { get; set; }

        [StringLength(255)]
        public string CoreHoursDetail { get; set; }

        public bool Agency { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? ContractDate { get; set; }

        [StringLength(255)]
        public string Telephone { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        [StringLength(255)]
        public string Status { get; set; }

        [StringLength(255)]
        public string EmergencyContactName { get; set; }

        [StringLength(255)]
        public string EmergencyTelNumber { get; set; }

        public DateTime? DOB { get; set; }

        [StringLength(255)]
        public string Gender { get; set; }

        [StringLength(255)]
        public string Languages { get; set; }

        [StringLength(255)]
        public string Address1 { get; set; }

        [StringLength(255)]
        public string Address2 { get; set; }

        [StringLength(255)]
        public string Postcode { get; set; }

        public string Notes { get; set; }

        [Column("General Comments")]
        public string General_Comments { get; set; }

        [Column("Finance Complete")]
        public bool Finance_Complete { get; set; }

        [Column("HR Complete")]
        public bool HR_Complete { get; set; }

        public bool Contract { get; set; }

        [Column("CH - Monday")]
        public int? CH___Monday { get; set; }

        [Column("CH - Tuesday")]
        public int? CH___Tuesday { get; set; }

        [Column("CH- Wenesday")]
        public int? CH__Wenesday { get; set; }

        [Column("CH - Thursday")]
        public int? CH___Thursday { get; set; }

        [Column("CH - Friday")]
        public int? CH___Friday { get; set; }

        [Column("CH - Saturday")]
        public int? CH___Saturday { get; set; }

        [Column("Leaver Comment")]
        public string Leaver_Comment { get; set; }

        [Column("Leaver Date")]
        public DateTime? Leaver_Date { get; set; }

        [Column("Leaver Re-Employ")]
        public bool Leaver_Re_Employ { get; set; }

        [Column("Leaver Reason")]
        [StringLength(255)]
        public string Leaver_Reason { get; set; }

        [Column("Leaver - HR Complete")]
        public bool Leaver___HR_Complete { get; set; }

        [Column("Leaver - Finance")]
        public bool Leaver___Finance { get; set; }

        [StringLength(255)]
        public string BadgeID { get; set; }

        [Column("CC>>HR_Comments")]
        public string CC__HR_Comments { get; set; }

        [Column("HR>>CC_Comments")]
        public string HR__CC_Comments { get; set; }

        [Column("CC>>FN_Comments")]
        public string CC__FN_Comments { get; set; }

        [Column("FN>>CC_Comments")]
        public string FN__CC_Comments { get; set; }

        [StringLength(255)]
        public string Department { get; set; }

        [StringLength(255)]
        public string Shift { get; set; }

        [StringLength(255)]
        public string Location { get; set; }

        public int? Age { get; set; }

        public int ID { get; set; }
    }
}
