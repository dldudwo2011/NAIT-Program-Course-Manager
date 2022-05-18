using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ClassLibrary1.BLL;
using ClassLibrary1.Entities;
using ClassLibrary1.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyApp.Namespace
{
    public class QueryModel : PageModel
    {
        private readonly SearchDatabaseService _service;
        private readonly ManageCourseService _service2;

        public QueryModel(SearchDatabaseService service, ManageCourseService service2)
        {
            _service = service ?? throw new ArgumentNullException();
            _service2 = service2 ?? throw new ArgumentNullException();
        }

        public PartialList<ProgramCourse> programCourses { get; set; }

        public List<SelectListItem> programs { get; set; }

        [BindProperty]
        public string PartialName { get; set; }

        public int selectedProgram { get; set; }

        public int Current { get; set; }

        public int NextPage
        {
            get
            {
                return Current < LastPage ? Current + 1 : LastPage;
            }
        }

        public int FirstPage { get; set; } = 1;

        public int lastDigit
        {
            get
            {
                return LastPage % PageSize;
            }
        }
        public int LastPage
        {
            get
            {
                if (TotalResults % PageSize != 0)
                {
                    return (TotalResults / PageSize) + 1;
                }
                else
                {
                    return TotalResults / PageSize;
                }
            }
        }

        [BindProperty]
        public int PageSize { get; set; } = 20;

        //[BindProperty]
        //public string aspPageSize { get; set; }

        public int PreviousPage
        {
            get
            {
                return Current > 1 ? Current - 1 : 1;
            }
        }

        public int LastPageLink
        {
            get
            {
                int last = (Current + PageSize) - 1;

                if (LastPage < PageSize || (Current <= LastPage && Current >= (LastPage - (lastDigit + 1))))
                {
                    return LastPage;
                }
                else
                {
                    return last;
                }

            }
        }

        public int PageIndex
        {
            get
            {
                return Current - 1;
            }
        }

        public int FromItem
        {
            get
            {
                int offset = PageIndex * PageSize;

                return offset + 1;
            }
        }

        public int ToItem
        {
            get
            {
                if (Current == LastPage)
                {
                    return TotalResults;
                }
                else
                {

                    return FromItem + PageSize - 1;
                }
            }
        }

        public int TotalResults
        {
            get
            {
                return programCourses.TotalCount;
            }
        }


        public void OnGet(int? currentPage, int selectedProgram)
        {
            PopulateDropDown();

            Current = currentPage.HasValue ? currentPage.Value : 1;

            int skip = PageIndex * PageSize;

            if (selectedProgram != 0)
            {
                this.selectedProgram = selectedProgram;

                programCourses = _service.GetCourses(selectedProgram, skip, PageSize);
            }

            else
            {
                programCourses = _service.GetCourses(skip, PageSize);
            }
            

        }

        public void PopulateDropDown()
        {
            programs = _service2.ListPrograms().Select(x => new SelectListItem(x.ProgramName, x.ProgramId.ToString())).ToList();
        }
    }
}
