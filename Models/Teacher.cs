using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Final.Models
{
    public class Teacher
    {
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        public string Degree { get; set; }
        [MaxLength(25)]
        public string AcademicRank { get; set; }
        [MaxLength(10)]
        public string OfficeNumber { get; set; }
        [DataType(DataType.Date)]
        public DateTime HireDate { get; set; }
        [InverseProperty("FirstTeacher")]
        public ICollection<Course> Courses1 { get; set; }
        [InverseProperty("SecondTeacher")]
        public ICollection<Course> Courses2 { get; set; }
        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
        public List<Course> AllCourses
        {
            get
            {
                if (Courses1 == null)
                {
                    if (Courses2 == null)
                    {
                        List<Course> l = new List<Course>();
                        return l;
                    }
                    List<Course> l3 = Courses2.ToList();
                    return l3;
                }
                if (Courses2 == null)
                {
                    List<Course> l = Courses1.ToList();
                    return l;
                }
                List<Course> l1 = Courses1.ToList();
                List<Course> l2 = Courses2.ToList();
                return l1.Concat(l2).ToList();
            }
        }
    }
}
