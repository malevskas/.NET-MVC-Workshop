using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Student
    {
        public long Id { get; set; }
        [MaxLength(10)]
        public string StudentId { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime EnrollmentDate { get; set; }
        public int AcquiredCredits { get; set; }
        public int CurrentSemester { get; set; }
        [MaxLength(25)]
        public string EducationLevel { get; set; }
        public ICollection<Enrollment> Courses { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
