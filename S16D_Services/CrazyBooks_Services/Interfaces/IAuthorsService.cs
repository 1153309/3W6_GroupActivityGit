using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;
using System.Threading.Tasks;

namespace CrazyBooks_Services.Interfaces
{
    public interface IAuthorsService
    {
        bool Exists(int id);
        Task<AuthorsIndexVM> GetIndexData();
        Task<AuthorsDisplayVM> GetDisplayData(ControllerAction action, int id);
        Task<AuthorsUpsertVM> GetUpsertData(ControllerAction action, int? id);
        AuthorsUpsertVM GetUpsertData(ControllerAction action, Author author);
        Task<int> Add(Author author);
        Task<int> Update(Author author);
        Task<int> Delete(int id);
    }
}
