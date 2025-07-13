namespace ProfessionalManagementService.Reviews.UpdateReview;

public class UpdateReviewCommand(int? rating, string? comment)
{
    public Guid Id { get; private set; }
    
    public int? Rating { get; private set; } = rating;
    
    public string? Comment { get; private set; } = comment;
    
    public void SetId(Guid id) => Id = id;
}