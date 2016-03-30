using System.ComponentModel.DataAnnotations;

namespace SampleApplication.Models
{
    public class MessengerViewModel
    {
        [Required]
        [MaxLength(20, ErrorMessage = "Max message length is 612 characters")]
        public string To { get; set; }

        [Required]
        [MaxLength(612, ErrorMessage = "Max message length is 612 characters")]
        public string Body { get; set; }
    }
}