using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class SubjectsDisplayVM
    {
        public SubjectsDisplayVM()
        { }
        public SubjectsDisplayVM(bool isDetails, string pageTitle, string pageHeading, List<PageLinks> links, Subject subject, string submitButtonText = "")
        {
            IsDetails = isDetails;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
            Subject = new SubjectForDisplayVM(subject);
            Id = subject.Id;
        }

        public bool IsDetails { get; set; }
        public int Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public SubjectForDisplayVM Subject { get; set; }
    }
}
