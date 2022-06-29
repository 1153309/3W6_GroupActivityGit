using CrazyBooks_Models.Models;
using CrazyBooks_Utility;
using System.Collections.Generic;

namespace CrazyBooks_Models.ViewModels
{
    public class PublishersDisplayVM
    {
        public PublishersDisplayVM()
        { }
        public PublishersDisplayVM(bool isDetails, string pageTitle, string pageHeading, List<PageLinks> links, Publisher publisher, string submitButtonText = "")
        {
            IsDetails = isDetails;
            GeneralViewInfos = new GeneralViewInfosVM(pageTitle, pageHeading, links, submitButtonText);
            Publisher = new PublisherForDisplayVM(publisher);
            Id = publisher.Id;
        }

        public bool IsDetails { get; set; }
        public int Id { get; set; }
        public GeneralViewInfosVM GeneralViewInfos { get; set; }
        public PublisherForDisplayVM Publisher { get; set; }
    }
}
