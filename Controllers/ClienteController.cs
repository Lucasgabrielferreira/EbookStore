using Microsoft.AspNetCore.Mvc;
using EbookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace EbookStore.Controllers
{
    public class ClienteController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public ClienteController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        // GET: Cliente
        public IActionResult Index()
        {
            var clientes = _contexto.Clientes.ToList();
            return View(clientes);
        }

        // GET: Cliente/Details/5
        public IActionResult Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // GET: Cliente/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Nome,Email,Senha")] Cliente cliente)
        {
            if (ModelState.IsValid)
            {
                _contexto.Add(cliente);
                _contexto.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(cliente);
        }

        // GET: Cliente/Edit/5
        public IActionResult Edit(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Clientes.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }
            return View(cliente);
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Guid id, [Bind("Id,Nome,Email,Senha")] Cliente cliente)
        {
            if (id != cliente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(cliente);
                    _contexto.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClienteExists(cliente.Id))
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
            return View(cliente);
        }

        // GET: Cliente/Delete/5
        public IActionResult Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cliente = _contexto.Clientes.FirstOrDefault(c => c.Id == id);

            if (cliente == null)
            {
                return NotFound();
            }

            return View(cliente);
        }

        // POST: Cliente/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var cliente = _contexto.Clientes.Find(id);
            _contexto.Clientes.Remove(cliente);
            _contexto.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ClienteExists(Guid id)
        {
            return _contexto.Clientes.Any(c => c.Id == id);
        }
    }
}
