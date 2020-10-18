

using AddressBook.DataBase.Models;
using System.ComponentModel;

namespace AddressBook.ViewModel.UserModels
{
    public class UserLoginResults
    {
        public User User { get; set; }
        public LoginResults LoginResults { get; set; }
    }
    /// <summary>
    /// Represents the User login result enumeration
    /// </summary>
    public enum LoginResults
    {
        /// <summary>
        /// Login successful
        /// </summary>
        [Description("Login successful")]
        Successful = 1,

        /// <summary>
        /// User does not exist (email )
        /// </summary>
        [Description("User does not exist")]
        UserNotExist = 2,

        /// <summary>
        /// Wrong password
        /// </summary>
        [Description("Wrong password")]
        WrongPassword = 3
        
    }
}
