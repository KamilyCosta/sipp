
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SIPP.Data;
using SIPP.Models;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace SIPP.Controllers
{
    public class PessoasController : Controller
    {
        private readonly SIPPDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public PessoasController(SIPPDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // Verifica se o usuário logado é admin
        private async Task<bool> IsAdminAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var pessoaLogada = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);
            return pessoaLogada != null && pessoaLogada.TipoPessoaId == Guid.Parse("799237FF-605B-4614-BBA8-6DA107B3FFE4");  // TipoPessoaId do Admin
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Obtém o ID do usuário logado
            var pessoaLogada = await _context.Pessoa.FirstOrDefaultAsync(p => p.UserId == userId);

            var isAdmin = await IsAdminAsync(); // Verifica se o usuário logado é admin

            var corretorTipoId = Guid.Parse("A83D62DD-7112-4B7A-9CB0-134AD4ACF74C");  // Tipo de corretor

            var corretores = await _context.Pessoa
                                           .Where(p => p.TipoPessoaId == corretorTipoId)
                                           .ToListAsync();

            ViewData["IsAdmin"] = isAdmin;

            return View(corretores);
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public async Task<IActionResult> Create()
        {
            // Verifica se o usuário é admin antes de permitir criar
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["AspNetUserId"] = userId;
            return View();
        }

        // POST: Pessoas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,Nome,CPF,DataNascimento,CEP,Bairro,Cidade,Rua,Complemento,Numero,Telefone,CRECI,UrlImagem,DataCadastro,UserId")] Pessoa pessoa, IFormFile? imagemPerfil)
        {
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            if (ModelState.IsValid)
            {
                // Processa a imagem de perfil, se houver
                if (imagemPerfil != null && imagemPerfil.Length > 0)
                {
                    try
                    {
                        var fileName = Path.GetFileName(imagemPerfil.FileName);
                        string extension = Path.GetExtension(fileName);
                        string dateNow = DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss.fff");
                        string newName = $"{Guid.NewGuid()}_{dateNow}{extension}";

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", newName);

                        // Salva a imagem no sistema de arquivos
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagemPerfil.CopyToAsync(stream);
                        }

                        // Define a URL da imagem de perfil
                        pessoa.UrlImagem = $"/img/{newName}";
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Erro ao salvar a imagem: {ex.Message}");
                    }
                }

                pessoa.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Define o tipo de pessoa com base em uma lógica (aqui está fixo como "Cliente" para exemplo)
                var tipoCliente = await _context.TipoPessoas.FirstOrDefaultAsync(tp => tp.Descricao == "Cliente");
                if (tipoCliente != null)
                {
                    pessoa.TipoPessoaId = tipoCliente.TipoPessoaId;  // Associa o tipo de cliente
                }

                pessoa.DataCadastro = DateOnly.FromDateTime(DateTime.Now);

                _context.Add(pessoa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            // Verifica se o usuário é admin antes de permitir editar
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PessoaId,Nome,CPF,DataNascimento,CEP,Bairro,Cidade,Rua,Complemento,Numero,Telefone,CRECI,UrlImagem,DataCadastro,UserId")] Pessoa pessoa)
        {
            // Verifica se o usuário é admin antes de permitir editar
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            if (id != pessoa.PessoaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaId))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            // Verifica se o usuário é admin antes de permitir deletar
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaId == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            // Verifica se o usuário é admin antes de permitir deletar
            if (!await IsAdminAsync())
            {
                return Forbid(); // Retorna uma resposta 403 caso o usuário não seja admin
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // CREATE CORRETOR 
        [HttpGet]
        public IActionResult CreateCorretor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCorretor([Bind("PessoaId,Nome,CPF,DataNascimento,CEP,Bairro,Cidade,Rua,Complemento,Numero,Telefone,CRECI,UrlImagem,Email,Senha,DataCadastro")] Pessoa pessoa, IFormFile imagemPerfil)
        {
            if (ModelState.IsValid)
            {

                // Cria o usuário
                IdentityUser identityUser = await criarUsuarioDeAcesso(pessoa.Email, pessoa.Senha);

                pessoa.UserId = identityUser.Id;

                // Processa a imagem de perfil, se houver
                if (imagemPerfil != null && imagemPerfil.Length > 0)
                {
                    try
                    {
                        var fileName = Path.GetFileName(imagemPerfil.FileName);
                        string extension = Path.GetExtension(fileName);
                        string dateNow = DateTime.Now.ToString("yyyy-MM-ddHH-mm-ss.fff");
                        string newName = $"{Guid.NewGuid()}_{dateNow}{extension}";

                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", newName);

                        // Salva a imagem no sistema de arquivos
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await imagemPerfil.CopyToAsync(stream);
                        }

                        // Define a URL da imagem de perfil
                        pessoa.UrlImagem = $"/img/{newName}";
                    }
                    catch (Exception ex)
                    {
                        ModelState.AddModelError("", $"Erro ao salvar a imagem: {ex.Message}");
                    }
                }

                // Define a pessoa como corretor
                var tipoCorretor = await _context.TipoPessoas.FirstOrDefaultAsync(tp => tp.Descricao == "Corretor");
                if (tipoCorretor != null)
                {
                    pessoa.TipoPessoaId = tipoCorretor.TipoPessoaId;
                }

                pessoa.DataCadastro = DateOnly.FromDateTime(DateTime.Now);
                _context.Add(pessoa);
                await _context.SaveChangesAsync();



                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
        }



        private async Task<IdentityUser> criarUsuarioDeAcesso(string? email, string? senha)
        {
            // TODO: Implementar a lógica para tratar erros.

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, senha);


            if (result.Succeeded)
            {
                // Usuário criado com sucesso
                // return RedirectToAction("Index", "Home");
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return user;
        }


        private bool PessoaExists(Guid id)
        {
            return _context.Pessoa.Any(e => e.PessoaId == id);
        }
    }
}
