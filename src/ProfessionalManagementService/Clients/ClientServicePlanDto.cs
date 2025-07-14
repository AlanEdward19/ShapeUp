using ProfessionalManagementService.Common.Enums;
using ProfessionalManagementService.ServicePlans;

namespace ProfessionalManagementService.Clients;

public class ClientServicePlanDto(ClientServicePlan clientServicePlan)
{
    public Guid Id { get; private set; } = clientServicePlan.Id;
    
    public DateTime StartDate { get; private set; } = clientServicePlan.StartDate;
    
    public DateTime EndDate { get; private set; } = clientServicePlan.EndDate;
    
    public ESubscriptionStatus Status { get; private set; } = clientServicePlan.Status;
    
    public string? Feedback { get; private set; } = clientServicePlan.Feedback;
    
    public ServicePlanDto ServicePlan { get; private set; } = new(clientServicePlan.ServicePlan);
}