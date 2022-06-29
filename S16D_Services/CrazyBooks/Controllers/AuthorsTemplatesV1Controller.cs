using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks.Controllers
{
    public class AuthorsTemplatesV1Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public AuthorsTemplatesV1Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Authors
        public async Task<IActionResult> Index()
        {
            var crazyBooksDbContext = _db.Authors.Include(a => a.AuthorDetail);
            return View(await crazyBooksDbContext.ToListAsync());
        }

        // GET: Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Authors
                .Include(a => a.AuthorDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            ViewData["AuthorDetail_Id"] = new SelectList(_db.AuthorsDetail, "Id", "Id");
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,AuthorDetail_Id")] Author author)
        {
            if (ModelState.IsValid)
            {
                _db.Add(author);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorDetail_Id"] = new SelectList(_db.AuthorsDetail, "Id", "Id", author.AuthorDetail_Id);
            return View(author);
        }

        // GET: Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            ViewData["AuthorDetail_Id"] = new SelectList(_db.AuthorsDetail, "Id", "Id", author.AuthorDetail_Id);
            return View(author);
        }

        // POST: Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,AuthorDetail_Id")] Author author)
        {
            if (id != author.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(author);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorDetail_Id"] = new SelectList(_db.AuthorsDetail, "Id", "Id", author.AuthorDetail_Id);
            return View(author);
        }

        // GET: Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var author = await _db.Authors
                .Include(a => a.AuthorDetail)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = await _db.Authors.FindAsync(id);
            _db.Authors.Remove(author);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            return _db.Authors.Any(e => e.Id == id);
        }
    }
}
