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
    public class AuthorsController : Controller
    {
        private readonly IAuthorsService _authorsSvc;

        public AuthorsController(IAuthorsService authorsSvc)
        {
            _authorsSvc = authorsSvc;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            return View(await _authorsSvc.GetIndexData());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_authorsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _authorsSvc.GetDisplayData(ControllerAction.Details, (int)id));
        }

        // GET: Authors/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {    
            if (id != null && !_authorsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View(await _authorsSvc.GetUpsertData(id == null ? ControllerAction.Create : ControllerAction.Edit, id));
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

            if (!ModelState.IsValid)
            {
                return View(_authorsSvc.GetUpsertData(vm.IsCreate ? ControllerAction.Create : ControllerAction.Edit, vm.Author));
            }

            if (vm.IsCreate)
            {
                await _authorsSvc.Add(vm.Author);
            }
            else
            {
                if (!_authorsSvc.Exists(vm.Author.Id))
                {
                    return NotFound();
                }

                await _authorsSvc.Update(vm.Author);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_authorsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _authorsSvc.GetDisplayData(ControllerAction.Delete, (int)id));
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _authorsSvc.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
