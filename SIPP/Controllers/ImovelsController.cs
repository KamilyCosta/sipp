using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPP.Data;
using SIPP.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SIPP.Controllers
{
    public class ImovelsController : Controller
    {
        private readonly SIPPDbContext _context;

        public ImovelsController(SIPPDbContext context)
        {
            _context = context;
        }

        // GET: Imovels
        public async Task<IActionResult> Index()
        {
            List<Imovel> imoveis = await _context.Imoveis.Include(i => i.Imagens).ToListAsync();
          //  List<Imovel> imoveis2 = imoveis.FindAll(p => p.Endereco == "Teste10");
            return View(imoveis);
        }

        


        // GET: Imoveis/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.Imagens) // Inclui as imagens associadas
                .FirstOrDefaultAsync(i => i.ImovelId == id);

            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }




        public IActionResult Create()
        {
            var model = new Imovel();

            // Suponha que estas sejam as opções de método de pagamento
            ViewBag.MetodosPagamento = new List<SelectListItem>
        {
            new SelectListItem { Value = "Aceita financiamento", Text = "Aceita financiamento" },
            new SelectListItem { Value = "Não aceita financiamento", Text = "Não aceita financiamento" }
        };

            return View(model);
        }




        // POST: Imovels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImovelId,Endereco,Cidade,QntDormitorios,QntGarragem,Tamanho,TamanhoAreaContuida,MetodoPagamento, Aluguel, Venda,Valor")] Imovel imovel, List<IFormFile> imagens)
        {
            if (ModelState.IsValid)
            {
                imovel.ImovelId = Guid.NewGuid();

                // Adiciona o imóvel ao contexto
                _context.Add(imovel);
                await _context.SaveChangesAsync(); // Salva o imóvel para gerar o ID

                int cont = 1;

                // Processa cada imagem
                foreach (var imagem in imagens)
                {
                    if (imagem.Length > 0)
                    {
                        try
                        {
                            var fileName = Path.GetFileName(imagem.FileName);

                            string extension = Path.GetExtension(fileName);

                            string dateNow = DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss.fff");

                            string newName = $"{imovel.ImovelId}_{dateNow}{extension}";
                            
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", newName);

                            // Salva a imagem no sistema de arquivos
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imagem.CopyToAsync(stream);
                            }

                            // Cria uma nova entrada na tabela de imagens
                            var novaImagem = new Imagem
                            {
                                ImagemId = Guid.NewGuid(),
                                ImovelId = imovel.ImovelId, // Relaciona a imagem ao imóvel
                                Url = $"/img/{newName}" // URL da imagem
                            };

                            // Adiciona a imagem ao contexto
                            _context.Imagens.Add(novaImagem);
                        }
                        catch (Exception ex)
                        {
                            ModelState.AddModelError("", $"Erro ao salvar a imagem: {ex.Message}");
                        }
                    }
                }

                // Salva as imagens no banco de dados
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(imovel);
        }


        // GET: Imovels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Imoveis == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
              //  .Include(i => i.Imagens) 
                .FirstOrDefaultAsync(i => i.ImovelId == id);

            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovels/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ImovelId,Endereco,Cidade,QntDormitorios,QntGarragem,Tamanho,TamanhoAreaContuida,MetodoPagamento,Aluguel,Venda,Valor")] Imovel imovel, List<IFormFile> novasImagens, List<Guid> removerImagens)
        {
            if (id != imovel.ImovelId)
            {
                return NotFound();
            }

            // Verifica se o ModelState é válido
            if (!ModelState.IsValid)
            {
                // Logar erros
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(imovel); // Retorna a view com o modelo para correção
            }

            try
            {
                // Atualiza o imóvel no contexto
                _context.Update(imovel);

                // Remove as imagens selecionadas para remoção
                if (removerImagens != null && removerImagens.Any())
                {
                    foreach (var imagemId in removerImagens)
                    {
                        var imagem = await _context.Imagens.FindAsync(imagemId);
                        if (imagem != null)
                        {
                            // Remove o arquivo do sistema de arquivos
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagem.Url.TrimStart('/'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

                            // Remove a imagem do banco de dados
                            _context.Imagens.Remove(imagem);
                        }
                    }
                }

                // Processa e adiciona novas imagens
                if (novasImagens != null && novasImagens.Any())
                {
                    foreach (var imagem in novasImagens)
                    {
                        if (imagem.Length > 0)
                        {
                            var fileName = Path.GetFileName(imagem.FileName);
                            string extension = Path.GetExtension(fileName);
                            string dateNow = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                            string newName = $"{imovel.ImovelId}_{dateNow}{extension}";

                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", newName);

                            // Salva a imagem no sistema de arquivos
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await imagem.CopyToAsync(stream);
                            }

                            // Adiciona a nova imagem ao banco de dados
                            var novaImagem = new Imagem
                            {
                                ImagemId = Guid.NewGuid(),
                                ImovelId = imovel.ImovelId,
                                Url = $"/img/{newName}"
                            };
                            _context.Imagens.Add(novaImagem);
                        }
                    }
                }

                // Salva as alterações no banco de dados
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImovelExists(imovel.ImovelId))
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




        // GET: Imovels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .FirstOrDefaultAsync(m => m.ImovelId == id);
            if (imovel == null)
            {
                return NotFound();
            }

            return View(imovel);
        }

        // POST: Imovels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var imovel = await _context.Imoveis.FindAsync(id);
            if (imovel != null)
            {
                _context.Imoveis.Remove(imovel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ImovelExists(Guid id)
        {
            return _context.Imoveis.Any(e => e.ImovelId == id);
        }
    }
}
