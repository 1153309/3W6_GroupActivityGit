using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;

namespace CrazyBooks.Controllers
{
    public class BooksTemplatesV2Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public BooksTemplatesV2Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(
                    new BooksIndexVM(
                        "Books",
                        "Books",
                        new List<PageLinks>() { PageLinks.Create },
                        await _db.Books
                            .Include(b => b.Publisher).Include(b => b.Subject)
                            .ToListAsync()
                    )
            );
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = await _db.Books
                                                
.Include(b => b.Publisher)
.Include(b => b.Subject)
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new BooksDisplayVM(
                        true,
                        "Book",
                        "Book",
                        new List<PageLinks>() { PageLinks.BackToList, PageLinks.Edit },
                        book
                    )
            );
        }

        // GET: Books/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {
            bool isCreate = id == null;
            Book book = null;

            if (!isCreate)
            {
                // Extra stuff for Edit
                book = await _db.Books.FirstOrDefaultAsync(e => e.Id == id);
                if (book == null)
                {
                    return NotFound();
                }
            }

            return View(
                GetBooksUpsertVM(
                    isCreate, 
                    new Dictionary<string,SelectList­>(){
                        { "ListForPublisher_Id", new SelectList(_db.Publishers, "Id", "Name") },
                        { "ListForSubject_Id", new SelectList(_db.Subjects, "Id", "Name") },
                    }, 
                    book
                )
            );
        }

        // POST: Books/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(BooksUpsertVM vm)
        {
            if(vm.IsCreate)
            {
                ModelState.Remove("Book.Id");
            }

            if (ModelState.IsValid)
            {
                if (vm.IsCreate)
                {
                    _db.Add(vm.Book);
                }
                else
                {
                    try
                    {
                        _db.Update(vm.Book);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!BookExists(vm.Book.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(
                    GetBooksUpsertVM(
                        vm.IsCreate, 
                        new Dictionary<string,SelectList­>(){
                        { "ListForPublisher_Id", new SelectList(_db.Publishers, "Id", "Name") },
                        { "ListForSubject_Id", new SelectList(_db.Subjects, "Id", "Name") },
                        }, 
                        vm.Book
                    )
            );
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Book book = await _db.Books
                                                
.Include(b => b.Publisher)
.Include(b => b.Subject)
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new BooksDisplayVM(
                        false,
                        "Book",
                        "Are you sure you want to delete this Book",
                        new List<PageLinks>() { PageLinks.BackToList },
                        book,
                        "Supprimer"
                    )
            );
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Book book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _db.Books.Any(e => e.Id == id);
        }

        private BooksUpsertVM GetBooksUpsertVM(bool isCreate, Dictionary<string,SelectList­> selectLists, Book book = null)
        {
            return new BooksUpsertVM(
                        isCreate,
                        "Book",
                        "Book",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        selectLists,
                        book
            );
        }
    }
}
