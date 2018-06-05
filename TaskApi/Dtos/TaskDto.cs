using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskApi.Dtos
{
    public class TaskDto
    {
        // public Guid Id { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
        public int DaysRemaining { get; set; }
    }
}
