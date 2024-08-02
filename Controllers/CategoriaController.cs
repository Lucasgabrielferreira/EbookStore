using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using EbookStore.Models; // Certifique-se de ajustar o namespace conforme a estrutura do seu projeto

namespace EbookStore.Controllers
{
    public class CategoriaController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public CategoriaController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        // GET: Categoria
        public IActionResult Index()
        {
            var categorias = _contexto.Categorias.ToList();
            return View(categorias);
        }

        // GET: Categoria/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = _contexto.Categorias
                .FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // GET: Categoria/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("CategoriaId,Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(categoria);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(categoria);
        }

        // GET: Categoria/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = _contexto.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }

        // POST: Categoria/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Nome")] Categoria categoria)
        {
            if (id != categoria.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(categoria);
                    _contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoriaExists(categoria.Id))
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
            return View(categoria);
        }

        // GET: Categoria/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var categoria = _contexto.Categorias
                .FirstOrDefault(c => c.Id == id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var categoria = _contexto.Categorias.Find(id);
            _contexto.Categorias.Remove(categoria);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoriaExists(Guid id)
        {
            return _contexto.Categorias.Any(e => e.Id == id);
        }
    }
}
