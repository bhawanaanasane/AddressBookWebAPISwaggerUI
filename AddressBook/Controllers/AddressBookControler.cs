using System;
using AddressBook.AutoMapper;
using AddressBook.DataBase.Models;
using AddressBook.Services.UserServices;
using AddressBook.ViewModel;
using AddressBook.ViewModel.AddressBookModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Controllers
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class AddressBookControler : ControllerBase
    {
        #region Feild
        //Service to do crud operation for user
        private readonly IUserService _userService;
        private readonly IAddressBookService _addressBookService;

        #endregion
        #region CTOR
        public AddressBookControler(IUserService UserService,
                                    IAddressBookService AddressBookService)
        {
            this._userService = UserService;
            this._addressBookService = AddressBookService;
        }
        #endregion

        //Get AddressBook List by UserId
        [HttpPost]
        [Route("GetAddressBooks")]
        public IActionResult GetAddressBooks(AddressBookFilterModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                var addressBooks = _addressBookService.GetAllAddressBookByUserIdSp(UserId: model.UserId,
                                                                                   FirstName: model.FirstName,
                                                                                   LastName: model.LastName,
                                                                                   Email: model.Email,
                                                                                   City: model.City,
                                                                                   MobileNumber: model.MobileNumber);
                result.Success = true;
                result.Responce = addressBooks;

            }
            catch(Exception e)
            {
                result.Success = false;
                result.Message = e.ToString();

            }

            return Ok(result);
        }

        //Add single addressbook by UserId
        [HttpPost]
        [Route("AddEditAddressBook")]
        public IActionResult AddEditAddressBook(AddressBookModel model)
        {
            ResultModel result = new ResultModel();
            try
            {
                var user = _userService.GetUserById(model.UserId);
                if (user != null)
                {
                    if (model.Id == 0) {                       
    
                        var addressbookData = model.ToEntity<AddressBooks>();
                        user.AddressBook.Add(addressbookData);
                        _userService.UpdateUser(user);
                        model.Id = addressbookData.Id;
                        result.Message = "AddressBook saved!";
                    }
                    else
                    {
                        var addressbook = _addressBookService.GetAddressBookById(model.Id);
                        if(addressbook !=null)
                        {
                            addressbook.FirstName = model.FirstName;
                            addressbook.LastName = model.LastName;
                            addressbook.City = model.City;
                            addressbook.Email = model.Email;
                            addressbook.MobileNumber = model.MobileNumber;
                            addressbook.UserId = model.UserId;
                            _addressBookService.UpdateAddressBook(addressbook);
                            result.Message = "AddressBook updated!";
                        }
                        else
                        {

                            result.Success = false;
                            result.Message = "Address book not exist!";
                            result.Responce = model;
                        }
                    }
                    result.Success = true;
                   
                    result.Responce = model;
                }
                else
                {
                    result.Success = false;
                    result.Message = "User not exist!";
                    result.Responce = model;
                }

            }
            catch (Exception e)
            {
                result.Success = false;
                result.Message = e.ToString();
            }

            return Ok(result);
        }


        [HttpGet]
        [Route("DeleteAddressBook")]
        public IActionResult DeleteAddressBook(int Id) {
            ResultModel result = new ResultModel();
            try
            {
                var addressBook = _addressBookService.GetAddressBookById(Id);
                if (addressBook != null)
                {
                    _addressBookService.DeleteAddressBook(addressBook);
                    result.Success = true;
                    result.Message = "Address book has been deleted!";
                }
                else
                {
                    result.Success = false;
                    result.Message = "Address book does not exist!";
                }
            }
            catch(Exception e)
            {
                result.Success = false;
                result.Message = e.ToString();
            }
            


            return Ok(result);
        }


        //[HttpPost]
        //public IActionResult AddEditAddressBooks(AddressBookListModel model)
        //{
        //    ResultModel result = new ResultModel();
        //    try
        //    {
        //        var user = _userService.GetUserById(model.UserId);
        //        if (user != null)
        //        {
        //            foreach(var addressbook in model.AddressBookList)
        //            {
        //                if (addressbook.Id == 0)
        //                {
        //                    var addressbookData = addressbook.ToEntity<AddressBooks>();
        //                    user.AddressBook.Add(addressbookData);
        //                    _userService.UpdateUser(user);
        //                    addressbook.Id = addressbookData.Id;
        //                }
        //                else
        //                {
        //                    var addressBookData = _addressBookService.GetAddressBookById(addressbook.Id);
        //                    addressBookData = addressbook.ToEntity<AddressBooks>();
        //                    _addressBookService.UpdateAddressBook(addressBookData);
        //                }
        //            }

        //            result.Success = true;
        //            result.Message = "AddressBooks saved!";
        //            result.Responce = model;
        //        }

        //    }
        //    catch (Exception e)
        //    {
        //        result.Success = false;
        //        result.Message = e.ToString();
        //    }

        //    return Ok(result);
        //}
    }
}
