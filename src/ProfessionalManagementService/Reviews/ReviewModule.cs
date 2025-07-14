using ProfessionalManagementService.Common.Interfaces;
using ProfessionalManagementService.Reviews.CreateReview;
using ProfessionalManagementService.Reviews.DeleteReview;
using ProfessionalManagementService.Reviews.GetReviewsByProfessionalId;
using ProfessionalManagementService.Reviews.UpdateReview;

namespace ProfessionalManagementService.Reviews;

/// <summary>
/// Módulo de configuração para serviços relacionados a avaliações.
/// </summary>
public static class ReviewModule
{
    /// <summary>
    /// Método de extensão para configurar os serviços relacionados a avaliações.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection ConfigureReviewServices(this IServiceCollection services)
    {
        services
            .AddHandlers();

        return services;
    }

    private static IServiceCollection AddHandlers(this IServiceCollection services)
    {
        services.AddScoped<IHandler<ClientProfessionalReviewDto, CreateReviewCommand>, CreateReviewCommandHandler>();
        services.AddScoped<IHandler<ClientProfessionalReviewDto, UpdateReviewCommand>, UpdateReviewCommandHandler>();
        services
            .AddScoped<IHandler<List<ClientProfessionalReviewDto>, GetReviewsByProfessionalIdQuery>,
                GetReviewsByProfessionalIdQueryHandler>();
        services.AddScoped<IHandler<bool, DeleteReviewCommand>, DeleteReviewCommandHandler>();

        return services;
    }
}