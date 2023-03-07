using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ApplaudoWeek04DBFirst.Application;

public partial class UniversityContext : DbContext
{
    public UniversityContext()
    {
    }

    public UniversityContext(DbContextOptions<UniversityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Instructor> Instructors { get; set; }

    public virtual DbSet<OfficeAssigment> OfficeAssigments { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<VwStudentsCoursesDetail> VwStudentsCoursesDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-PH8N518;Integrated Security=True; Connect timeout=30;Encrypt=False;TrustServerCertificate=False; ApplicationIntent=ReadWrite; MultiSubnetFailover=False; Database=University;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CourseId).HasName("PK__Courses__C92D71874F928F95");

            entity.HasIndex(e => e.Credits, "IX_Courses_Credits");

            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Title)
                .HasMaxLength(25)
                .IsUnicode(false);

            entity.HasOne(d => d.Department).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DepartmentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Deparment");

            entity.HasMany(d => d.Instructors).WithMany(p => p.Courses)
                .UsingEntity<Dictionary<string, object>>(
                    "CoursesAssigment",
                    r => r.HasOne<Instructor>().WithMany()
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseAssigment_Intructor"),
                    l => l.HasOne<Course>().WithMany()
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CourseAssigment_Course"),
                    j =>
                    {
                        j.HasKey("CourseId", "InstructorId").HasName("PK_CourseIntructor");
                        j.ToTable("CoursesAssigments");
                        j.IndexerProperty<int>("CourseId").HasColumnName("CourseID");
                        j.IndexerProperty<int>("InstructorId").HasColumnName("InstructorID");
                    });
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DepartmentId).HasName("PK__Departme__B2079BCD3B92CD28");

            entity.Property(e => e.DepartmentId).HasColumnName("DepartmentID");
            entity.Property(e => e.Budget).HasColumnType("smallmoney");
            entity.Property(e => e.InstructorId).HasColumnName("InstructorID");
            entity.Property(e => e.Name)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.StartDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");

            entity.HasOne(d => d.Instructor).WithMany(p => p.Departments)
                .HasForeignKey(d => d.InstructorId)
                .HasConstraintName("FK_Department_Instructor");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.EnrollmentId).HasName("PK__Enrollme__7F6877FB7CA308D5");

            entity.HasIndex(e => e.Grade, "IX_Enrollments_Grade");

            entity.Property(e => e.EnrollmentId).HasColumnName("EnrollmentID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Grade)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.StudentId).HasColumnName("StudentID");

            entity.HasOne(d => d.Course).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Course");

            entity.HasOne(d => d.Student).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Enrollment_Student");
        });

        modelBuilder.Entity<Instructor>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Instruct__3214EC2796BA8B4D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.FistMidName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.HireDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<OfficeAssigment>(entity =>
        {
            entity.HasKey(e => e.InstructorId).HasName("PK__OfficeAs__9D010B7BE5BBABDF");

            entity.Property(e => e.InstructorId)
                .ValueGeneratedNever()
                .HasColumnName("InstructorID");
            entity.Property(e => e.Location)
                .HasMaxLength(30)
                .IsUnicode(false);

            entity.HasOne(d => d.Instructor).WithOne(p => p.OfficeAssigment)
                .HasForeignKey<OfficeAssigment>(d => d.InstructorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OfficeAssigment_Instructor");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC279CFC46CE");

            entity.HasIndex(e => e.LastName, "IX_Students_LastName");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EnrollmentDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("date");
            entity.Property(e => e.FirstMidName)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwStudentsCoursesDetail>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_Students_CoursesDetails");

            entity.Property(e => e.CourseTitle)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Grade)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.InstructorName)
                .HasMaxLength(61)
                .IsUnicode(false);
            entity.Property(e => e.StudentName)
                .HasMaxLength(61)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
