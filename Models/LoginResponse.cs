﻿namespace AutoLibraryAPI.Models;

public class LoginResponse
{
    public string Token { get; set; }=string.Empty;
    public DateTime Expiration { get; set; }
    public UserInfo UserInfo { get; set; } = null!;
}