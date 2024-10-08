﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EbookStore.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using EbookStore.Extensions;
using X.PagedList;
using System.IO;
using System.Linq;
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
        public async Task<IActionResult> Index(string searchString, int? page, int? pageSize)
        {
            // Valor padrão para o tamanho da página
            int defaultPageSize = 10;

            // Define o tamanho da página com base na escolha do usuário ou usa o valor padrão
            int currentPageSize = pageSize ?? defaultPageSize;

            // Salva o tamanho da página na ViewData para que a seleção seja mantida
            ViewData["CurrentPageSize"] = currentPageSize;

            // Número da página atual
            int pageNumber = (page ?? 1);

            var livros = _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .AsQueryable();

            // Filtro de pesquisa
            if (!string.IsNullOrEmpty(searchString))
            {
                livros = livros.Where(l => l.Titulo.Contains(searchString) || l.Descricao.Contains(searchString));
            }

            var pagedLivros = livros.ToPagedList(pageNumber, currentPageSize);

            ViewData["CurrentFilter"] = searchString;

            return View(pagedLivros);
        }

        // GET: Livro/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var livro = await _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

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

        [ClaimsAuthorize("ADM", "AD")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,ImagemUpload,Descricao,Preco,AutorId,CategoriaId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(livro.ImagemUpload, imgPrefixo))
                {
                    return View(livro);
                }

                livro.Imagem = imgPrefixo + livro.ImagemUpload.FileName;

                _contexto.Add(livro);
                await _contexto.SaveChangesAsync(); // Certifique-se de que esta linha está sendo executada
                return RedirectToAction(nameof(Index));
            }
            ViewBag.CategoriaId = new SelectList(_contexto.Categorias, "Id", "Nome", livro.CategoriaId);
            ViewBag.AutorId = new SelectList(_contexto.Autores, "Id", "Nome", livro.AutorId);
            return View(livro);
        }

        // GET: Livro/Edit/5
        [ClaimsAuthorize("ADM", "ED")]
        public async Task<IActionResult> Edit(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var livro = await _contexto.Livros.FindAsync(id);

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
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,Titulo,ImagemUpload,Descricao,Preco,AutorId,CategoriaId")] Livro livro)
        {
            if (id != livro.Id)
            {
                return NotFound();
            }

            var livroDb = await _contexto.Livros.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

            if (livroDb == null)
            {
                return NotFound();
            }

            if (livro.ImagemUpload == null)
            {
                // Mantenha a imagem existente se nenhuma nova for carregada
                livro.Imagem = livroDb.Imagem;
            }
            else
            {
                var imgPrefixo = Guid.NewGuid() + "_";
                if (!await UploadArquivo(livro.ImagemUpload, imgPrefixo))
                {
                    // Se ocorrer um erro durante o upload, retorne o modelo para a view com a imagem atual
                    livro.Imagem = livroDb.Imagem;
                    return View(livro);
                }

                // Atualize o nome da imagem se o upload for bem-sucedido
                livro.Imagem = imgPrefixo + livro.ImagemUpload.FileName;
            }

            // Remova a validação obrigatória da imagem se for uma edição
            ModelState.Remove("ImagemUpload");

            if (ModelState.IsValid)
            {
                try
                {
                    _contexto.Update(livro);
                    await _contexto.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await LivroExists(livro.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.Autores = new SelectList(_contexto.Autores, "Id", "Nome", livro.AutorId);
            ViewBag.Categorias = new SelectList(_contexto.Categorias, "Id", "Nome", livro.CategoriaId);
            return View(livro);
        }

        // GET: Livro/Delete/5
        [ClaimsAuthorize("ADM", "EX")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return NotFound();
            }

            var livro = await _contexto.Livros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(l => l.Id == id);

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
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var livro = await _contexto.Livros.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            _contexto.Livros.Remove(livro);
            await _contexto.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        private async Task<bool> LivroExists(Guid id)
        {
            return await _contexto.Livros.AnyAsync(e => e.Id == id);
        }

        private async Task<bool> UploadArquivo(IFormFile arquivo, string imgPrefixo)
        {
            if (arquivo.Length <= 0) return false;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", imgPrefixo + arquivo.FileName);

            if (System.IO.File.Exists(path))
            {
                ModelState.AddModelError(string.Empty, "Já existe um arquivo com este nome!");
                return false;
            }

            using (var stream = new FileStream(path, FileMode.Create))
            {
                await arquivo.CopyToAsync(stream);
            }

            return true;
        }
    }
}
