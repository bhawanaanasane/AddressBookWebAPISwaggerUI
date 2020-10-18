using AddressBook.DataBase.Models;
using AddressBook.ViewModel.AddressBookModels;
using System.Collections.Generic;

namespace AddressBook.ViewModel
{
    public class UserModel :BaseEntityViewModel
    {
        public UserModel()
        {
            AddressBook = new List<AddressBookModel>();
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public UserRole UserRole
        {
            get { return (UserRole)UserRoleId; }
            set
            {
                this.UserRoleId = (int)value;
            }
        }
        public List<AddressBookModel> AddressBook { get; set; }
        public string Token { get; set; }
    }
}
