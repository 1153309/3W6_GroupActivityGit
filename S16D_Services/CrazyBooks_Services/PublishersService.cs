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
    public class PublishersService : IPublishersService
    {
        private readonly CrazyBooksDbContext _db;

        public PublishersService(CrazyBooksDbContext db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            return _db.Publishers.Any(e => e.Id == id);
        }

        public async Task<PublishersIndexVM> GetIndexData()
        {
            return new PublishersIndexVM(
                            "Publishers",
                            "Publishers",
                            new List<PageLinks>() { PageLinks.Create },
                            await _db.Publishers
                                        .ToListAsync()
                    );
        }

        public async Task<PublishersDisplayVM> GetDisplayData(ControllerAction action, int id)
        {
            bool isDetails = action == ControllerAction.Details;
            List<PageLinks> pageLinks = new List<PageLinks>() { PageLinks.BackToList };

            if (isDetails)
                pageLinks.Add(PageLinks.Edit);

            return new PublishersDisplayVM(
                        isDetails,
                        "Publisher",
                        isDetails ? "Publisher" : "Are you sure you want to delete this Publisher",
                        pageLinks,
                        await _db.Publishers
                                    .FirstOrDefaultAsync(e => e.Id == id),
                        isDetails ? null : "Supprimer"
                    );
        }

        public async Task<PublishersUpsertVM> GetUpsertData(ControllerAction action, int? id)
        {
            return GetUpsertData(action, action == ControllerAction.Create ? null : await _db.Publishers.FirstOrDefaultAsync(e => e.Id == (int)id));
        }

        public PublishersUpsertVM GetUpsertData(ControllerAction action, Publisher publisher)
        {
            bool isCreate = action == ControllerAction.Create;

            return new PublishersUpsertVM(
                        isCreate,
                        "Publisher",
                        "Publisher",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        null,
                        isCreate ? null : publisher
                    );
        }

        public async Task<int> Add(Publisher publisher)
        {
            _db.Add(publisher);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Update(Publisher publisher)
        {
            try
            {
                _db.Update(publisher);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Publisher publisher = await _db.Publishers.FindAsync(id);
            _db.Publishers.Remove(publisher);
            return await _db.SaveChangesAsync();
        }
    }
}
