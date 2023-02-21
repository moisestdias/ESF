using EpmDashboard.Models;
using EpmDashboard.Models.EPM;
using Microsoft.EntityFrameworkCore;

namespace EpmDashboard.Data
{
    public partial class EPMContext : DbContext
    {
        public EPMContext()
        {
        }

        public EPMContext(DbContextOptions<EPMContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Problem>()
                .HasOne(i => i.ProblemMaker)
                .WithMany(i => i.Problems);

            builder.Entity<Problem>()
                .HasOne(i => i.ProblemSolver)
                .WithMany(i => i.Problems);

            builder.Entity<ProblemSolver>()
                .HasOne(i => i.EngineeringArea)
                .WithMany(i => i.ProblemSolvers)
                .HasForeignKey(i => i.EngineeringAreasid)
                .HasPrincipalKey(i => i.id);

            builder.Entity<Problem>()
                .Property(p => p.funding)
                .HasPrecision(18, 2);
            builder.Entity<ApplicationUser>()
                .HasOne(i => i.ProblemMaker)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey<ProblemMaker>();
            builder.Entity<ApplicationUser>()
                .HasOne(i => i.ProblemSolver)
                .WithOne(i => i.ApplicationUser)
                .HasForeignKey<ProblemSolver>();
        }

        public DbSet<Problem> Problems { get; set; }

        public DbSet<ProblemMaker> ProblemMakers { get; set; }

        public DbSet<ProblemSolver> ProblemSolvers { get; set; }

        public DbSet<EngineeringArea> EngineeringAreas { get; set; }
    }
}