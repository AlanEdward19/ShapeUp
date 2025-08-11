using ProfessionalManagementService.Common.Enums;

namespace ProfessionalManagementService.ServicePlans.ChangeServicePlanStatusFromClient;

public class ChangeServicePlanStatusFromClientValueObject(string reason, ESubscriptionStatus status)
{
    public string Reason { get; private set; } = reason;
    public ESubscriptionStatus Status { get; private set; } = status;
}