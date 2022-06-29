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
    public class PublishersController : Controller
    {
        private readonly IPublishersService _publishersSvc;

        public PublishersController(IPublishersService publishersSvc)
        {
            _publishersSvc = publishersSvc;
        }

        // GET: Publishers
        public async Task<IActionResult> Index()
        {
            return View(await _publishersSvc.GetIndexData());
        }

        // GET: Publishers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || !_publishersSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _publishersSvc.GetDisplayData(ControllerAction.Details, (int)id));
        }

        // GET: Publishers/Upsert/5
        public async Task<IActionResult> Upsert(int? id)
        {    
            if (id != null && !_publishersSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View(await _publishersSvc.GetUpsertData(id == null ? ControllerAction.Create : ControllerAction.Edit, id));
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

            if (!ModelState.IsValid)
            {
                return View(_publishersSvc.GetUpsertData(vm.IsCreate ? ControllerAction.Create : ControllerAction.Edit, vm.Publisher));
            }

            if (vm.IsCreate)
            {
                await _publishersSvc.Add(vm.Publisher);
            }
            else
            {
                if (!_publishersSvc.Exists(vm.Publisher.Id))
                {
                    return NotFound();
                }

                await _publishersSvc.Update(vm.Publisher);
            }

            return RedirectToAction(nameof(Index));
        }

        // GET: Publishers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || !_publishersSvc.Exists((int)id))
            {
                return NotFound();
            }

            return View("Display", await _publishersSvc.GetDisplayData(ControllerAction.Delete, (int)id));
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _publishersSvc.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
