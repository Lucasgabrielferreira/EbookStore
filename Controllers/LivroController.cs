using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using EbookStore.Extensions;
using X.PagedList;
using X.PagedList.Extensions;

namespace EbookStore.Controllers
{
    [Authorize]
    public class LivroController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public LivroController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        // GET: Livro
        [AllowAnonymous]
        public async Task<IActionResult> Index(string searchString, int? page)
        {
            int pageSize = 10; // Define o tamanho da página
            int pageNumber = (page ?? 1); // Define o número da página atual

            var livros = _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .AsQueryable();

            if (!string.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(l => l.Titulo.Contains(searchString) || l.Descricao.Contains(searchString));
            }

            var pagedLivros = livros.ToPagedList(pageNumber, pageSize);

            ViewData["CurrentFilter"] = searchString;

            return View(pagedLivros);
        }

        // GET: Livro/Details/5
        [AllowAnonymous]
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        [ClaimsAuthorize("ADM", "AD")]
        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_contexto.Categorias, "Id", "Nome");
            ViewBag.AutorId = new SelectList(_contexto.Autores, "Id", "Nome");
            return View(new Livro());
        }

        // POST: Livro/Create
        [ClaimsAuthorize("ADM", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Titulo,Descricao,Preco,AutorId,CategoriaId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(livro);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaId = new SelectList(_contexto.Categorias, "Id", "Nome", livro.CategoriaId);
            ViewBag.AutorId = new SelectList(_contexto.Autores, "Id", "Nome", livro.AutorId);
            return View(livro);
        }

        // GET: Livro/Edit/5
        [ClaimsAuthorize("ADM", "ED")]
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _contexto.Livros.Find(id);

            if (livro == null)
            {
                return NotFound();
            }

            ViewBag.Autores = new SelectList(_contexto.Autores, "Id", "Nome", livro.AutorId);
            ViewBag.Categorias = new SelectList(_contexto.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // POST: Livro/Edit/5
        [ClaimsAuthorize("ADM", "ED")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Titulo,Descricao,Preco,AutorId,CategoriaId")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(livro);
                    _contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.Id))
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
            ViewBag.Autores = new SelectList(_contexto.Autores, "Id", "Nome", livro.AutorId);
            ViewBag.Categorias = new SelectList(_contexto.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // GET: Livro/Delete/5
        [ClaimsAuthorize("ADM", "EX")]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var livro = _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefault(l => l.Id == id);

            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livro/Delete/5
        [ClaimsAuthorize("ADM", "EX")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var livro = _contexto.Livros.Find(id);
            _contexto.Livros.Remove(livro);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(Guid id)
        {
            return _contexto.Livros.Any(e => e.Id == id);
        }
    }
}
