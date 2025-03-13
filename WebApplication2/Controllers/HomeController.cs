using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var errorModel = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                StatusCode = statusCode
            };

            if (statusCode == 404)
            {
                errorModel.ErrorMessage = "Страница не найдена.";
                _logger.LogWarning("HomeController: Обнаружена ошибка 404.");
            }
            else
            {
                var exceptionHandlerPathFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
                errorModel.ErrorMessage = exceptionHandlerPathFeature?.Error.Message ?? "Произошла ошибка.";
                _logger.LogError("HomeController: Произошло исключение - {ErrorMessage}", errorModel.ErrorMessage);
            }

            return View(errorModel);
        }
    }
}
