using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;
using System.Linq;

namespace CrazyBooks_Models.ViewModels
{
    public class BooksIndexVM
    {
        public BooksIndexVM()
        { }
        public BooksIndexVM(string pageTitle, string pageHeading, List<PageLinks> links, ICollection<Book> books)
        {
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links);
            Books = books.Select(b => new BookForListVM(b)).ToList();
        }

        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public ICollection<BookForListVM> Books { get; set; }
    }
}
