using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize] //-> Faz com que toda a controller necessite de autenticação para ser acessada.
    public class HomeController : Controller
    {
        [AllowAnonymous] //-> Permite sem que haja uma autenticação
        public IActionResult Index()
        {
            return View();
        }

        //[Authorize]
        public IActionResult Privacy()
        {
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

        [ClaimAuthorize("Home", "Secret")]
        public IActionResult ClaimCustom()
        {
            return View("SecretGravar");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
