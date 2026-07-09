using Microsoft.EntityFrameworkCore;
using MyTestMvc.Models.Setup;
using Web.API.Entities;

namespace Web.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> Options) : base(Options)
        {

        }

        public DbSet<HREmployee> HREmployee { get; set; }
        public DbSet<HRBranch> HRBranch { get; set; }
        public DbSet<HRRole> HRRole { get; set; }
        public DbSet<HRRolePermissionLink> HRRolePermissionLink { get; set; }
        public DbSet<HREmployeePermissionLink> HREmployeePermissionLink { get; set; }
        public DbSet<HRPermission> HRPermission { get; set; }
        public DbSet<HRCorporateTitle> HRCorporateTitle { get; set; }
        public DbSet<HRFunctionalTitle> HRFunctionalTitle { get; set; }
    }
}
