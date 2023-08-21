using System.ComponentModel.DataAnnotations;

namespace CsvProject3.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public string UserIdentifier { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
