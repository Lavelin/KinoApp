using KinioApp.Models;
using KinioApp.Models.Data;
using KinioApp.ViesModels.Movies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


namespace KinioApp.Controllers
{
    //[Authorize(Roles = "admin, registeredUser")]
    public class MoviesController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public MoviesController(AppCtx context, UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var appCtx = _context.Movies
                .Include(s => s.GenreKino)                    // связываем фильмы с жанрами
                .OrderBy(f => f.Id);                          // сортировка по коду фильма
            return View(await appCtx.ToListAsync());            // полученный результат передаем в представление списком
        }

        

        // GET: Movies/Create
        public IActionResult Create()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["GenreId"] = new SelectList(_context.GenresKino
                  .OrderBy(f => f.Id), "Id", "Genre");
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateMoviesViewModel model)
        {
           // IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (_context.Movies
                .Where(f => f.MovieTitle == model.MovieTitle &&
                    f.CountryMovie == model.CountryMovie &&
                    f.YearMovie == model.YearMovie &&
                    f.GenreId == model.GenreId &&
                    f.DurationMovie == model.DurationMovie &&
                    f.StartOfRentalMovie == model.StartOfRentalMovie &&
                    f.EndOfRentalMovie == model.EndOfRentalMovie &&
                    f.RentalCompanyMovie == model.RentalCompanyMovie)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный фильм уже существует");
            }

            if (ModelState.IsValid)
            {
                // если введены корректные данные,
                // то создается экземпляр класса модели Movie, т.е. формируется запись в таблицу Movies
                Movie specialty = new()
                {
                    MovieTitle = model.MovieTitle,
                    CountryMovie = model.CountryMovie,
                    YearMovie = model.YearMovie,
                    DurationMovie = model.DurationMovie,
                    StartOfRentalMovie = model.StartOfRentalMovie,
                    EndOfRentalMovie = model.EndOfRentalMovie,
                    RentalCompanyMovie = model.RentalCompanyMovie,

                    // с помощью свойства модели получим идентификатор выбранного жанра
                    GenreId = model.GenreId
                };

                _context.Add(specialty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["GenreId"] = new SelectList(
                _context.GenresKino.
                OrderBy(f => f.Id),
                "Id", "Genre", model.GenreId);
            return View(model);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            EditMoviesViewModel model = new()
            {
                MovieTitle = movie.MovieTitle,
                CountryMovie = movie.CountryMovie,
                YearMovie = movie.YearMovie,
                DurationMovie = movie.DurationMovie,
                StartOfRentalMovie = movie.StartOfRentalMovie,
                EndOfRentalMovie = movie.EndOfRentalMovie,
                RentalCompanyMovie = movie.RentalCompanyMovie,
                GenreId = movie.GenreId
            };
            // в списке в качестве текущего элемента устанавливаем значение из базы данных,
            // указываем параметр specialty.IdFormOfStudy
            ViewData["GenreId"] = new SelectList(
                _context.GenresKino.
                OrderBy(f => f.Id),
                "Id", "Genre", model.GenreId);

            return View(model);
        }

        // POST: Movies/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditMoviesViewModel model)
        {
            var movie = await _context.Movies.FindAsync(id);

            if (id != movie.Id)
            {
                return NotFound();
            }            

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    movie.MovieTitle = model.MovieTitle;
                    movie.CountryMovie = model.CountryMovie;
                    movie.YearMovie = model.YearMovie;
                    movie.DurationMovie = model.DurationMovie;
                    movie.StartOfRentalMovie = model.StartOfRentalMovie;
                    movie.EndOfRentalMovie = model.EndOfRentalMovie;
                    movie.RentalCompanyMovie = model.RentalCompanyMovie;
                    movie.GenreId = model.GenreId;

                    _context.Update(movie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            /*}
            ViewData["GenreId"] = new SelectList(
                _context.GenresKino.
                OrderBy(f => f.Id),
                "Id", "Genre", model.GenreId);

            return View(model);*/
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Movies == null)
            {
                return Problem("Entity set 'AppCtx.Movies'  is null.");
            }
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        // GET: Movies/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Movies == null)
            {
                return NotFound();
            }

            var movie = await _context.Movies
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        private bool MovieExists(short id)
        {
          return (_context.Movies?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
