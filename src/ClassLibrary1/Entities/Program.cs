using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ClassLibrary1.Entities
{
    [Index(nameof(SchoolCode), Name = "IX_SchoolCode")]
    public partial class Program
    {
        public Program()
        {
            ProgramCourses = new HashSet<ProgramCourse>();
        }

        [Key]
        [Column("ProgramID")]
        public int ProgramId { get; set; }
        [Required]
        [StringLength(100)]
        public string ProgramName { get; set; }
        [StringLength(100)]
        public string DiplomaName { get; set; }
        [Required]
        [StringLength(10)]
        public string SchoolCode { get; set; }
        [Column(TypeName = "money")]
        public decimal Tuition { get; set; }
        [Column(TypeName = "money")]
        public decimal InternationalTuition { get; set; }

        [InverseProperty(nameof(ProgramCourse.Program))]
        public virtual ICollection<ProgramCourse> ProgramCourses { get; set; }
    }
}
