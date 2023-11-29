using Humanizer.Localisation;
using KinioApp.Models;
using KinioApp.Models.Data;
using KinioApp.ViesModels.GenreKino;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KinioApp.Controllers
{ 
    public class GenreKinoesController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public GenreKinoesController(
            AppCtx context,
            UserManager<User> user)
        {
            _context = context;
            _userManager = user; 
        }

        // GET: GenreKinoes
        public async Task<IActionResult> Index()
        {
            var appCtx = _context.GenresKino
                .OrderBy(f => f.Genre); //сортируем все записи по имени форм обучения

             return _context.GenresKino != null ? 
                View(await appCtx.ToListAsync()) :
                Problem("Entity set 'AppCtx.GenreKino'  is null.");
        }

        
        // GET: GenreKinoes/Create
        public IActionResult Create()
        {
            return View();
        }
          
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGenreKinoViewModel model)
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            
            if (_context.GenresKino
               .Where(f => f.Genre == model.Genre).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный жанр уже существует");
            }

            if (ModelState.IsValid)
            {
                GenreKino genreKino = new()
                {
                    Genre = model.Genre,
                };

                _context.Add(genreKino);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        } 

        // GET: GenreKinoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.GenresKino == null)
            {
                return NotFound();
            }

            var genre = await _context.GenresKino.FindAsync(id);
            if (genre == null)
            {
                return NotFound();
            }


            EditGenreKinoViewModel model = new()
            {
                Id = genre.Id,
                Genre = genre.Genre
            };

            return View(model);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditGenreKinoViewModel model)
        {
            if (_context.GenresKino
               .Where(f => f.Genre == model.Genre).FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный жанр уже существует");
            }

            GenreKino genreKino = await _context.GenresKino.FindAsync(id);

            if (id != genreKino.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    genreKino.Genre = model.Genre;
                    _context.Update(genreKino);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GenreKinoExists(genreKino.Id))
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
            return View(model);
        }

        // GET: GenreKinoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.GenresKino == null)
            {
                return NotFound();
            }

            var genreKino = await _context.GenresKino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreKino == null)
            {
                return NotFound();
            }

            return View(genreKino);
        }

        // POST: GenreKinoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.GenresKino == null)
            {
                return Problem("Entity set 'AppCtx.GenreKino'  is null.");
            }
            var genreKino = await _context.GenresKino.FindAsync(id);
            if (genreKino != null)
            {
                _context.GenresKino.Remove(genreKino);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: GenreKinoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.GenresKino == null)
            {
                return NotFound();
            }

            var genreKino = await _context.GenresKino
                .FirstOrDefaultAsync(m => m.Id == id);
            if (genreKino == null)
            {
                return NotFound();
            }

            return View(genreKino);
        }

        private bool GenreKinoExists(int id)
        {
          return (_context.GenresKino?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        
 
    }
}
