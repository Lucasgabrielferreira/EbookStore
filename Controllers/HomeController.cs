using EbookStore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EbookStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ContextoEbookStore _contexto;

        public HomeController(ContextoEbookStore contexto)
        {
            _contexto = contexto;
        }

        public IActionResult Index()
        {
            var categoriasComLivros = _contexto.Categorias
                .Include(c => c.Livros)
                .ThenInclude(l => l.Autor)
                .ToList();

            return View(categoriasComLivros);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
