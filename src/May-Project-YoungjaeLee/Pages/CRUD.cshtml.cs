using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassLibrary1.Entities;
using ClassLibrary1.BLL;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net;

namespace MyApp.Namespace
{
    public class CRUDModel : PageModel
    {
        private readonly ManageCourseService _service;

        public CRUDModel(ManageCourseService service)
        {
            _service = service ?? throw new ArgumentNullException();
        }

        [BindProperty]
        public ProgramCourse programCourse { get; set; }

        public List<SelectListItem> programs { get; set; }

        public List<SelectListItem> courses { get; set; }

        [BindProperty]
        public string filterWord { get; set; }

        public string ErrorMessage { get; set; }

        public void OnGet(int programCourseID, string filterWord)
        {

            if (programCourseID != 0)
            {
                programCourse = _service.FindProgramCourseWithID(programCourseID);
            }

            if(filterWord != null)
            {
                PopulateDropDown(filterWord);
            }

            else
            {
                PopulateDropDown();
            }

            
        }
        
        public IActionResult OnPostCancel()
        {
            return RedirectToPage("Query");
        }

        public IActionResult OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.AddProgramCourse(programCourse);

                    return RedirectToPage("CRUD", new { programCourseID = programCourse.ProgramCourseId});
                }

                catch (Exception ex)
                {
                    Exception rootCause = ex;

                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDown();

                    return Page();
                }
            }

            PopulateDropDown();
            return Page();
        }

        public IActionResult OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.UpdateProgramCourse(programCourse);

                    return RedirectToPage("CRUD", new { programCourseID = programCourse.ProgramCourseId});
                }

                catch (Exception ex)
                {
                    Exception rootCause = ex;

                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDown();

                    return Page();
                }
            }

            PopulateDropDown();
            return Page();
        }


        public IActionResult OnPostDelete()
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.DeleteProgramCourse(programCourse);

                    return RedirectToPage("CRUD", new { programCourseID = programCourse.ProgramCourseId});
                }

                catch (Exception ex)
                {
                    Exception rootCause = ex;

                    while (rootCause.InnerException != null)
                        rootCause = rootCause.InnerException;

                    ErrorMessage = rootCause.Message;
                    PopulateDropDown();

                    return Page();
                }
            }

            PopulateDropDown();
            return Page();
        }

        public void PopulateDropDown(string filterWord = null)
        {
            if (String.IsNullOrEmpty(filterWord))
            {
                programs = _service.ListPrograms().Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
                courses = _service.ListCourses().Select(x => new SelectListItem(x.CourseName, x.CourseId.ToString())).ToList();
            }

            else
            {
                programs = _service.ListPrograms().Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
                courses = _service.ListCourses(filterWord).Select(x => new SelectListItem(x.CourseName, x.CourseId.ToString())).ToList();
            }
            
        }
    }
}
