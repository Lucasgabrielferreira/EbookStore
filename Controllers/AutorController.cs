using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;
using Microsoft.AspNetCore.Authorization;
using EbookStore.Extensions;
using X.PagedList.Extensions;


namespace EbookStore.Controllers
{
    [Authorize]
    public class AutorController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public AutorController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }
        // GET: Autor
        [AllowAnonymous]
        public IActionResult Index(string searchString, int page = 1)
        {
            // Filtra os autores com base na string de busca
            var autoresQuery = _contexto.Autores.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                autoresQuery = autoresQuery.Where(a => a.Nome.Contains(searchString) || a.Biografia.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            // Pagina os resultados
            int pageSize = 10; // Número de autores por página
            var autores = autoresQuery.ToPagedList(page, pageSize);

            return View(autores);
        }

        // GET: Autor/Details/5
        [AllowAnonymous]
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
        [ClaimsAuthorize("ADM", "AD")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Autor/Create
        [ClaimsAuthorize("ADM", "AD")]
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
        [ClaimsAuthorize("ADM", "ED")]
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
        [ClaimsAuthorize("ADM", "ED")]
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
        [ClaimsAuthorize("ADM", "EX")]
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
        [ClaimsAuthorize("ADM", "EX")]
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
