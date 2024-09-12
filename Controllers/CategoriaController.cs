using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;
using Microsoft.AspNetCore.Authorization;
using EbookStore.Extensions;
using X.PagedList.Extensions; // Certifique-se de ajustar o namespace conforme a estrutura do seu projeto

namespace EbookStore.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public CategoriaController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        // GET: Categoria
        [AllowAnonymous]
        public IActionResult Index(string searchString, int page = 1)
        {
            // Filtra as categorias com base na string de busca
            var categoriasQuery = _contexto.Categorias.AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                categoriasQuery = categoriasQuery.Where(c => c.Nome.Contains(searchString));
                ViewData["CurrentFilter"] = searchString;
            }

            // Pagina os resultados
            int pageSize = 10; // Número de categorias por página
            var categorias = categoriasQuery.ToPagedList(page, pageSize);

            return View(categorias);
        }

        // GET: Categoria/Details/5
        [AllowAnonymous]
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

        // GET: Categoria/Create]
        [ClaimsAuthorize("ADM", "AD")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        [ClaimsAuthorize("ADM", "AD")]
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

        [HttpPost]
        public async Task<IActionResult> CreateAjax([Bind("Nome")] Categoria categoria)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(categoria);
                await _contexto.SaveChangesAsync();
                return Json(new { success = true, categoriaId = categoria.Id, categoriaNome = categoria.Nome });
            }
            return Json(new { success = false });
        }

        // GET: Categoria/Edit/5
        [ClaimsAuthorize("ADM", "ED")]
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
        [ClaimsAuthorize("ADM", "ED")]
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
        [ClaimsAuthorize("ADM", "EX")]
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
        [ClaimsAuthorize("ADM", "EX")]
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
