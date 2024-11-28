using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SIPP.Data;
using SIPP.Models;

namespace SIPP.Controllers
{
    public class ImovelsController : Controller
    {
        private readonly SIPPDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ImovelsController(SIPPDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private async Task<bool> IsAdminAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var pessoaLogada = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);
            return pessoaLogada != null && pessoaLogada.TipoPessoaId == Guid.Parse("799237FF-605B-4614-BBA8-6DA107B3FFE4");  // TipoPessoaId do Admin
        }

        // GET: Imovels
        public async Task<IActionResult> Index()
        {
            List<Imovel> imoveis = await _context.Imoveis.Include(i => i.Imagens).ToListAsync();

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var pessoaLogada = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

            var isAdmin = await IsAdminAsync(); // Verifica se o usuário logado é admin

            
            ViewData["IsAdmin"] = isAdmin; // Passa essa informação para a view

            return View(imoveis);
        }

        // GET: Imovels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var imovel = await _context.Imoveis
                .Include(i => i.Imagens)
                .FirstOrDefaultAsync(i => i.ImovelId == id);

            if (imovel == null)
            {
                return NotFound();
            }

            // Verifica se o usuário está logado
            if (!User.Identity.IsAuthenticated)
            {
                return Redirect("/Identity/Account/Register");
           }

            var userId = _userManager.GetUserId(User);
            var pessoa = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

           if (pessoa == null)
           {
               return RedirectToAction("Create", "Pessoas");
            }

           ViewData["PessoaExistente"] = true;


            ViewData["ImovelId"] = imovel.ImovelId;

            return View(imovel);
        }

        // GET: Imovels/Create
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ImovelId,Endereco,Cidade,QntDormitorios,QntGarragem,Tamanho,TamanhoAreaContuida,MetodoPagamento,Aluguel,Venda,Valor")] Imovel imovel, List<IFormFile> imagens)
        {
            if (ModelState.IsValid)
            {
                var userId = _userManager.GetUserId(User);
                var pessoa = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

                if (pessoa == null)
                {
                    return RedirectToAction("Create", "Pessoas");
                }

                imovel.ImovelId = Guid.NewGuid();

                // Adiciona o imóvel ao contexto
                _context.Add(imovel);
                await _context.SaveChangesAsync(); // Salva o imóvel para gerar o ID

                // Salva as imagens associadas ao imóvel
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
        .Include(i => i.Imagens) // Garante que as imagens serão carregadas com o imóvel
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
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", imagem.Url.TrimStart('/'));
                            if (System.IO.File.Exists(filePath))
                            {
                                System.IO.File.Delete(filePath);
                            }

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
