using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class AuthorsUpsertVM
    {
        public AuthorsUpsertVM()
        { }
        public AuthorsUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists)
        {
            IsCreate = isCreate;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
        }
        public AuthorsUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists, Author author) : this(isCreate, pageTitle, pageHeading, links, submitButtonText, selectLists)
        {
            Author = author;
            Id = author?.Id;
        }
        public bool IsCreate { get; set; }
        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public Author Author { get; set; }
    }
}
