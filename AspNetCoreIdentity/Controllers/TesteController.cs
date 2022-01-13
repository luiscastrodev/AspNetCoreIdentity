using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace AspNetCoreIdentity.Controllers
{
    public class TesteController : Controller
    {
        private readonly ILogger<TesteController> _logger;

        public TesteController(ILogger<TesteController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            //_logger.LogError("esse erro aconteuceu");

            //throw new Exception("Eitaa aconteceu um erro");
            return View();
        }
    }
}
