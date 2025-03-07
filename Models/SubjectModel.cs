using System;

namespace SchoolAPI.Models
{
    public class SubjectModel
    {
        public int? SubjectId { get; set; }
        public string SubjectName { get; set; }
        public string SubjectCode { get; set; }
        public string Description { get; set; }
    }
}