﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Models
{
    public class TaskEntity
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public DateTime DeadLine { get; set; }
    }
}
