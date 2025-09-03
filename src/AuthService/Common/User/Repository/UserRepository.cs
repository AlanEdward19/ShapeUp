using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;

namespace AuthService.Common.User.Repository;

public class UserRepository(AuthDbContext dbContext) : IUserRepository
{
    public async Task<User?> GetByObjectIdAsync(string objectId, CancellationToken cancellationToken) =>
        await dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ObjectId == objectId, cancellationToken: cancellationToken);

    public async Task AddAsync(User user, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await dbContext.Users.AddAsync(user, cancellationToken);

                Group.Group userGroup = new($"{user.ObjectId}'s Group", "Personal group for user");
                userGroup.AddUser(user, EGroupRole.Owner);
                await dbContext.Groups.AddAsync(userGroup, cancellationToken);

                await dbContext.SaveChangesAsync(cancellationToken);

                await dbContext.Database.CommitTransactionAsync(cancellationToken);
            }
            catch (Exception e)
            {
                await dbContext.Database.RollbackTransactionAsync(cancellationToken);
                throw;
            }
        });
    }

    public async Task<User> GetUserAsync(string userId,
        CancellationToken cancellationToken)
    {
        User? user = await dbContext.Users
            .Include(x => x.UserGroups)
            .ThenInclude(x => x.Group)
            .ThenInclude(x => x.GroupPermissions)
            .ThenInclude(x => x.Permission)
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ObjectId == userId, cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        return user;
    }
}