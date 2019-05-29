using System;

namespace Infrastructure.Entities
{
    public class ProjectMark
    {
        public Guid Id { get; set; }
        public int Num { get; set; }
        public string Text { get; set; }
        public string Color { get; set; }
        
        public int ProjectId { get; set; }
        public Project Projects { get; set; }
    }
}