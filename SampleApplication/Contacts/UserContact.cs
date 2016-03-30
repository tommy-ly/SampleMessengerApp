using System;

namespace SampleApplication.Contacts
{
    public class UserContact
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string ContactName { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? LastSentTo { get; set; }
    }
}