using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClassLibrary1.DAL;
using ClassLibrary1.Entities;
using ClassLibrary1.Collections;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;

namespace ClassLibrary1.BLL
{
    public class SearchDatabaseService
    {
        public readonly StarTEDContext _context;
        public SearchDatabaseService(StarTEDContext context)
        {
            _context = context ?? throw new ArgumentException();
        }

        public int countAllCourse()
        {
            return _context.ProgramCourses.Count();
        }
        public PartialList<ProgramCourse> GetCourses(string partialCourseName, int skip, int take)
        {
            var items = _context.ProgramCourses.Where(item => item.CourseId.Contains(partialCourseName)).Skip(skip).Take(take).Include(x => x.Course).Include(x => x.Program);
            var total = _context.ProgramCourses.Where(item => item.CourseId.Contains(partialCourseName)).Count();
            return new PartialList<ProgramCourse>(total, items.ToList());
        }

        public PartialList<ProgramCourse> GetCourses(int programID, int skip, int take)
        {
            var items = _context.ProgramCourses.Where(item => item.ProgramId.Equals(programID)).Skip(skip).Take(take).Include(x => x.Course).Include(x => x.Program);
            var total = _context.ProgramCourses.Where(item => item.ProgramId.Equals(programID)).Count();
            return new PartialList<ProgramCourse>(total, items.ToList());
        }

        public PartialList<ProgramCourse> GetCourses(int skip, int take)
        {
            int total = _context.ProgramCourses.Count();
            var items = _context.ProgramCourses.Skip(skip).Take(take).Include(x => x.Course).Include(x => x.Program);
            return new PartialList<ProgramCourse>(total, items.ToList());
        }
    }
}
