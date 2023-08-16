using Microsoft.AspNetCore.Mvc;

namespace FYP.Web.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult Error()
        {
            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            ViewBag.ErrorMessage = "An error occurred while processing your request.";

            return View();
        }

        public IActionResult NotFound()
        {
            var exceptionHandlerPathFeature =
            HttpContext.Features.Get<Microsoft.AspNetCore.Diagnostics.IExceptionHandlerPathFeature>();

            ViewBag.ErrorMessage = "The Request you ar looking for is not Found";

            return View();
        }
    }
}
