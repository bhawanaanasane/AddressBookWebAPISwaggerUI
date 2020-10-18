
using AddressBook.AutoMapper;
using AddressBook.Services.LoginServices;
using AddressBook.Services.UserServices;
using AddressBook.ViewModel;
using AddressBook.ViewModel.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AddressBook.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        #region Feild
        private readonly AppSettings _appSettings;
        private readonly ILoginService _loginService;
        private readonly IUserService _userService;
       
        #endregion
        #region CTOR
        public LoginController(IOptions<AppSettings> appSettings, 
                                ILoginService LoginService,
                               IUserService UserService)
        {
            _appSettings = appSettings.Value;
            this._loginService = LoginService;
            this._userService = UserService;
            
        }
        #endregion
        [HttpGet]
        public ResultModel UserAuthenticate(string username, string password)
        {
            ResultModel resultModel = new ResultModel();            
            var userLoginResults = _loginService.ValidateUserLogin(username, password);

            if (userLoginResults.LoginResults == LoginResults.Successful)
            {
               
                var userDetails = _userService.GetUserById(userLoginResults.User.Id);
                var userModel = new UserModel();
                if (userDetails != null)
                {
                    userModel = userDetails.ToModel<UserModel>();
                    string token = GenerateToken.Generate(username, _appSettings.Secret);
                    userModel.Token = token;
                    resultModel.Success = true;
                    resultModel.Responce = userModel;
                    resultModel.Message = "Login Success";
                    return resultModel;
                }
                else
                {
                    resultModel.Success = true;
                    resultModel.Responce = null;
                    resultModel.Message ="User not exist!";
                    return resultModel;
                }
            }
            else
            {
                resultModel.Success = false;
                resultModel.Responce = null;
                resultModel.Message = userLoginResults.LoginResults.ToString();
                return resultModel;
            }
        }
    }
}
