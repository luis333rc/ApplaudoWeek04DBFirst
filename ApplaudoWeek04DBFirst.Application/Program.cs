using Microsoft.EntityFrameworkCore;

namespace ApplaudoWeek04DBFirst.Application
{
    public class Program
    {
        private static UniversityContext _context = new UniversityContext();

        static async Task Main(string[] args)
        {
            // All Students (names)
            _context.Students.ToList().ForEach(student => Console.WriteLine(student.FirstMidName));


            // Course title, Course Credits, Intructor names and grade
            //var results = await (from c in _context.Courses
            //                     from i in c.Instructors
            //                     join e in _context.Enrollments
            //                       on c.CourseId equals e.CourseId
            //                     where (e.StudentId == 2)
            //                     select new { StudentName = e.Student.LastName, e.StudentId, c.Title, c.Credits, i.LastName, e.Grade }
            //                    ).ToListAsync();

            //results.ForEach(r => Console.WriteLine($"{r.Title} - {r.Credits} - {r.LastName} - {r.Grade}"));


            // Student Name, Course title, Course Credits, Intructor names and grade
            //var results2 = await (from c in _context.Courses
            //                      from i in c.Instructors
            //                      join e in _context.Enrollments
            //                        on c.CourseId equals e.CourseId
            //                      select new { StudentName = $"{e.Student.FirstMidName} {e.Student.LastName}", e.StudentId, c.Title, c.Credits, InstructorName = $"{i.FistMidName} {i.LastName}", e.Grade }
            //                    ).ToListAsync();

            //results2.ForEach(r => Console.WriteLine($"{r.StudentName} - {r.Title} - {r.Credits} - {r.InstructorName} - {r.Grade}"));


            // Student Name, Course title, Course Credits, Intructor names and grade
            //var result4 = await _context.Enrollments
            //                                 .Include(x => x.Student)
            //                                 .Include(x => x.Course)
            //                                 .Select(x => new { StudentName = $"{x.Student.FirstMidName} {x.Student.LastName}", 
            //                                     x.Course.Title, 
            //                                     InstructorName = x.Course.Instructors.FirstOrDefault().LastName,
            //                                     x.Grade })
            //                                .ToListAsync();

            //result4.ForEach(i => Console.WriteLine($"{i.StudentName} -  {i.Title} -  {i.InstructorName} - {i.Grade}"));
        }
    }
}