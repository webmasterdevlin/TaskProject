﻿using System;

namespace TaskApi.Entities
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
