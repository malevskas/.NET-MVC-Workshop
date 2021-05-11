using Final.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Final.ViewModels
{
    public class SearchStudent
    {
        public IList<Student> Students { get; set; }
        public SelectList Index { get; set; }
        public string StudentIndex { get; set; }
        public string SearchString { get; set; }
    }
}
