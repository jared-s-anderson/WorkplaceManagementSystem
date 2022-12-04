using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WorkplaceManagementSystem.Controllers
{
    public class FeedbackController : Controller
    {

        [HttpGet]

        // Administrators and employees are granted access to view the feedback index view.
        [Authorize(Roles ="Administrator, Employee")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
