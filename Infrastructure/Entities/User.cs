using System.Collections.Generic;

namespace Infrastructure.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Color { get; set; }

        public int? ProjectId { get; set; }

        public Project Project { get; set; }
        public ICollection<ProjectTask> ProjectTasks { get; set; }
    }
}