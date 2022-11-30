using Microsoft.AspNetCore.Mvc;

namespace WorkplaceManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
