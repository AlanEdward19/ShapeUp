﻿namespace SocialService.Friends.AddFriend;

public class AddFriendCommand
{
    public Guid FriendId { get; set; }
    public string? RequestMessage { get; set; }
}