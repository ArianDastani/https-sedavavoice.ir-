using System.ComponentModel.DataAnnotations;


namespace Domain.Models
{
    public class Informations
    {

        [Key]
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? TelegramChannel { get; set; }
        public string? Instagram { get; set; }
        public string? Address { get; set; }
        public string? Experience { get; set; }
        public string? ViewCount { get; set; }
        public string? ProfileImage { get; set; }
        public string? AboutMe { get; set; }


    }
}
