using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;
using System.Linq;

namespace CrazyBooks_Models.ViewModels
{
    public class PublishersIndexVM
    {
        public PublishersIndexVM()
        { }
        public PublishersIndexVM(string pageTitle, string pageHeading, List<PageLinks> links, ICollection<Publisher> publishers)
        {
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links);
            Publishers = publishers.Select(b => new PublisherForListVM(b)).ToList();
        }

        public int? Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public ICollection<PublisherForListVM> Publishers { get; set; }
    }
}
