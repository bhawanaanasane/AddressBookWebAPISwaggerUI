
namespace AddressBook.DataBase.Models
{
    public class AddressBooks : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int UserId { get; set; }

        public virtual User User {get;set;}
    }
}
