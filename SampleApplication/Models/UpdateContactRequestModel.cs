using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Models
{
    public class UpdateContactRequestModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        [RegularExpression(@"^[a-zA-Z0-9_\s]*$", ErrorMessage = "Contact name can only contain A-Z, 0-9, _ or spaces")]
        public string ContactName { get; set; }

        [Required]
        [MaxLength(25)]
        public string PhoneNumber { get; set; }
    }
}