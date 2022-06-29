using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;
using System.Linq;

namespace CrazyBooks_Models.ViewModels
{
    public class SubjectsIndexVM
    {
        public SubjectsIndexVM()
        { }
        public SubjectsIndexVM(string pageTitle, string pageHeading, List<PageLinks> links, ICollection<Subject> subjects)
        {
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links);
            Subjects = subjects.Select(s => new SubjectForListVM(s)).ToList();
        }

        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public ICollection<SubjectForListVM> Subjects { get; set; }
    }
}
