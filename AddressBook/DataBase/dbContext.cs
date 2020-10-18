using AddressBook.DataBase.Models;

using AddressBook.ViewModel.AddressBookModels;
using Microsoft.EntityFrameworkCore;


namespace AddressBook.DataBase
{
    public class dbContext:DbContext
    {
        public dbContext(DbContextOptions<dbContext> options):base(options)
        {

        }
        //User Table
       public DbSet<User> Users { get; set; }
        //AddressBook Table
       public DbSet<AddressBooks> AddressBooks { get; set; }

        //SPModel for AddressBook
        [System.Obsolete]
        public DbQuery<AddressBookModel> AddressBookModel { get; set; }

    }
}
