using System;

namespace Infrastructure.Entities
{
    public class TaskMark
    {
        public Guid Id { get; set; }

        public bool Active { get; set; }

        public Guid ProjectMarkId { get; set; }
        public ProjectMark ProjectMarks { get; set; }

        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }
    }
}