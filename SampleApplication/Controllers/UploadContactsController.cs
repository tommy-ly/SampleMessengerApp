using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
using SampleApplication.Contacts;

namespace SampleApplication.Controllers
{
    public class UploadContactsController : Controller
    {
        private readonly string _uploadLocation;
        private readonly UploadContactsService _uploadContactService;

        public UploadContactsController()
        {
            _uploadLocation = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "UploadedFiles");

            if (!Directory.Exists(_uploadLocation))
            {
                Directory.CreateDirectory(_uploadLocation);
            }

            _uploadContactService = new UploadContactsService();
        }

        [HttpGet]
        public ActionResult Index(ErrorCode? errorCode)
        {
            if (errorCode.HasValue)
            {
                switch (errorCode.Value)
                {
                    case ErrorCode.UnsupportedFileType:
                        ViewBag.ErrorMessage = "You can only upload text files";
                        break;

                    case ErrorCode.NoFileFound:
                        ViewBag.ErrorMessage = "Please select a file to upload";
                        break;

                    case ErrorCode.Unknown:
                        ViewBag.ErrorMessage = "Something went wrong, please try again";
                        break;
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Upload(HttpPostedFileBase file)
        {
            if (file == null)
            {
                return RedirectToAction("Index", new { errorCode = ErrorCode.NoFileFound });
            }

            if (file.ContentType != "text/plain")
            {
                return RedirectToAction("Index", new { errorCode = ErrorCode.UnsupportedFileType });
            }

            var fullFilePath = Path.Combine(_uploadLocation, file.FileName);
            file.SaveAs(fullFilePath);

            if (_uploadContactService.ProcessFile(fullFilePath, User.Identity.Name))
            {
                return RedirectToAction("List", "Contact", new { success = true });
            }

            return RedirectToAction("Index", new { errorCode = ErrorCode.Unknown });
        }

        public enum ErrorCode
        {
            UnsupportedFileType = 1,
            Unknown = 2,
            NoFileFound = 3
        }
    }
}