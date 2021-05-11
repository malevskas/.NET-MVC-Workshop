using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Course
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Title { get; set; }
        public int Credits { get; set; }
        public int Semester { get; set; }
        [MaxLength(100)]
        public string Program { get; set; }
        [MaxLength(25)]
        public string EducationLevel { get; set; }
        [ForeignKey("FirstTeacher")]
        public int? FirstTeacherId { get; set; }
        public Teacher FirstTeacher { get; set; }
        [ForeignKey("SecondTeacher")]
        public int? SecondTeacherId { get; set; }
        public Teacher SecondTeacher { get; set; }
        public ICollection<Enrollment> Students { get; set; }
    }
}
