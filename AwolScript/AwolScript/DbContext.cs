namespace AwolScript
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DBContext : DbContext
    {
     
        public virtual DbSet<CC_AgencyDetails> CC_AgencyDetails { get; set; }

        public virtual DbSet<CC_InterviewerList> CC_InterviewerList { get; set; }

        public virtual DbSet<CC_ManagerTeamLink> CC_ManagerTeamLink { get; set; }

        public virtual DbSet<CC_Scheduling> CC_Scheduling { get; set; }

        public virtual DbSet<CC_SchedulingSickLate> CC_SchedulingSickLate { get; set; }

        public virtual DbSet<CC_SchedulingSickLateDeletions> CC_SchedulingSickLateDeletions { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
   

            modelBuilder.Entity<CC_InterviewerList>()
                .Property(e => e.PayRateH)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CC_InterviewerList>()
                .Property(e => e.PayRateM)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CC_InterviewerList>()
                .Property(e => e.AgencyRateH)
                .HasPrecision(19, 4);

            modelBuilder.Entity<CC_InterviewerList>()
                .Property(e => e.AgencyRateM)
                .HasPrecision(19, 4);
        }
    }
}
