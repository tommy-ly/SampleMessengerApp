using System.Web;
using System.Web.Mvc;
using SampleApplication.Contacts;
using SampleApplication.Messaging;

namespace SampleApplication.Controllers
{
    [Authorize]
    public class SendToContactController : Controller
    {
        private readonly UserContactsService _contactService;
        private readonly SendMessageService _sendMessageService;

        public SendToContactController()
        {
            _contactService = new UserContactsService();
            _sendMessageService = new SendMessageService();
        }

        [HttpGet]
        public ActionResult Send(int id)
        {
            var contact = _contactService.Get(User.Identity.Name, id);

            if (contact == null)
            {
                throw new HttpException(404, "Contact was not found for user");
            }

            return View("Send", contact);
        }

        [HttpPost]
        public ActionResult Send(int id, string body)
        {
            var contact = _contactService.Get(User.Identity.Name, id);

            if (contact == null)
            {
                throw new HttpException(404, "Contact was not found for user");
            }

            _sendMessageService.SendMessage(User.Identity.Name, contact.PhoneNumber, body);
            _contactService.UpdateLastSentTime(User.Identity.Name, id);

            return RedirectToAction("List", "Contact");
        }
    }
}