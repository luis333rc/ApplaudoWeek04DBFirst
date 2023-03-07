using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class OfficeAssigment
{
    public int InstructorId { get; set; }

    public string Location { get; set; } = null!;

    public virtual Instructor Instructor { get; set; } = null!;
}
