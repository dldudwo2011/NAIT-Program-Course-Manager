using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ClassLibrary1.Entities
{
    public partial class Course
    {
        public Course()
        {
            ProgramCourses = new HashSet<ProgramCourse>();
        }

        [Key]
        [Column("CourseID")]
        [StringLength(7)]
        public string CourseId { get; set; }
        [Required]
        [StringLength(75)]
        public string CourseName { get; set; }
        [Column(TypeName = "decimal(3, 1)")]
        public decimal Credits { get; set; }
        public int? TotalHours { get; set; }
        public int? ClassroomType { get; set; }
        public int Term { get; set; }
        [Column(TypeName = "money")]
        public decimal Tuition { get; set; }
        [StringLength(1028)]
        public string Description { get; set; }

        [InverseProperty(nameof(ProgramCourse.Course))]
        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }
    }
}
