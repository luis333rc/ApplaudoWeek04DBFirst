using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class Student
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstMidName { get; set; } = null!;

    public DateTime EnrollmentDate { get; set; }

    public virtual ICollection<Enrollment> Enrollments { get; } = new List<Enrollment>();
}
