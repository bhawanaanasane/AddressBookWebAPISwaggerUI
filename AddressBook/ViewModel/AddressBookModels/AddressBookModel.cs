
namespace AddressBook.ViewModel.AddressBookModels
{

    public class AddressBookModel :BaseEntityViewModel
    {
        public AddressBookModel()
        {
            //User = new UserModel();
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public int UserId { get; set; }

        //public virtual UserModel User { get; set; }
    }
}
