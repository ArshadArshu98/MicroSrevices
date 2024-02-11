using System.ComponentModel.DataAnnotations;

namespace TestService.Models
{
    public class UserData
    {
        [Key]
        public int Id { get; set; }
        public string URN { get; set; } 
        public string Username { get; set; }
        public string PhoneNumber { get; set; }
        public string EmailAddress { get; set; }
        public string Age { get; set; }
        public string UserAddress { get; set; }
        public string EncryptedBankAccountNumber { get; set; }
    }


}
