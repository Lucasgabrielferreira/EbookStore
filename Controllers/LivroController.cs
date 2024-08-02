using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EbookStore.Controllers
{
    public class LivroController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public LivroController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        // GET: Livro
        public IActionResult Index()
        {
            var livros = _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .ToList();
            return View(livros);
        }

        // GET: Livro/Details/5
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

        public IActionResult Create()
        {
            ViewBag.CategoriaId = new SelectList(_contexto.Categorias, "Id", "Nome");
            ViewBag.AutorId = new SelectList(_contexto.Autores, "Id", "Nome");
            return View(new Livro());
        }

        // POST: Livro/Create
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
