using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Services.Interfaces;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks_Services
{
    public class SubjectsService : ISubjectsService
    {
        private readonly CrazyBooksDbContext _db;

        public SubjectsService(CrazyBooksDbContext db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            return _db.Subjects.Any(e => e.Id == id);
        }

        public async Task<SubjectsIndexVM> GetIndexData()
        {
            return new SubjectsIndexVM(
                            "Subjects",
                            "Subjects",
                            new List<PageLinks>() { PageLinks.Create },
                            await _db.Subjects
                                        .ToListAsync()
                    );
        }

        public async Task<SubjectsDisplayVM> GetDisplayData(ControllerAction action, int id)
        {
            bool isDetails = action == ControllerAction.Details;
            List<PageLinks> pageLinks = new List<PageLinks>() { PageLinks.BackToList };

            if (isDetails)
                pageLinks.Add(PageLinks.Edit);

            return new SubjectsDisplayVM(
                        isDetails,
                        "Subject",
                        isDetails ? "Subject" : "Are you sure you want to delete this Subject",
                        pageLinks,
                        await _db.Subjects
                                    .FirstOrDefaultAsync(e => e.Id == id),
                        isDetails ? null : "Supprimer"
                    );
        }

        public async Task<SubjectsUpsertVM> GetUpsertData(ControllerAction action, int? id)
        {
            return GetUpsertData(action, action == ControllerAction.Create ? null : await _db.Subjects.FirstOrDefaultAsync(e => e.Id == (int)id));
        }

        public SubjectsUpsertVM GetUpsertData(ControllerAction action, Subject subject)
        {
            bool isCreate = action == ControllerAction.Create;

            return new SubjectsUpsertVM(
                        isCreate,
                        "Subject",
                        "Subject",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        null,
                        isCreate ? null : subject
                    );
        }

        public async Task<int> Add(Subject subject)
        {
            _db.Add(subject);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Update(Subject subject)
        {
            try
            {
                _db.Update(subject);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Subject subject = await _db.Subjects.FindAsync(id);
            _db.Subjects.Remove(subject);
            return await _db.SaveChangesAsync();
        }
    }
}
