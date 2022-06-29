using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;
using System.Threading.Tasks;

namespace CrazyBooks_Services.Interfaces
{
    public interface IBooksService
    {
        bool Exists(int id);
        Task<BooksIndexVM> GetIndexData();
        Task<BooksDisplayVM> GetDisplayData(ControllerAction action, int id);
        Task<BooksUpsertVM> GetUpsertData(ControllerAction action, int? id);
        BooksUpsertVM GetUpsertData(ControllerAction action, Book book);
        Task<int> Add(Book book);
        Task<int> Update(Book book);
        Task<int> Delete(int id);
    }
}
