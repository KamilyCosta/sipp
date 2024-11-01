using System;
using System.Collections.Generic;
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
    public class PessoasController : Controller
    {
        private readonly SIPPDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public PessoasController(SIPPDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;

        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
            var sIPPDbContext = _context.Users;
            return View(await sIPPDbContext.ToListAsync());
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
        public IActionResult Create()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["AspNetUserId"] = userId;
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaId,Nome,CPF,DataNascimento,CEP,Bairro,Cidade,Rua,Complemento,Numero,Telefone,CRECI,UrlImagem,DataCadastro,UserId")] Pessoa pessoa, IFormFile imagemPerfil)
        {
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


                var tipoCliente = await _context.TipoPessoas.FirstOrDefaultAsync(tp => tp.Descricao == "Cliente");
                if (tipoCliente != null)
                {
                    pessoa.TipoPessoaId = tipoCliente.TipoPessoaId; 
                }


                pessoa.DataCadastro = DateOnly.FromDateTime(DateTime.Now);

               
                _context.Add(pessoa);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(pessoa);
        }



        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                ViewData["AspNetUserId"] = id;
                //return RedirectToAction("Create");
                return View("Create");
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("PessoaId,Nome,CPF,DataNascimento,CEP,Bairro,Cidade,Rua,Complemento,Numero,Telefone,CRECI,UrlImagem,DataCadastro,UserId")] Pessoa pessoa)
        {
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
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(Guid id)
        {
            return _context.Pessoa.Any(e => e.PessoaId == id);
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
    }
}
