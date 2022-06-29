using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class BooksDisplayVM
    {
        public BooksDisplayVM()
        { }
        public BooksDisplayVM(bool isDetails, string pageTitle, string pageHeading, List<PageLinks> links, Book book, string submitButtonText = "")
        {
            IsDetails = isDetails;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
            Book = new BookForDisplayVM(book);
            Id = book.Id;
        }

        public bool IsDetails { get; set; }
        public int Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public BookForDisplayVM Book { get; set; }
    }
}
