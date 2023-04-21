﻿namespace AstronBase.Domain.Entity
{
    public class StatusRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Request> Requests { get; set; } = new();
    }
}