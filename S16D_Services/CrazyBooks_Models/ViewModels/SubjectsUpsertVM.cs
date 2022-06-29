using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class SubjectsUpsertVM
    {
        public SubjectsUpsertVM()
        { }
        public SubjectsUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists)
        {
            IsCreate = isCreate;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
        }
        public SubjectsUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists, Subject subject) : this(isCreate, pageTitle, pageHeading, links, submitButtonText, selectLists)
        {
            Subject = subject;
            Id = subject?.Id;
        }
        public bool IsCreate { get; set; }
        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public Subject Subject { get; set; }        
    }
}
