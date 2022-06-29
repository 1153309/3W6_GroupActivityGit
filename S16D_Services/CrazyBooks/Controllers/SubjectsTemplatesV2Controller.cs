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
    public class SubjectsTemplatesV2Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public SubjectsTemplatesV2Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            return View(
                    new SubjectsIndexVM(
                        "Subjects",
                        "Subjects",
                        new List<PageLinks>() { PageLinks.Create },
                        await _db.Subjects
                            
                            .ToListAsync()
                    )
            );
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject subject = await _db.Subjects
                                                
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new SubjectsDisplayVM(
                        true,
                        "Subject",
                        "Subject",
                        new List<PageLinks>() { PageLinks.BackToList, PageLinks.Edit },
                        subject
                    )
            );
        }

        // GET: Subjects/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {
            bool isCreate = id == null;
            Subject subject = null;

            if (!isCreate)
            {
                // Extra stuff for Edit
                subject = await _db.Subjects.FirstOrDefaultAsync(e => e.Id == id);
                if (subject == null)
                {
                    return NotFound();
                }
            }

            return View(
                GetSubjectsUpsertVM(
                    isCreate, 
                    new Dictionary<string,SelectList­>(){
                    }, 
                    subject
                )
            );
        }

        // POST: Subjects/Upsert
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Upsert(SubjectsUpsertVM vm)
        {
            if(vm.IsCreate)
            {
                ModelState.Remove("Subject.Id");
            }

            if (ModelState.IsValid)
            {
                if (vm.IsCreate)
                {
                    _db.Add(vm.Subject);
                }
                else
                {
                    try
                    {
                        _db.Update(vm.Subject);
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!SubjectExists(vm.Subject.Id))
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
                    GetSubjectsUpsertVM(
                        vm.IsCreate, 
                        new Dictionary<string,SelectList­>(){
                        }, 
                        vm.Subject
                    )
            );
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Subject subject = await _db.Subjects
                                                
                                                    .FirstOrDefaultAsync(e => e.Id == id);
            if (subject == null)
            {
                return NotFound();
            }

            return View(
                    "Display",
                    new SubjectsDisplayVM(
                        false,
                        "Subject",
                        "Are you sure you want to delete this Subject",
                        new List<PageLinks>() { PageLinks.BackToList },
                        subject,
                        "Supprimer"
                    )
            );
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            Subject subject = await _db.Subjects.FindAsync(id);
            _db.Subjects.Remove(subject);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubjectExists(int id)
        {
            return _db.Subjects.Any(e => e.Id == id);
        }

        private SubjectsUpsertVM GetSubjectsUpsertVM(bool isCreate, Dictionary<string,SelectList­> selectLists, Subject subject = null)
        {
            return new SubjectsUpsertVM(
                        isCreate,
                        "Subject",
                        "Subject",
                        new List<PageLinks>() { PageLinks.BackToList },
                        isCreate ? "Ajouter" : "Modifier",
                        selectLists,
                        subject
            );
        }
    }
}
