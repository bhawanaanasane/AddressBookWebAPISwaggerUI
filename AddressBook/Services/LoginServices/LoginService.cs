
using AddressBook.DataBase.Models;
using AddressBook.Services.CommonServices;
using AddressBook.Services.UserServices;
using AddressBook.ViewModel.UserModels;

namespace AddressBook.Services.LoginServices
{
    public class LoginService : ILoginService
    {
        #region Feild
        private readonly IUserService _userService;
        private readonly ICommonService _commonService;
        #endregion
        #region CTOR
        public LoginService(IUserService UserService,
                                 ICommonService CommonService)
        {
            this._userService = UserService;
            this._commonService = CommonService;
        }
        #endregion
        #region Utilities

        /// <summary>
        /// Check whether the entered password matches with a saved one
        /// </summary>
        /// <param name="UserPassword">User password</param>
        /// <param name="enteredPassword">The entered password</param>
        /// <returns>True if passwords match; otherwise false</returns>
        public bool PasswordsMatch(User UserPassword, string enteredPassword)
        {
            if (UserPassword == null || string.IsNullOrEmpty(enteredPassword))
                return false;

            var savedPassword = string.Empty;

            savedPassword = _commonService.EncryptText(enteredPassword);


            if (UserPassword.Password == null)
                return false;

            return UserPassword.Password.Equals(savedPassword);
        }


        #endregion
        public virtual UserLoginResults ValidateUserLogin(string Email, string password)
        {
            UserLoginResults userLoginResults = new UserLoginResults();
            var User = _userService.GetUserByEmail(Email);


            userLoginResults.User = User;
            if (userLoginResults.User == null)
            {
                userLoginResults.LoginResults = LoginResults.UserNotExist;
                return userLoginResults;
            }           
            
            if (!PasswordsMatch(User, password))
            {
                userLoginResults.LoginResults = LoginResults.WrongPassword;
                return userLoginResults;
            }

            userLoginResults.LoginResults = LoginResults.Successful;
            return userLoginResults;
        }

    }
}
