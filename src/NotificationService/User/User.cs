using System.ComponentModel.DataAnnotations;
using NotificationService.Common.Enums;

namespace NotificationService.User;

public class User(string userId)
{
    [Key]
    public string Id { get; private set; } = userId;

    public List<UserDevice> Devices { get; private set; } = new();
    
    public void AddDevice(string deviceToken, EPlatform platform)
    {
        Devices.Add(new UserDevice(deviceToken, platform));
    }
}