using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class Instructor
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FistMidName { get; set; } = null!;

    public DateTime HireDate { get; set; }

    public virtual ICollection<Department> Departments { get; } = new List<Department>();

    public virtual OfficeAssigment? OfficeAssigment { get; set; }

    public virtual ICollection<Course> Courses { get; } = new List<Course>();
}
