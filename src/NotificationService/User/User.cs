using System.ComponentModel.DataAnnotations;

namespace NotificationService.User;

public class User
{
    [Key]
    public string Id { get; private set; }

    public List<UserDevice> Devices { get; private set; } = new();

    public User() { }

    public User(string id)
    {
        Id = id;
    }

    public void AddDevice(string deviceToken)
    {
        Devices.Add(new UserDevice(deviceToken));
    }
    
    public void RemoveDevice(string deviceToken)
    {
        var device = Devices.FirstOrDefault(x => x.FcmToken == deviceToken);
        
        if (device != null)
            Devices.Remove(device);
    }
}