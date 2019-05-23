using System;

namespace Infrastructure.Entities
{
    public class ProjectMemberRole
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}