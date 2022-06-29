using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using CrazyBooks_Services.Interfaces;
using CrazyBooks_Utility;
using CrazyBooks_Models.ViewModels;

namespace CrazyBooks.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBooksService _booksSvc;

        public BooksController(IBooksService booksSvc)
        {
            _booksSvc = booksSvc;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            return View(await _booksSvc.GetIndexData());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_booksSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _booksSvc.GetDisplayData(ControllerAction.Details, (int)id));
        }

        // GET: Books/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {    
            if (id != null && !_booksSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View(await _booksSvc.GetUpsertData(id == null ? ControllerAction.Create : ControllerAction.Edit, id));
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

            if (!ModelState.IsValid)
            {
                return View(_booksSvc.GetUpsertData(vm.IsCreate ? ControllerAction.Create : ControllerAction.Edit, vm.Book));
            }

            if (vm.IsCreate)
            {
                await _booksSvc.Add(vm.Book);
            }
            else
            {
                if (!_booksSvc.Exists(vm.Book.Id))
                {
                    return NotFound();
                }

                await _booksSvc.Update(vm.Book);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_booksSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _booksSvc.GetDisplayData(ControllerAction.Delete, (int)id));
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _booksSvc.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
