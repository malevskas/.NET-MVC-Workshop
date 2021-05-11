using Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.ViewModels
{
    public class SearchCourse
    {
        public IList<Course> Courses { get; set; }
        public SelectList Semesters { get; set; }
        public SelectList Programs { get; set; }
        public int CourseSemester { get; set; }
        public string CourseProgram { get; set; }
        public string SearchString { get; set; }    
    }
}
