using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;
using System.Threading.Tasks;

namespace CrazyBooks_Services.Interfaces
{
    public interface ISubjectsService
    {
        bool Exists(int id);
        Task<SubjectsIndexVM> GetIndexData();
        Task<SubjectsDisplayVM> GetDisplayData(ControllerAction action, int id);
        Task<SubjectsUpsertVM> GetUpsertData(ControllerAction action, int? id);
        SubjectsUpsertVM GetUpsertData(ControllerAction action, Subject subject);
        Task<int> Add(Subject subject);
        Task<int> Update(Subject subject);
        Task<int> Delete(int id);
    }
}
