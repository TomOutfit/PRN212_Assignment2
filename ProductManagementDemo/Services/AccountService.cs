using BusinessObjects;
using Repositories;

namespace Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService()
        {
            _accountRepository = new AccountRepository();
        }

        public AccountMember GetAccountById(string accountID)
        {
            return _accountRepository.GetAccountById(accountID);
        }
    }
}
