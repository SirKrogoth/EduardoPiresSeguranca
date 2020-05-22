using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using KissLog;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize] //-> Faz com que toda a controller necessite de autenticação para ser acessada.
    public class HomeController : Controller
    {
        private readonly ILogger _logger;

        public HomeController(ILogger logger)
        {
            _logger = logger;
        }

        [AllowAnonymous] //-> Permite sem que haja uma autenticação
        public IActionResult Index()
        {
            _logger.Trace("Usuário acesso a home page");
            return View();
        }

        //[Authorize]
        public IActionResult Privacy()
        {
            throw new Exception("HOuve um erro aqui");
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretCleam()
        {
            return View("Secret");
        }

        [Authorize(Policy = "PodeEscrever")]
        public IActionResult SecretCleamGravar()
        {
            return View("SecretGravar");
        }

        //[ClaimAuthorize("Home", "Secret")]
        public IActionResult ClaimCustom()
        {
            return View("SecretGravar");
        }
        
        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var modelErro = new ErrorViewModel();

            if(id == 500)
            {
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate nosso suporte técnico.";
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.ErroCode = id;
            }
            else if(id == 400)
            {
                modelErro.Mensagem = "A página que está procurando não existe. <br /> Em caso de dúvidas entrar em contato com nosso suporte.";
                modelErro.Titulo = "Ops! Página não encontrada!";
                modelErro.ErroCode = id;
            }
            else if(id == 403)
            {
                modelErro.Mensagem = "Você não tem permissão para fazer isso.";
                modelErro.Titulo = "Acesso Negado!";
                modelErro.ErroCode = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", modelErro);
        }
    }
}
