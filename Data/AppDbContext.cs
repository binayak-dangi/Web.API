using Microsoft.EntityFrameworkCore;
using Web.API.Models.DTOS.Setup;
using Web.API.Models.Entities.Setup;

namespace Web.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> Options) : base(Options)
        {

        }

        public DbSet<HREmployee> HREmployee { get; set; }
        public DbSet<HRCompany> HRCompany { get; set; }
        public DbSet<HRBranch> HRBranch { get; set; }
        public DbSet<HRRole> HRRole { get; set; }
        public DbSet<HRRolePermissionLink> HRRolePermissionLink { get; set; }
        public DbSet<HRRolePermissionLinkMirror> HRRolePermissionLinkMirror { get; set; }
        public DbSet<HREmployeePermissionLink> HREmployeePermissionLink { get; set; }
        public DbSet<HREmployeePermissionLinkMirror> HREmployeePermissionLinkMirror { get; set; }
        public DbSet<HRPermission> HRPermission { get; set; }
        public DbSet<HRCorporateTitle> HRCorporateTitle { get; set; }
        public DbSet<HRFunctionalTitle> HRFunctionalTitle { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        public DbSet<ActivityLog> ActivityLog { get; set; }
        public DbSet<AdmMainHeading> Adm_MainHeading { get; set; }
        public DbSet<AdmHeading> Adm_Heading { get; set; }
        public DbSet<AdmElement> Adm_Element { get; set; }
        public DbSet<HREmailTemplate> HREmailTemplate { get; set; }
        public DbSet<HRSmsTemplate> HRSmsTemplate { get; set; }
        public DbSet<EventLog> EventLog { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<HRBranch>()
            //    .HasIndex(x => x.BranchName)
            //    .IsUnique();

            //modelBuilder.Entity<HREmployee>()
            //    .HasIndex(x => x.Username)
            //    .IsUnique();

            //modelBuilder.Entity<HRCompany>()
            //    .HasIndex(x => x.CompanyName)
            //    .IsUnique();
            //modelBuilder.Entity<HRBranch>()
            //    .HasIndex(x => x.BranchName)
            //    .IsUnique();
            //modelBuilder.Entity<HRRole>()
            //    .HasIndex(x => x.RoleName)
            //    .IsUnique();
            //modelBuilder.Entity<HRCorporateTitle>()
            //    .HasIndex(x => x.LevelGrade)
            //    .IsUnique();
            //modelBuilder.Entity<HRFunctionalTitle>()
            //    .HasIndex(x => x.PositionHead)
            //    .IsUnique();
         
        }
    }

}