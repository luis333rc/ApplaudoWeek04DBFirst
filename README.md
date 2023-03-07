# Applaudo Week 04 - DBFirst

## Steps

### 1. Create a Blank Solution

### 2. Add Project (Console Application)

### 3. Add a Class Library for Context .Data

### 4. Add a Class Library for Entities .Domain

### 5. Add Nugget Pakages to the Application
  - Microsoft.EntityFrameworkCore.Tool 
  - Microsoft.EntityFrameworkCore.SqlServer

### 6. In the Package Manager Console execute:

Scaffold-DbContext -provider Microsoft.EntityFrameworkCore.SqlServer -connection "Data Source=DESKTOP-PH8N518;Integrated Security=True; Connect timeout=30;Encrypt=False;TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False; Database=University;" -ContextDir "C:\Users\L\source\repos\luis333rc\ApplaudoWeek04DatabaseFirst\ApplaudoWeek04DBFirst\ApplaudoWeek04DBFirst.Data" -OutputDir "C:\Users\L\source\repos\luis333rc\ApplaudoWeek04DatabaseFirst\ApplaudoWeek04DBFirst\ApplaudoWeek04DBFirst.Domain"

### 7. Add Nugget Pakages to .Data
  - Microsoft.EntityFrameworkCore.Tool 
  - Microsoft.EntityFrameworkCore.SqlServer

### 8. Add missed References, 
- .Data To .Domain
- .Application to .Data and .Domain

### 9. Generate a Diagram

![image](https://user-images.githubusercontent.com/125097644/223535005-d84e4635-771f-41d1-9e68-23f3f0c19ce1.png)


### 10. Add some queries

```cs
// All Students (names)
_context.Students.ToList().ForEach(student => Console.WriteLine(student.FirstMidName));
```

```cs
//Course title, Course Credits, Intructor names and grade
var results = await (from c in _context.Courses
                     from i in c.Instructors
                     join e in _context.Enrollments
                       on c.CourseId equals e.CourseId
                     where (e.StudentId == 2)
                     select new { StudentName = e.Student.LastName, e.StudentId, c.Title, c.Credits, i.LastName, e.Grade }
                    ).ToListAsync();

results.ForEach(r => Console.WriteLine($"{r.Title} - {r.Credits} - {r.LastName} - {r.Grade}"));
```

```cs
// Student Name, Course title, Course Credits, Intructor names and grade
var results2 = await (from c in _context.Courses
                      from i in c.Instructors
                      join e in _context.Enrollments
                        on c.CourseId equals e.CourseId
                      select new { StudentName = $"{e.Student.FirstMidName} {e.Student.LastName}",
                                  e.StudentId, 
                                  c.Title, 
                                  c.Credits, 
                                  InstructorName = $"{i.FistMidName {i.LastName}",
                                  e.Grade}
                     ).ToListAsync();

results2.ForEach(r => Console.WriteLine($"{r.StudentName} - {r.Title} - {r.Credits} - {r.InstructorName} - {r.Grade}"));
```

```cs
// Student Name, Course title, Course Credits, Intructor names and grade
var result4 = await _context.Enrollments
                               .Include(x => x.Student)
                               .Include(x => x.Course)
                               .Select(x => new { StudentName = $"{x.Student.FirstMidName} {x.Student.LastName}", 
                                   x.Course.Title, 
                                   InstructorName = x.Course.Instructors.FirstOrDefault().LastName,
                                   x.Grade })
                              .ToListAsync();

result4.ForEach(i => Console.WriteLine($"{i.StudentName} -  {i.Title} -  {i.InstructorName} - {i.Grade}"));
```

