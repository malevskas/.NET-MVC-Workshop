using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Final.Data;
using Final.Models;
using Final.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Final.Controllers
{
    public class CoursesController : Controller
    {
        private readonly FinalContext _context;

        public CoursesController(FinalContext context)
        {
            _context = context;
        }

        // GET: Courses
        public async Task<IActionResult> Index(int? courseSemester, string courseProgram, string searchString)
        {
            IQueryable<Course> courses = _context.Course.AsQueryable();
            courses = _context.Course.Include(c => c.FirstTeacher).Include(c => c.SecondTeacher).Include(c => c.Students).ThenInclude(c => c.Student);
            IQueryable<int> semesterQuery = _context.Course.OrderBy(m => m.Semester).Select(m => m.Semester).Distinct();
            IQueryable<string> programQuery = _context.Course.OrderBy(m => m.Program).Select(m => m.Program).Distinct();
            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses.Where(s => s.Title.Contains(searchString));
            }
            if (courseSemester != null)
            {
                courses = courses.Where(x => x.Semester == courseSemester);
            }
            if (!string.IsNullOrEmpty(courseProgram))
            {
                courses = courses.Where(x => x.Program == courseProgram);
            }

            var searchCourseVM = new SearchCourse
            {
                Semesters = new SelectList(await semesterQuery.ToListAsync()),
                Programs = new SelectList(await programQuery.ToListAsync()),
                Courses = await courses.ToListAsync()
            };
            return View(searchCourseVM);
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName");
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Credits,Semester,Program,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(course);
        }

        // GET: Courses/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Credits,Semester,Program,EducationLevel,FirstTeacherId,SecondTeacherId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(course);
        }

        // GET: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Students(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            IQueryable<Enrollment> enrollments = _context.Enrollment.AsQueryable();
            enrollments = _context.Enrollment.Include(t => t.Student);
            enrollments = enrollments.Where(x => x.CourseId == id);
            IList<Enrollment> Enroll = await enrollments.ToListAsync();

            var course = await _context.Course
                .FirstOrDefaultAsync(t => t.Id == id);
            ViewData["course"] = course.Title;

            return View(Enroll);
        }

        // GET
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Enroll(int courseid)
        {
            var course = _context.Course.Where(s => s.Id == courseid).Include(m => m.Students).Include(c => c.FirstTeacher)
                .Include(c => c.SecondTeacher).First();
            if (course == null)
            {
                return NotFound();
            }

            IEnumerable<Student> students = _context.Student.AsEnumerable();
            students = students.OrderBy(s => s.FullName);
            AddStudentVM viewmodel = new AddStudentVM
            {
                Course = course,
                SelectedStudents = course.Students.Select(c => c.StudentId),
                StudentList = new MultiSelectList(students, "Id", "FullName"),
                Year = 0
            };
            ViewData["FirstTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.FirstTeacherId);
            ViewData["SecondTeacherId"] = new SelectList(_context.Set<Teacher>(), "Id", "FullName", course.SecondTeacherId);
            return View(viewmodel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int id, AddStudentVM viewmodel)
        {
            if (id != viewmodel.Course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viewmodel.Course);
                    await _context.SaveChangesAsync();

                    IEnumerable<long> listStudents = viewmodel.SelectedStudents;
                    IQueryable<Enrollment> toBeRemoved = _context.Enrollment.Where(s => !listStudents.Contains(s.StudentId) && s.CourseId == id);
                    _context.Enrollment.RemoveRange(toBeRemoved);
                    IEnumerable<long> existStudents = _context.Enrollment.Where(s => listStudents.Contains(s.StudentId) && s.CourseId == id).Select(s => s.StudentId);
                    IEnumerable<long> newStudents = listStudents.Where(s => !existStudents.Contains(s));
                    foreach (long studentId in newStudents)
                        _context.Enrollment.Add(new Enrollment
                        {
                            StudentId = studentId,
                            CourseId = id,
                            Semester = viewmodel.Semester,
                            Year = viewmodel.Year,
                            Grade = 0,
                            SeminalUrl = "",
                            ProjectUrl = "",
                            ExamPoints = 0,
                            SeminalPoints = 0,
                            ProjectPoints = 0,
                            AdditionalPoints = 0,
                            FinishDate = DateTime.Parse("2000-1-01")
                        });


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(viewmodel.Course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", viewmodel.Course.Id);
            //ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", viewmodel.Enrollment.StudentId);
            return View(viewmodel);
        }

        //GET
        public async Task<IActionResult> DetailsEnrollment(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var enrollment = await _context.Enrollment
                .Include(e => e.Course)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrollment == null)
            {
                return NotFound();
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsEnrollment(long id, [Bind("Id,CourseId,StudentId,Semester,Year,Grade,SeminalUrl,ProjectUrl,ExamPoints,SeminalPoints,ProjectPoints,AdditionalPoints,FinishDate")] Enrollment enrollment)
        {
            if (id != enrollment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrollment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrollmentExists(enrollment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Students), new { id = enrollment.CourseId });
            }
            ViewData["CourseId"] = new SelectList(_context.Course, "Id", "Title", enrollment.CourseId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "FullName", enrollment.StudentId);
            return View(enrollment);
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        private bool EnrollmentExists(long id)
        {
            return _context.Enrollment.Any(e => e.Id == id);
        }
    }
}
