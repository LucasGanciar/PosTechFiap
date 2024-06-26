﻿namespace Domain.Request;

public class UpdateContactRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DDD { get; set; }
    public string Telephone { get; set; } = string.Empty;
    public string  Email { get; set; } = string.Empty;
}
