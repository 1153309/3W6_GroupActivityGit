using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;
using System.Linq;

namespace CrazyBooks_Models.ViewModels
{
    public class GeneralViewInfosVM
    {
        public GeneralViewInfosVM()
        { }
        public GeneralViewInfosVM(string pageTitle, string pageHeading, List<PageLinks> links, string submitButtonText = "")
        {
            PageTitle = pageTitle;
            PageHeading = pageHeading;
            Links = links.Select(l => PageLink.Links[l]).ToList();
            SubmitButtonText = submitButtonText;
        }

        public string PageTitle { get; set; }
        public string PageHeading { get; set; }
        public ICollection<PageLinkInfos> Links { get; set; }
        public string SubmitButtonText { get; set; }
    }
}
