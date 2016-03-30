using System.Web;
using System.Web.Mvc;
using SampleApplication.Contacts;
using SampleApplication.Models;

namespace SampleApplication.Controllers
{
    [Authorize]
    public class ContactController : Controller
    {
        private readonly UserContactsService _contactsService;

        public ContactController()
        {
            _contactsService = new UserContactsService();
        }

        [HttpGet]
        public ActionResult List()
        {
            var userContacts = _contactsService.GetAll(User.Identity.Name);
            return View("List", userContacts);
        }
        
        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost]
        public ActionResult Create(UpdateContactRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException(400, "Invalid");
            }

            var contact = new UserContact
            {
                Username = User.Identity.Name,
                ContactName = model.ContactName,
                PhoneNumber = model.PhoneNumber
            };

            _contactsService.Create(contact);

            return RedirectToAction("List");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var toEdit = _contactsService.Get(User.Identity.Name, id);

            if (toEdit == null)
            {
                throw new HttpException(404, "Contact not found");
            }

            var viewModel = new UpdateContactRequestModel
            {
                ContactName = toEdit.ContactName,
                PhoneNumber = toEdit.PhoneNumber,
                Id = toEdit.Id
            };

            return View("Edit", viewModel);
        }

        [HttpPost]
        public ActionResult Edit(UpdateContactRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpException(400, "Invalid");
            }

            var contact = new UserContact
            {
                Id = model.Id,
                Username = User.Identity.Name,
                ContactName = model.ContactName,
                PhoneNumber = model.PhoneNumber
            };

            _contactsService.Update(contact);

            return RedirectToAction("List");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            _contactsService.Delete(User.Identity.Name, id);
            return RedirectToAction("List");
        }
    }
}