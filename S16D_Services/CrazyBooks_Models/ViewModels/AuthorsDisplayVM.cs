using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class AuthorsDisplayVM
    {
        public AuthorsDisplayVM()
        { }
        public AuthorsDisplayVM(bool isDetails, string pageTitle, string pageHeading, List<PageLinks> links, Author author, string submitButtonText = "")
        {
            IsDetails = isDetails;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
            Author = new AuthorForDisplayVM(author);
            Id = author.Id;
        }

        public bool IsDetails { get; set; }
        public int Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public AuthorForDisplayVM Author { get; set; }
    }
}
