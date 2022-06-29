using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class PublishersUpsertVM
    {
        public PublishersUpsertVM()
        { }
        public PublishersUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists)
        {
            IsCreate = isCreate;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
        }
        public PublishersUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists, Publisher publisher) : this(isCreate, pageTitle, pageHeading, links, submitButtonText, selectLists)
        {
            Publisher = publisher;
            Id = publisher?.Id;
        }
        public bool IsCreate { get; set; }
        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public Publisher Publisher { get; set; }        
    }
}
