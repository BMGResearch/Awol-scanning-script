namespace AwolScript
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CC_AgencyDetails
    {
        [StringLength(255)]
        public string AgencyName { get; set; }

        [StringLength(255)]
        public string AgencyAM { get; set; }

        [StringLength(255)]
        public string AgencyAMPhone { get; set; }

        [StringLength(255)]
        public string AgencyAMEmail { get; set; }

        public int ID { get; set; }

        [StringLength(255)]
        public string PayID { get; set; }
    }
}
