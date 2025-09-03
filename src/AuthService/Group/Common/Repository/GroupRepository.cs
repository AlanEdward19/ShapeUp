using AuthService.Common.User;
using AuthService.Connections.Database;
using AuthService.Group.Common.Enums;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Exceptions;

namespace AuthService.Group.Common.Repository;

public class GroupRepository(AuthDbContext dbContext) : IGroupRepository
{
    public async Task AddUserToGroupAsync(Guid groupId, string userId, EGroupRole role,
        CancellationToken cancellationToken)
    {
        Group? group = await dbContext.Groups.Include(g => g.Users)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken: cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        User? user = await dbContext.Users.Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.ObjectId == userId, cancellationToken: cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);
            
            try
            {
                group.AddUser(user, role);

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

    public async Task ChangeUserRoleInGroupAsync(Guid groupId, string userId, EGroupRole role,
        CancellationToken cancellationToken)
    {
        Group? group = await dbContext.Groups
            .Include(g => g.Users)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken: cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        User? user = await dbContext.Users.Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.ObjectId == userId, cancellationToken: cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                group.ChangeUserRole(user, role);

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

    public async Task RemoveUserFromGroupAsync(Guid groupId, string userId, CancellationToken cancellationToken)
    {
        Group? group = await dbContext.Groups.Include(g => g.Users)
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken: cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        User? user = await dbContext.Users.Include(u => u.UserGroups)
            .FirstOrDefaultAsync(u => u.ObjectId == userId, cancellationToken: cancellationToken);

        if (user is null)
            throw new NotFoundException($"User with id '{userId}' not found.");

        if (group.Users.All(u => u.UserId != userId))
            throw new NotFoundException($"User with id '{userId}' not found in group with id '{groupId}'.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                group.RemoveUser(user);

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

    public async Task AddAsync(Group group, string userId, CancellationToken cancellationToken)
    {
        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                await dbContext.Groups.AddAsync(group, cancellationToken);

                User user = await dbContext.Users
                    .FirstAsync(u => u.ObjectId == userId, cancellationToken: cancellationToken);

                group.AddUser(user, EGroupRole.Admin);

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

    public async Task DeleteAsync(Guid groupId, CancellationToken cancellationToken)
    {
        Group? group = await dbContext.Groups
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken: cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        var strategy = dbContext.Database.CreateExecutionStrategy();

        await strategy.ExecuteAsync(async () =>
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            try
            {
                dbContext.Groups.Remove(group);

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

    public async Task<ICollection<User>> GetUsersFromGroupAsync(Guid groupId, CancellationToken cancellationToken)
    {
        Group? group = await dbContext.Groups
            .Include(g => g.Users)
            .ThenInclude(userGroup => userGroup.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(g => g.Id == groupId, cancellationToken: cancellationToken);

        if (group is null)
            throw new NotFoundException($"Group with id '{groupId}' not found.");

        return group.Users.Select(x => x.User).ToList();
    }
}