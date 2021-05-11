using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Enrollment
    {
        public long Id { get; set; }
        public int CourseId { get; set; }
        public Course Course { get; set; }
        public long StudentId { get; set; }
        public Student Student { get; set; }
        [MaxLength(10)]
        public string Semester { get; set; }
        public int Year { get; set; }
        public int Grade { get; set; }
        [MaxLength(255)]
        public string SeminalUrl { get; set; }
        [MaxLength(255)]
        public string ProjectUrl { get; set; }
        public int ExamPoints { get; set; }
        public int SeminalPoints { get; set; }
        public int ProjectPoints { get; set; }
        public int AdditionalPoints { get; set; }
        [DataType(DataType.Date)]
        public DateTime FinishDate { get; set; }
    }
}
