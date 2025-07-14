using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using ProfessionalManagementService.Clients;
using ProfessionalManagementService.Professionals;
using ProfessionalManagementService.Reviews;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Connections.Database;

public partial class DatabaseContext
{
    public DbSet<Professional> Professionals { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientServicePlan> ClientServicePlans { get; set; }
    public DbSet<ClientProfessionalReview> ClientProfessionalReviews { get; set; }
    public DbSet<ServicePlan> ServicePlans { get; set; }
}