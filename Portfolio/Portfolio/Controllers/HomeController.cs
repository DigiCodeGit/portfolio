using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Portfolio.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Viewbag cannot/won't be set inside constructor, can only be set after constructor is processed which is by
        // event executed. Hence, override executed event.
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            base.OnActionExecuted(context);

            // Note web section we're in (for Home nav link)
            ViewBag.controller = "home";
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}