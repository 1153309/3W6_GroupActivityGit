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
    public class SubjectsController : Controller
    {
        private readonly ISubjectsService _subjectsSvc;

        public SubjectsController(ISubjectsService subjectsSvc)
        {
            _subjectsSvc = subjectsSvc;
        }

        // GET: Subjects
        public async Task<IActionResult> Index()
        {
            return View(await _subjectsSvc.GetIndexData());
        }

        // GET: Subjects/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_subjectsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _subjectsSvc.GetDisplayData(ControllerAction.Details, (int)id));
        }

        // GET: Subjects/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {    
            if (id != null && !_subjectsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View(await _subjectsSvc.GetUpsertData(id == null ? ControllerAction.Create : ControllerAction.Edit, id));
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

            if (!ModelState.IsValid)
            {
                return View(_subjectsSvc.GetUpsertData(vm.IsCreate ? ControllerAction.Create : ControllerAction.Edit, vm.Subject));
            }

            if (vm.IsCreate)
            {
                await _subjectsSvc.Add(vm.Subject);
            }
            else
            {
                if (!_subjectsSvc.Exists(vm.Subject.Id))
                {
                    return NotFound();
                }

                await _subjectsSvc.Update(vm.Subject);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Subjects/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_subjectsSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _subjectsSvc.GetDisplayData(ControllerAction.Delete, (int)id));
        }

        // POST: Subjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _subjectsSvc.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
