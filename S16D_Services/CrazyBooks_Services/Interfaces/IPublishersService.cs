using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;
using System.Threading.Tasks;

namespace CrazyBooks_Services.Interfaces
{
    public interface IPublishersService
    {
        bool Exists(int id);
        Task<PublishersIndexVM> GetIndexData();
        Task<PublishersDisplayVM> GetDisplayData(ControllerAction action, int id);
        Task<PublishersUpsertVM> GetUpsertData(ControllerAction action, int? id);
        PublishersUpsertVM GetUpsertData(ControllerAction action, Publisher publisher);
        Task<int> Add(Publisher publisher);
        Task<int> Update(Publisher publisher);
        Task<int> Delete(int id);
    }
}
