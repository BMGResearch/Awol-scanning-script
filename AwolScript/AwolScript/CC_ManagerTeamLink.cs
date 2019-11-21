namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CC_ManagerTeamLink
    {
        [StringLength(255)]
        public string Managers { get; set; }

        [Column("Team Manager")]
        [StringLength(255)]
        public string Team_Manager { get; set; }

        [StringLength(255)]
        public string Shift { get; set; }

        public int? ManagerID { get; set; }

        [StringLength(255)]
        public string Email { get; set; }

        public int? TeamNo { get; set; }

        [StringLength(255)]
        public string TeamName { get; set; }

        [StringLength(255)]
        public string TeamManager { get; set; }

        [StringLength(255)]
        public string Coach1 { get; set; }

        [StringLength(255)]
        public string Coach2 { get; set; }

        public int ID { get; set; }
    }
}
