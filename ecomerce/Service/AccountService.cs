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
    dbContext.Attach(account).State = EntityState.Modified;
    dbContext.SaveChanges();
}

public bool checkAccountDuplicate(Account account)
{
    return dbContext.Accounts.FirstOrDefault(acc => acc.UserName.Equals(account.UserName)) != null;
}

internal async Task registerAccountAsync(Account account)
{
    account.Type = false;
    dbContext.Accounts.Add(account);
    await dbContext.SaveChangesAsync();
}
    }
}
