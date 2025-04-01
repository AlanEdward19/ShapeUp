using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using Microsoft.EntityFrameworkCore;

namespace AuthService.Common.User.Repository;

public class UserRepository(AuthDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByObjectIdAsync(string objectId, CancellationToken cancellationToken) => 
        await dbContext.Users.Include(u => u.UserGroups)
        .ThenInclude(ug => ug.Group)
        .ThenInclude(g => g.GroupPermissions)
        .ThenInclude(x => x.Permission)
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