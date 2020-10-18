
using System.Linq;
using AddressBook.DataBase.Models;
using AddressBook.DataBase.Repository;
using System;
using System.Collections.Generic;

using AddressBook.DataBase;
using Microsoft.EntityFrameworkCore;
using AddressBook.ViewModel.AddressBookModels;

namespace AddressBook.Services.UserServices
{
    public class AddressBookService : IAddressBookService
    {
        #region Feilds
        private readonly IRepository<AddressBooks> _addressBookRepository;
        private readonly dbContext _dbContext;
        #endregion
        #region CTOR

        public AddressBookService(IRepository<AddressBooks> AddressBookRepository,
                                     dbContext DbContext)
        {
            this._addressBookRepository = AddressBookRepository;
            this._dbContext = DbContext;
        }
        #endregion
        //Insert AddressBook Data
        public void CreateAddressBook(AddressBooks model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _addressBookRepository.Insert(model);
        }
        //Update AddressBook Data
        public void UpdateAddressBook(AddressBooks model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _addressBookRepository.Update(model);
        }
        //Dalete AddressBook Data
        public void DeleteAddressBook(AddressBooks model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            _addressBookRepository.Delete(model);
        }
        //Get AddressBook Data By Id
        public AddressBooks GetAddressBookById(int Id)
        {
            var query = from a in _addressBookRepository.Table
                        where a.Id == Id
                        select a;
            return query.FirstOrDefault();

        }
        //Get ALladdressbook bu user id
     public   List<AddressBooks> GetAddressBookByUserId(int UserId)
        {
            var query = from a in _addressBookRepository.Table
                        where a.UserId == UserId
                        select a;
            return query.ToList();

        }

        #region Stored procedures

        [Obsolete]
        public List<AddressBookModel> GetAllAddressBookByUserIdSp(int UserId = 0,
                                                                  string FirstName = null,
                                                                  string LastName = null,
                                                                  string Email = null,
                                                                  string City = null,
                                                                  string MobileNumber = null)
        {

            try
            {

                string query = @"exec [sp_GetAllAddressBookByUserId]" +
                                "@UserId='" + UserId + "'," +
                                "@FirstName='" + FirstName + "'," +
                                "@LastName='" + LastName + "'," +
                                "@Email='" + Email + "'," +
                                "@City='" + City + "'," +
                                "@MobileNumber='" + MobileNumber + "'";

                var data = _dbContext.AddressBookModel.FromSqlRaw(query).ToList();
                return data;

            }
            catch (Exception ex)
            {
                return null;
            }

        }



        #endregion
    }

}
