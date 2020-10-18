

using AddressBook.DataBase.Models;
using AddressBook.DataBase.Repository;
using System;
using System.Linq;

namespace AddressBook.Services.UserServices
{
    public class UserService : IUserService
    {
        #region Feilds
        private readonly IRepository<User> _userRepository;
        #endregion
        #region CTOR
        public UserService(IRepository<User> UserRepository)
        {
            this._userRepository = UserRepository;
        }
        #endregion
        //Insert user data
        public void CreateUser(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _userRepository.Insert(model);
        }
        //Update user data
        public void UpdateUser(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _userRepository.Update(model);
        }
        //Delete user data
        public void DeleteUser(User model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _userRepository.Delete(model);
        }
        //Get user data By Id
       public User GetUserById(int Id)
        {
            var query = from u in _userRepository.Table
                        where u.Id == Id
                        select u;
            return query.FirstOrDefault();
        }

        //Get User data by Email
        public User GetUserByEmail(string Email)
        {
            var query = from u in _userRepository.Table
                        where u.Email == Email 
                        select u;
            return query.FirstOrDefault();
            
        }
    }
}
