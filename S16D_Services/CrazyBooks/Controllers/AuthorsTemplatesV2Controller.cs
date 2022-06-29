using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using CrazyBooks_Models.ViewModels;
using CrazyBooks_Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks.Controllers
{
    public class AuthorsTemplatesV2Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public AuthorsTemplatesV2Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(
                    new AuthorsIndexVM(
                        "Authors",
                        "Authors",
                        new List<PageLinks>() { PageLinks.Create },
                        await _db.Authors
                            .Include(a => a.AuthorDetail)
                            .ToListAsync()
                    )
            );
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author author = await _db.Authors
                                                
.Include(a => a.AuthorDetail)
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new AuthorsDisplayVM(
                        true,
                        "Author",
                        "Author",
                        new List<PageLinks>() { PageLinks.BackToList, PageLinks.Edit },
                        author
                    )
            );
        }

        // GET: Authors/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {
            bool isCreate = id == null;
            Author author = null;

            if (!isCreate)
            {
                // Extra stuff for Edit
                author = await _db.Authors.FirstOrDefaultAsync(e => e.Id == id);
                if (author == null)
                {
                    return NotFound();
                }
            }

            return View(
                GetAuthorsUpsertVM(
                    isCreate, 
                    new Dictionary<string,SelectList­>(){
                        { "ListForAuthorDetail_Id", new SelectList(_db.AuthorsDetail, "Id", "Id") },
                    }, 
                    author
                )
            );
        }

        // POST: Authors/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(AuthorsUpsertVM vm)
        {
            if(vm.IsCreate)
            {
                ModelState.Remove("Author.Id");
            }

            if (ModelState.IsValid)
            {
                if (vm.IsCreate)
                {
                    _db.Add(vm.Author);
                }
                else
                {
                    try
                    {
                        _db.Update(vm.Author);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!AuthorExists(vm.Author.Id))
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
                    GetAuthorsUpsertVM(
                        vm.IsCreate, 
                        new Dictionary<string,SelectList­>(){
                        { "ListForAuthorDetail_Id", new SelectList(_db.AuthorsDetail, "Id", "Id") },
                        }, 
                        vm.Author
                    )
            );
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Author author = await _db.Authors
                                                
.Include(a => a.AuthorDetail)
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new AuthorsDisplayVM(
                        false,
                        "Author",
                        "Are you sure you want to delete this Author",
                        new List<PageLinks>() { PageLinks.BackToList },
                        author,
                        "Supprimer"
                    )
            );
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Author author = await _db.Authors.FindAsync(id);
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }

        private AuthorsUpsertVM GetAuthorsUpsertVM(bool isCreate, Dictionary<string,SelectList­> selectLists, Author author = null)
        {
            return new AuthorsUpsertVM(
                        isCreate,
                        "Author",
                        "Author",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        selectLists,
                        author
            );
        }
    }
}
