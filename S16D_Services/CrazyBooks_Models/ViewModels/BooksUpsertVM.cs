using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class BooksUpsertVM
    {
        public BooksUpsertVM()
        { }
        public BooksUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists)
        {
            IsCreate = isCreate;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
            ListForSubject_Id = selectLists["ListForSubject_Id"];
            ListForPublisher_Id = selectLists["ListForPublisher_Id"];
        }
        public BooksUpsertVM(bool isCreate, string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText, Dictionary<string, SelectList­> selectLists, Book book) : this(isCreate, pageTitle, pageHeading, links, submitButtonText, selectLists)
        {
            Book = book;
            Id = book?.Id;
        }
        public bool IsCreate { get; set; }
        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public Book Book { get; set; }        
        public IEnumerable<SelectListItem> ListForSubject_Id { get; set; }
        public IEnumerable<SelectListItem> ListForPublisher_Id { get; set; }
    }
}
