using CrazyBooks_DataAccess.Data;
using CrazyBooks_Models.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyBooks.Controllers
{
    public class BooksTemplatesV1Controller : Controller
    {
        private readonly CrazyBooksDbContext _db;

        public BooksTemplatesV1Controller(CrazyBooksDbContext db)
        {
            _db = db;
        }

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var crazyBooksDbContext = _db.Books.Include(b => b.Publisher).Include(b => b.Subject);
            return View(await crazyBooksDbContext.ToListAsync());
        }

        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(b => b.Publisher)
                .Include(b => b.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["Publisher_Id"] = new SelectList(_db.Publishers, "Id", "Name");
            ViewData["Subject_Id"] = new SelectList(_db.Subjects, "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,ISBN,Price,Promo,Available,Resume,PublishedDate,Subject_Id,Publisher_Id")] Book book)
        {
            if (ModelState.IsValid)
            {
                _db.Add(book);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Publisher_Id"] = new SelectList(_db.Publishers, "Id", "Name", book.Publisher_Id);
            ViewData["Subject_Id"] = new SelectList(_db.Subjects, "Id", "Name", book.Subject_Id);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["Publisher_Id"] = new SelectList(_db.Publishers, "Id", "Name", book.Publisher_Id);
            ViewData["Subject_Id"] = new SelectList(_db.Subjects, "Id", "Name", book.Subject_Id);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,ISBN,Price,Promo,Available,Resume,PublishedDate,Subject_Id,Publisher_Id")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Update(book);
                    await _db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
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
            ViewData["Publisher_Id"] = new SelectList(_db.Publishers, "Id", "Name", book.Publisher_Id);
            ViewData["Subject_Id"] = new SelectList(_db.Subjects, "Id", "Name", book.Subject_Id);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var book = await _db.Books
                .Include(b => b.Publisher)
                .Include(b => b.Subject)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = await _db.Books.FindAsync(id);
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
            return _db.Books.Any(e => e.Id == id);
        }
    }
}
