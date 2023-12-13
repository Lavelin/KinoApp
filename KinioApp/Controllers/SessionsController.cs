using KinioApp.Models;
using KinioApp.Models.Data;
using KinioApp.ViesModels.Movies;
using KinioApp.ViesModels.Session;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KinioApp.Controllers
{
    public class SessionsController : Controller
    {
        private readonly AppCtx _context;
        private readonly UserManager<User> _userManager;

        public SessionsController(AppCtx context, UserManager<User> user)
        {
            _context = context;
            _userManager = user;
        }

        // GET: Sessions
        public async Task<IActionResult> Index()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            var appCtx = _context.Sessions
                .Include(s => s.Movie)                    
                .OrderBy(f => f.Id);                         
            return View(await appCtx.ToListAsync());            
        }

        

        // GET: Sessions/Create
        public IActionResult Create()
        {
            //IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);
            ViewData["MovieId"] = new SelectList(_context.Movies
                  .OrderBy(f => f.Id), "Id", "MovieTitle");
            return View();
        }

        // POST: Sessions/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateSessionViewModel model)
        {
            // IdentityUser user = await _userManager.FindByNameAsync(HttpContext.User.Identity.Name);

            if (_context.Sessions
                .Where(f => f.DateAndTime == model.DateAndTime &&
                    f.Hall == model.Hall &&
                    f.MovieId == model.MovieId)
                .FirstOrDefault() != null)
            {
                ModelState.AddModelError("", "Введеный сеанс уже существует");
            }

            if (ModelState.IsValid)
            {
                // если введены корректные данные,
                // то создается экземпляр класса модели Movie, т.е. формируется запись в таблицу Movies
                Session specialty = new()
                {
                    DateAndTime = model.DateAndTime,
                    Hall = model.Hall,


                    // с помощью свойства модели получим идентификатор выбранного фильма
                    MovieId = model.MovieId
                };

                _context.Add(specialty);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["MovieId"] = new SelectList(
                _context.Movies.
                OrderBy(f => f.Id),
                "Id", "MovieTitle", model.MovieId);
            return View(model);
        }

        // GET: Sessions/Edit/5
        public async Task<IActionResult> Edit(short? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
           /* ViewData["MovieId"] = new SelectList(_context.Movies, "Id", "MovieId", session.MovieId);
            return View(session);*/

            EditSessionViewModel model = new()
            {
                DateAndTime = session.DateAndTime,
                Hall = session.Hall,
                MovieId = session.MovieId
            };

            // в списке в качестве текущего элемента устанавливаем значение из базы данных,
            // указываем параметр specialty.IdFormOfStudy
            ViewData["MovieId"] = new SelectList(
                _context.Movies.
                OrderBy(f => f.Id),
                "Id", "MovieTitle", model.MovieId);

            return View(model);
        }

        // POST: Sessions/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(short id, EditSessionViewModel model)
        {
            var session = await _context.Sessions.FindAsync(id);

            if (id != session.Id)
            {
                return NotFound();
            }

            /*if (ModelState.IsValid)
            {*/
                try
                {
                    session.DateAndTime = model.DateAndTime;
                    session.Hall = model.Hall;
                    session.MovieId = model.MovieId;


                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
           /* }
            ViewData["MovieId"] = new SelectList(
                _context.Movies.
                OrderBy(f => f.Id),
                "Id", "MovieTitle", model.MovieId);

            return View(model);*/
        }

        // GET: Sessions/Delete/5
        public async Task<IActionResult> Delete(short? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(short id)
        {
            if (_context.Sessions == null)
            {
                return Problem("Entity set 'AppCtx.Sessions'  is null.");
            }
            var session = await _context.Sessions.FindAsync(id);
            if (session != null)
            {
                _context.Sessions.Remove(session);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Sessions/Details/5
        public async Task<IActionResult> Details(short? id)
        {
            if (id == null || _context.Sessions == null)
            {
                return NotFound();
            }

            var session = await _context.Sessions
                .Include(s => s.Movie)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        private bool SessionExists(short id)
        {
          return (_context.Sessions?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
