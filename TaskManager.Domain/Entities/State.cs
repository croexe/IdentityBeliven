﻿namespace TaskManager.Domain.Entities;

public sealed class State : Entity
{
    public string StateName { get; set; }

    public Task Task { get; set; }
}