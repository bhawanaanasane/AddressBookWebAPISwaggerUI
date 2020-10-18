

using System.Collections;
using System.Collections.Generic;

namespace AddressBook.DataBase.Models
{
    public class User : BaseEntity
    {
        private  ICollection<AddressBooks> _addressBook;
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
        public virtual ICollection<AddressBooks> AddressBook
        {
            get { return _addressBook ?? (_addressBook = new List<AddressBooks>()); }
            protected set { _addressBook = value; }
        }

    }

    public enum UserRole
    {
        
        Admin = 1,
        NormalUser = 2
    }

}
