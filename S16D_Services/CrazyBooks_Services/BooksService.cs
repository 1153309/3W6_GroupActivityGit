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
    public class BooksService : IBooksService
    {
        private readonly CrazyBooksDbContext _db;

        public BooksService(CrazyBooksDbContext db)
        {
            _db = db;
        }

        public bool Exists(int id)
        {
            return _db.Books.Any(e => e.Id == id);
        }

        public async Task<BooksIndexVM> GetIndexData()
        {
            return new BooksIndexVM(
                            "Books",
                            "Books",
                            new List<PageLinks>() { PageLinks.Create },
                            await _db.Books
                                    .Include(b => b.Publisher)
                                    .Include(b => b.Subject)
                                        .ToListAsync()
                    );
        }

        public async Task<BooksDisplayVM> GetDisplayData(ControllerAction action, int id)
        {
            bool isDetails = action == ControllerAction.Details;
            List<PageLinks> pageLinks = new List<PageLinks>() { PageLinks.BackToList };

            if (isDetails)
                pageLinks.Add(PageLinks.Edit);

            return new BooksDisplayVM(
                        isDetails,
                        "Book",
                        isDetails ? "Book" : "Are you sure you want to delete this Book",
                        pageLinks,
                        await _db.Books
                                    .Include(b => b.Publisher)
                                    .Include(b => b.Subject)
                                        .FirstOrDefaultAsync(e => e.Id == id),
                        isDetails ? null : "Supprimer"
                    );
        }

        public async Task<BooksUpsertVM> GetUpsertData(ControllerAction action, int? id)
        {
            return GetUpsertData(action, action == ControllerAction.Create ? null : await _db.Books.FirstOrDefaultAsync(e => e.Id == (int)id));
        }

        public BooksUpsertVM GetUpsertData(ControllerAction action, Book book)
        {
            bool isCreate = action == ControllerAction.Create;

            return new BooksUpsertVM(
                        isCreate,
                        "Book",
                        "Book",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        new Dictionary<string, SelectList­>(){
                            { "ListForPublisher_Id", new SelectList(_db.Publishers, "Id", "Name") },
                            { "ListForSubject_Id", new SelectList(_db.Subjects, "Id", "Name") },
                        },
                        isCreate ? null : book
                    );
        }

        public async Task<int> Add(Book book)
        {
            _db.Add(book);
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Update(Book book)
        {
            try
            {
                _db.Update(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
            return await _db.SaveChangesAsync();
        }

        public async Task<int> Delete(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            return await _db.SaveChangesAsync();
        }
    }
}
