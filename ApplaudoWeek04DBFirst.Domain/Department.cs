using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Budget { get; set; }

    public DateTime? StartDate { get; set; }

    public int? InstructorId { get; set; }

    public virtual ICollection<Course> Courses { get; } = new List<Course>();

    public virtual Instructor? Instructor { get; set; }
}
