using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class Project
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Members { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}