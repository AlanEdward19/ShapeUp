﻿using Microsoft.EntityFrameworkCore;
using NotificationService.Connections.Database;
using SharedKernel.Exceptions;

namespace NotificationService.User.Repository;

/// <summary>
/// Repositório de usuários
/// </summary>
/// <param name="dbContext"></param>
/// <param name="logger"></param>
public class UserRepository(NotificationDbContext dbContext, ILogger<UserRepository> logger) : IUserRepository
{
    /// <summary>
    /// Método para adicionar um usuário ao banco de dados
    /// </summary>
    /// <param name="user"></param>
    /// <param name="cancellationToken"></param>
    private async Task<User> AddUserAsync(User user, CancellationToken cancellationToken)
    {
        await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        return user;
    }

    /// <summary>
    /// Método responsável por adicionar um dispositivo ao usuário ou atualizar o último acesso
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceToken"></param>
    /// <param name="cancellationToken"></param>
    public async Task UserLogInAsync(string userId, string deviceToken, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await dbContext.Users
                .Include(x => x.Devices)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            user ??= await AddUserAsync(new User(userId), cancellationToken);

            var device = user.Devices.FirstOrDefault(x => x.FcmToken == deviceToken);

            if (device != null)
                device.UpdateLastAccess();

            else
                user.AddDevice(deviceToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while logging in user {UserId}", userId);
            throw;
        }
    }

    /// <summary>
    /// Método responsável por remover o dispositivo do usuário
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="deviceToken"></param>
    /// <param name="cancellationToken"></param>
    /// <exception cref="NotFoundException"></exception>
    public async Task UserSignOutAsync(string userId, string deviceToken, CancellationToken cancellationToken)
    {
        try
        {
            User? user = await dbContext.Users
                .Include(x => x.Devices)
                .FirstOrDefaultAsync(x => x.Id == userId, cancellationToken);

            if (user == null)
                throw new NotFoundException("User not found");

            var device = user.Devices.FirstOrDefault(x => x.FcmToken == deviceToken);

            if (device == null)
                throw new NotFoundException("Device not found");

            user.RemoveDevice(deviceToken);

            await dbContext.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(e, "Error while logging in user {UserId}", userId);
            throw;
        }
    }

    /// <summary>
    /// Método responsável por retornar o token do último dispositivo acessado pelo usuário
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotFoundException"></exception>
    public async Task<string> GetUserLastAccessDeviceTokenAsync(string userId, CancellationToken cancellationToken)
    {
        User? user = await dbContext.Users
            .Include(x => x.Devices)
            .AsNoTracking()
            .FirstAsync(x => x.Id == userId, cancellationToken);

        if (user == null)
            throw new NotFoundException("User not found");

        return user.Devices
            .OrderBy(x => x.LastAccess)
            .First().FcmToken;
    }
}