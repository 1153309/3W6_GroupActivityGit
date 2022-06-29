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
    public class PublishersTemplatesV2Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public PublishersTemplatesV2Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            return View(
                    new PublishersIndexVM(
                        "Publishers",
                        "Publishers",
                        new List<PageLinks>() { PageLinks.Create },
                        await _db.Publishers
                            
                            .ToListAsync()
                    )
            );
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher publisher = await _db.Publishers
                                                
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new PublishersDisplayVM(
                        true,
                        "Publisher",
                        "Publisher",
                        new List<PageLinks>() { PageLinks.BackToList, PageLinks.Edit },
                        publisher
                    )
            );
        }

        // GET: Publishers/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {
            bool isCreate = id == null;
            Publisher publisher = null;

            if (!isCreate)
            {
                // Extra stuff for Edit
                publisher = await _db.Publishers.FirstOrDefaultAsync(e => e.Id == id);
                if (publisher == null)
                {
                    return NotFound();
                }
            }

            return View(
                GetPublishersUpsertVM(
                    isCreate, 
                    new Dictionary<string,SelectList­>(){
                    }, 
                    publisher
                )
            );
        }

        // POST: Publishers/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(PublishersUpsertVM vm)
        {
            if(vm.IsCreate)
            {
                ModelState.Remove("Publisher.Id");
            }

            if (ModelState.IsValid)
            {
                if (vm.IsCreate)
                {
                    _db.Add(vm.Publisher);
                }
                else
                {
                    try
                    {
                        _db.Update(vm.Publisher);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!PublisherExists(vm.Publisher.Id))
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
                    GetPublishersUpsertVM(
                        vm.IsCreate, 
                        new Dictionary<string,SelectList­>(){
                        }, 
                        vm.Publisher
                    )
            );
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher publisher = await _db.Publishers
                                                
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new PublishersDisplayVM(
                        false,
                        "Publisher",
                        "Are you sure you want to delete this Publisher",
                        new List<PageLinks>() { PageLinks.BackToList },
                        publisher,
                        "Supprimer"
                    )
            );
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Publisher publisher = await _db.Publishers.FindAsync(id);
            _db.Publishers.Remove(publisher);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PublisherExists(int id)
        {
            return _db.Publishers.Any(e => e.Id == id);
        }

        private PublishersUpsertVM GetPublishersUpsertVM(bool isCreate, Dictionary<string,SelectList­> selectLists, Publisher publisher = null)
        {
            return new PublishersUpsertVM(
                        isCreate,
                        "Publisher",
                        "Publisher",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        selectLists,
                        publisher
            );
        }
    }
}
