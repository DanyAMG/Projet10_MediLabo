using Microsoft.EntityFrameworkCore;
using Back_Patient.Model;

namespace Back_Patient.Data
{
    public class LocalDbContext :DbContext
    {
        public LocalDbContext(DbContextOptions<LocalDbContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
