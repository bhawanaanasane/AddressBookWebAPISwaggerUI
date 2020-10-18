using AddressBook.AutoMapper;
using AddressBook.DataBase.Models;
using AddressBook.Services.CommonServices;
using AddressBook.Services.UserServices;
using AddressBook.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace AddressBook.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region Feild
        //Service to do crud operation for user
        private readonly IUserService _userService;
        //Services to do some common operations
        private readonly ICommonService _commonService;
        #endregion
        #region CTOR
        public UserController(IUserService UserService,
                                 ICommonService CommonService)
        {
            this._userService = UserService;
            this._commonService = CommonService;
        }
        #endregion
        [HttpPost]
        [Route("AddEditUser")]
        public IActionResult AddEditUser(UserModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                var userDataByEmail = _userService.GetUserByEmail(model.Email);
                if(userDataByEmail != null)
                {
                    result.Success = false;
                    result.Message = "Email already exist!";
                    return Ok(result);
                   
                }
                if (model.Id == 0)
                {
                    var user = model.ToEntity<User>();
                    user.Password = _commonService.EncryptText(model.Password);
                    _userService.CreateUser(user);
                    model.Id = user.Id;
                    foreach (var addressbook in model.AddressBook)
                    {

                        var addressbookData = addressbook.ToEntity<AddressBooks>();
                        user.AddressBook.Add(addressbookData);
                        _userService.UpdateUser(user);
                        addressbook.Id = addressbookData.Id;
                        addressbook.UserId = user.Id;
                    }
                    //string token = GenerateToken.Generate(username, secretKey);
                    //userModel.Token = token;
                }
                else
                {
                    var userData = _userService.GetUserById(model.Id);
                    userData.FirstName = model.FirstName;
                    userData.LastName = model.LastName;
                    userData.Email = model.Email;
                    userData.UserRoleId = model.UserRoleId;
                    userData.Password = _commonService.EncryptText(model.Password);
                    _userService.UpdateUser(userData);
                }
                
                result.Success = true;
                result.Message = "User saved!";
                result.Responce = model;
                    }
            catch(Exception e)
            {
                result.Success = false;
                result.Message =e.ToString();
            }

            return Ok(result); 
        }

        [HttpGet]
        [Route("DeleteUser")]
        public IActionResult DeleteUser(int Id,UserRole userRole)
        {
            ResultModel result = new ResultModel();
            try
            {
                if (userRole == UserRole.Admin)
                {
                    var userData = _userService.GetUserById(Id);
                    if (userData != null)
                    {
                        _userService.DeleteUser(userData);
                        result.Success = true;
                        result.Message = "User hase been deleted!";
                    }
                    else
                    {
                        result.Success = false;
                        result.Message = "User does not exist!";
                    }
                }
                else
                {
                    result.Success = false;
                    result.Message = "Unauthorized user!";
                }
            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.ToString();
            }
            return Ok(result);
        }

    }
}
