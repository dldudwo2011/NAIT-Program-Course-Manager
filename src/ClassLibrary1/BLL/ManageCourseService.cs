using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.DAL;
using ClassLibrary1.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ClassLibrary1.BLL
{
    public class ManageCourseService
    {
        public readonly StarTEDContext _context;
        public ManageCourseService(StarTEDContext context)
        {
            _context = context ?? throw new ArgumentException();
        }

        public List<Program> ListPrograms()
        {
            return _context.Programs.ToList();
        }

        public List<Course> ListCourses()
        {
            return _context.Courses.ToList();
        }

        public List<Course> ListCourses(string filterWord)
        {
            List<Course> list = new();
            
            list = _context.Courses.Where(item => item.CourseId.Contains(filterWord)).ToList();

            List<Course> list2 = new();

            list2 = _context.Courses.Where(item => item.CourseName.Contains(filterWord)).ToList();

            if(list.Any() && list2.Any())
            {
                list.Union<Course>(list2);
                return list;
            }

            else if (list.Any())
            {
                return list;
            }

            else if (list2.Any())
            {
                return list2;
            }

            else
            {
                return new List<Course>();
            }        
        }


        public ProgramCourse FindProgramCourseWithID(int programCourseID)
        {
            return _context.ProgramCourses.Find(programCourseID);
        }

        public void AddProgramCourse(ProgramCourse programCourse)
        {
            _context.ProgramCourses.Add(programCourse);
            _context.SaveChanges();
        }

        public void UpdateProgramCourse(ProgramCourse programCourse)
        {
            var existing = _context.Entry(programCourse);
            existing.State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteProgramCourse(ProgramCourse programCourse)
        {
            var existing = _context.Entry(programCourse);
            existing.Entity.Active = false;
            existing.State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
