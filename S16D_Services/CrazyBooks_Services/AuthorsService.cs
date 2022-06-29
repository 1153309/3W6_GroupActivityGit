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
    public class AuthorsService : IAuthorsService
    {
        private readonly CrazyBooksDbContext _db;

        public AuthorsService(CrazyBooksDbContext db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }

        public async Task<AuthorsIndexVM> GetIndexData()
        {
            return new AuthorsIndexVM(
                            "Authors",
                            "Authors",
                            new List<PageLinks>() { PageLinks.Create },
                            await _db.Authors
                                        .ToListAsync()
                    );
        }

        public async Task<AuthorsDisplayVM> GetDisplayData(ControllerAction action, int id)
        {
            bool isDetails = action == ControllerAction.Details;
            List<PageLinks> pageLinks = new List<PageLinks>() { PageLinks.BackToList };

            if (isDetails)
                pageLinks.Add(PageLinks.Edit);

            return new AuthorsDisplayVM(
                        isDetails,
                        "Author",
                        isDetails ? "Author" : "Are you sure you want to delete this Author",
                        pageLinks,
                        await _db.Authors
                                    .FirstOrDefaultAsync(e => e.Id == id),
                        isDetails ? null : "Supprimer"
                    );
        }

        public async Task<AuthorsUpsertVM> GetUpsertData(ControllerAction action, int? id)
        {
            return GetUpsertData(action, action == ControllerAction.Create ? null : await _db.Authors.FirstOrDefaultAsync(e => e.Id == (int)id));
        }

        public AuthorsUpsertVM GetUpsertData(ControllerAction action, Author author)
        {
            bool isCreate = action == ControllerAction.Create;

            return new AuthorsUpsertVM(
                        isCreate,
                        "Author",
                        "Author",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        null,
                        isCreate ? null : author
                    );
        }

        public async Task<int> Add(Author author)
        {
            _db.Add(author);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Update(Author author)
        {
            try
            {
                _db.Update(author);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Author author = await _db.Authors.FindAsync(id);
            _db.Authors.Remove(author);
            return await _db.SaveChangesAsync();
        }
    }
}
