using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;

namespace EbookStore.Controllers
{
    public class AutorController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public AutorController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }
        // GET: Autor
        public IActionResult Index()
        {
            var autores = _contexto.Autores.ToList();
            return View(autores);
        }

        // GET: Autor/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = _contexto.Autores
               .FirstOrDefault(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // GET: Autor/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome,Biografia")] Autor autor)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(autor);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(autor);
        }

        // GET: Autor/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = _contexto.Autores.Find(id);

            if (autor == null)
            {
                return NotFound();
            }
            return View(autor);
        }

        // POST: Autor/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Nome,Biografia")] Autor autor)
        {
            if (id != autor.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(autor);
                    _contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AutorExists(autor.Id))
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
            return View(autor);
        }

        private bool AutorExists(Guid id)
        {
            return _contexto.Autores.Any(e => e.Id == id);
        }

        // GET: Autor/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autor = _contexto.Autores
                .FirstOrDefault(a => a.Id == id);

            if (autor == null)
            {
                return NotFound();
            }

            return View(autor);
        }

        // POST: Autor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var autor = _contexto.Autores.Find(id);
            _contexto.Autores.Remove(autor);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
