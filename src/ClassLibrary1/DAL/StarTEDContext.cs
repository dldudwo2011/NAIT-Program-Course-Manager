using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ClassLibrary1.Entities;

#nullable disable

namespace ClassLibrary1.DAL
{
    public partial class StarTEDContext : DbContext
    {
        public StarTEDContext()
        {
        }

        public StarTEDContext(DbContextOptions<StarTEDContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Program> Programs { get; set; }
        public virtual DbSet<ProgramCourse> ProgramCourses { get; set; }

    }
}
