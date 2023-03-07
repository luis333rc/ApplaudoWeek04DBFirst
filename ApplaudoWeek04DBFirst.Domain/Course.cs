using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class Course
{
    public int CourseId { get; set; }

    public string Title { get; set; } = null!;

    public byte Credits { get; set; }

    public int DepartmentId { get; set; }

    public virtual Department Department { get; set; } = null!;

    public virtual ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();

    public virtual ICollection<Instructor> Instructors { get; } = new List<Instructor>();
}
