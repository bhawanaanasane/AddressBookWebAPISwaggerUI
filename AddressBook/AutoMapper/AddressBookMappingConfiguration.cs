using AddressBook.DataBase.Models;
using AddressBook.ViewModel;
using AddressBook.ViewModel.AddressBookModels;
using AutoMapper;
namespace AddressBook.AutoMapper
{
    public class AddressBookMappingConfiguration :Profile
    {

        #region CTOR
        public AddressBookMappingConfiguration()
        {
            UserMaps();
            AddressBookMaps();
        }
        #endregion
        private void UserMaps()
        {
            //user
            CreateMap<UserModel, User>(); ;
            CreateMap<User, UserModel>().ForMember(dest=>dest.Token, mo => mo.Ignore());
        }

        private void AddressBookMaps()
        {
            //user
            CreateMap<AddressBookModel, AddressBooks>();
            //CreateMap<User, UserModel>();
        }

    }
}
