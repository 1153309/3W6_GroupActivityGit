using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;
using System.Linq;

namespace CrazyBooks_Models.ViewModels
{
    public class AuthorsIndexVM
    {
        public AuthorsIndexVM()
        { }
        public AuthorsIndexVM(string pageTitle, string pageHeading, List<PageLinks> links, ICollection<Author> authors)
        {
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links);
            Authors = authors.Select(a => new AuthorForListVM(a)).ToList();
        }

        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public ICollection<AuthorForListVM> Authors { get; set; }
    }
}
