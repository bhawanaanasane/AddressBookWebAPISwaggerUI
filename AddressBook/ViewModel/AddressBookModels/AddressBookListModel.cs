using System.Collections.Generic;


namespace AddressBook.ViewModel.AddressBookModels
{

    public class AddressBookListModel
    {
        public AddressBookListModel()
        {
            AddressBookList = new List<AddressBookModel>();
        }
        public List<AddressBookModel> AddressBookList { get; set; }

        public int UserId { get; set; }
    }
}
