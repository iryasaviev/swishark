using System;

namespace Infrastructure.Entities
{
    public class TaskMember
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public Guid TaskId { get; set; }
        public ProjectTask Task { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}