using System;

namespace Infrastructure.Entities
{
    public class ProjectMember
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Photo { get; set; }

        public Guid ?RoleId { get; set; }
        public ProjectMemberRole Role { get; set; }

        public int DoesTaskId { get; set; }
        public ProjectTask Task { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }
    }
}