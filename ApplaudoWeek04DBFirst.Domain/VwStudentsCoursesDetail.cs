using System;
using System.Collections.Generic;

namespace ApplaudoWeek04DBFirst.Application;

public partial class VwStudentsCoursesDetail
{
    public string StudentName { get; set; } = null!;

    public string CourseTitle { get; set; } = null!;

    public byte Credits { get; set; }

    public string InstructorName { get; set; } = null!;

    public string? Grade { get; set; }
}
