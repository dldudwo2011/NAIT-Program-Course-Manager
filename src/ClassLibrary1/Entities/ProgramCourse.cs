using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ClassLibrary1.Entities
{
    [Index(nameof(CourseId), Name = "IX_CourseID")]
    [Index(nameof(ProgramId), Name = "IX_ProgramID")]
    [Index(nameof(ProgramId), nameof(CourseId), Name = "UN_dbo.ProgramCourses_ProgramIDCourseID", IsUnique = true)]
    public partial class ProgramCourse
    {
        [Key]
        [Required(ErrorMessage = "You must supply a ProgramCourseID (0 for adding)")]
        [Column("ProgramCourseID")]
        public int ProgramCourseId { get; set; }
        [Column("ProgramID")]
        [Required(ErrorMessage = "You must supply a ProgramID")]
        public int ProgramId { get; set; }
        [Required(ErrorMessage = "You must supply a CourseID - very important")]
        [Column("CourseID")]
        [StringLength(7, ErrorMessage = "Maximum of 7 characters allowed")]
        public string CourseId { get; set; }
        public bool Required { get; set; }
        [StringLength(50, ErrorMessage = "Maximum of 50 characters allowed")]
        public string Comments { get; set; }

        [Required(ErrorMessage = "You must supply a SectionLimit")]
        public int SectionLimit { get; set; }

        public bool Active { get; set; }

        [ForeignKey(nameof(CourseId))]
        [InverseProperty("ProgramCourses")]
        public virtual Course Course { get; set; }
        [ForeignKey(nameof(ProgramId))]
        [InverseProperty("ProgramCourses")]
        public virtual Program Program { get; set; }
    }

    
}
