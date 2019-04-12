using System;

namespace Infrastructure.Entities
{
    public class ProjectTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Color { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string Members { get; set; }
        public int State { get; set; }

        public int CretedUserId { get; set; }

        public int ProjectId { get; set; }
        public Project Projects { get; set; }
    }
}