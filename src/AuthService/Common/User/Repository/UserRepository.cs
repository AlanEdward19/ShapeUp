using AuthService.Authentication.Common.Enums;
using AuthService.Connections.Database;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Common.User.Repository;

public class UserRepository(AuthDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByObjectIdAsync(Guid objectId, CancellationToken cancellationToken) => 
        await dbContext.Users.Include(u => u.UserGroups)
        .ThenInclude(ug => ug.Group)
        .ThenInclude(g => g.Permissions)
        .FirstOrDefaultAsync(u => u.ObjectId == objectId, cancellationToken: cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        await dbContext.Users.AddAsync(user, cancellationToken);
        
        Group.Group userGroup = new();
        userGroup.AddUser(user, EGroupRole.Owner);
        await dbContext.Groups.AddAsync(userGroup, cancellationToken);
        
        await dbContext.SaveChangesAsync(cancellationToken);
        
        await dbContext.Database.CommitTransactionAsync(cancellationToken);
    }
}