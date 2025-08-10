using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Reviews;

namespace ProfessionalManagementService.Connections.Database;

public partial class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ClientProfessionalReview>()
            .HasOne(r => r.Client)
            .WithMany(c => c.ClientProfessionalReviews)
            .HasForeignKey(r => r.ClientId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ClientProfessionalReview>()
            .HasOne(r => r.Professional)
            .WithMany()
            .HasForeignKey(r => r.ProfessionalId)
            .OnDelete(DeleteBehavior.SetNull);
        
        modelBuilder.Entity<ClientProfessionalReview>()
            .HasOne(r => r.ClientServicePlan)
            .WithMany()
            .HasForeignKey(r => r.ClientServicePlanId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<ClientServicePlan>()
            .HasOne(csp => csp.ServicePlan)
            .WithMany()
            .HasForeignKey(csp => csp.ServicePlanId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}