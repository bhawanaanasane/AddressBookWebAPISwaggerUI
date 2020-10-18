

using AddressBook.ViewModel.UserModels;

namespace AddressBook.Services.LoginServices
{
  public  interface ILoginService
    {
        /// <summary>
        /// Validate User Login
        /// </summary>
        /// <param name="Email"> email</param>
        /// <param name="password">Password</param>
        /// <returns>Result</returns>
        UserLoginResults ValidateUserLogin(string Email, string password);

    }
}
