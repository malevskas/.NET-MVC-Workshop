using Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.ViewModels
{
    public class AddStudentVM
    {
        public Course Course { get; set; }
        public IEnumerable<long> SelectedStudents { get; set; }
        public IEnumerable<SelectListItem> StudentList { get; set; }
        public string Semester { get; set; }
        public int Year { get; set; }
    }
}
