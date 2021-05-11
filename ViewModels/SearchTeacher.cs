using Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.ViewModels
{
    public class SearchTeacher
    {
        public IList<Teacher> Teachers { get; set; }
        public SelectList Degrees { get; set; }
        public SelectList Ranks { get; set; }
        public string TeacherDegree { get; set; }
        public string TeacherRank { get; set; }
        public string SearchString { get; set; }
    }
}
