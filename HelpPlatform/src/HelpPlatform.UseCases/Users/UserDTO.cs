﻿namespace HelpPlatform.UseCases.Users;

public class UserDto(int id, string name, string email)
{
    public int Id { get; set; } = id;

    public string Name { get; set; } = name;

    public string Email { get; set; } = email;
}
