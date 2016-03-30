using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SampleApplication.Contacts
{
    public class UploadContactsService
    {
        private readonly UserContactsService _contactsService;

        public UploadContactsService()
        {
            _contactsService = new UserContactsService();
        }

        public bool ProcessFile(string fullFilePath, string username)
        {
            var contactsToCreate = new HashSet<UserContact>();

            using (var file = File.Open(fullFilePath, FileMode.Open, FileAccess.Read))
            using (var reader = new StreamReader(file, Encoding.UTF8))
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();

                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                var lineParts = line.Split(',');

                var contactName = lineParts[0].Trim('"').Trim();
                var phoneNumber = NumericOnly(lineParts[1]);

                if (IsValidContact(contactName, phoneNumber))
                {
                    contactsToCreate.Add(new UserContact
                    {
                        ContactName = contactName,
                        PhoneNumber = phoneNumber,
                        Username = username
                    });
                }
            }

            if (!contactsToCreate.Any())
            {
                return false;
            }

            foreach (var userContact in contactsToCreate)
            {
                _contactsService.Create(userContact);
            }

            return true;
        }

        private bool IsValidContact(string contactName, string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(contactName) || contactName.Length > 20)
            {
                return false;
            }

            if (string.IsNullOrEmpty(phoneNumber) || phoneNumber.Length > 25)
            {
                return false;
            }

            return true;
        }

        private static string NumericOnly(string input)
        {
            return Regex.Match(input, @"\d+").Value;
        }
    }
}