using Microsoft.EntityFrameworkCore;
using ecomerce.Models;

namespace ecomerce.Service
{

    public class AccountService
    {
        private readonly ecomerce.Models.MyStoreContext dbContext;
        public AccountService()
        {
            dbContext = new MyStoreContext();
        }

        public Account getAccount(string username, string password)
        {
            return dbContext.Accounts.FirstOrDefault(acc
                    => acc.UserName.Equals(username) && acc.Password.Equals(password));
        }

        public void updateAccountDetail(Account account)
        {
            try
            {
                dbContext.Attach(account).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

      

        public bool checkAccountDuplicate(Account account)
        {
            try
            {
                return dbContext.Accounts.FirstOrDefault(acc => acc.UserName.Equals(account.UserName)) != null;
            }
            catch (Exception)
            {

                throw;
            }
        }

        internal async Task registerAccountAsync(Account account)
        {
            try
            {
                account.Type = false;
                dbContext.Accounts.Add(account);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
