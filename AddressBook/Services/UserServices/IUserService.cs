using AddressBook.DataBase.Models;


namespace AddressBook.Services.UserServices
{
   public interface IUserService
    {
        void CreateUser(User model);
        void UpdateUser(User model);
        void DeleteUser(User model);

        User GetUserById(int Id);
        User GetUserByEmail(string Email);
        

    }
}
