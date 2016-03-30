using System.Linq;
using System.Web.Mvc;
using SampleApplication.Messaging;
using SampleApplication.Models;

namespace SampleApplication.Controllers
{
    public class MessengerController : Controller
    {
        private readonly SendMessageService _sendMessageService;

        public MessengerController()
        {
            _sendMessageService = new SendMessageService();
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View("Index", new MessengerViewModel());
        }

        [HttpPost]
        public ActionResult SendMessage(MessengerViewModel model)
        {
            if (model.To.Length > 25 || !IsNumeric(model.To))
            {
                return RedirectToAction("Index", new { ErrorMessage = "Please enter a valid phone number" });
            }

            if (model.Body.Length > 612)
            {
                return RedirectToAction("Index", new { ErrorMessage = "Body length can only be 612 characters" });
            }

            _sendMessageService.SendMessage(User.Identity.Name, model.To, model.Body);

            return RedirectToAction("Index", new {SuccessMessage = "Message sent successfully"});
        }

        private bool IsNumeric(string input)
        {
            return input.All(char.IsDigit);
        }
    }
}