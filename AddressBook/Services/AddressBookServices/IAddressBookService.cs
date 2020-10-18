using AddressBook.DataBase.Models;

using AddressBook.ViewModel.AddressBookModels;
using System.Collections.Generic;

namespace AddressBook.Services.UserServices
{
   public interface IAddressBookService
    {
        void CreateAddressBook(AddressBooks model);
        void UpdateAddressBook(AddressBooks model);
        void DeleteAddressBook(AddressBooks model);

        AddressBooks GetAddressBookById(int Id);
        List<AddressBooks> GetAddressBookByUserId(int UserId);
        #region Stored procedures

       
      List<AddressBookModel> GetAllAddressBookByUserIdSp(int UserId = 0,
                                                                  string FirstName = null,
                                                                  string LastName = null,
                                                                  string Email = null,
                                                                  string City = null,
                                                                  string MobileNumber = null);
        #endregion
    }
}
