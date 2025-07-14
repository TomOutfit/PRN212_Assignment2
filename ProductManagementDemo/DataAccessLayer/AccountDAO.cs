using System;
using System.Linq;
using BusinessObjects;

namespace DataAccessLayer
{
    public class AccountDAO
    {
        public static AccountMember? GetAccountById(string accountId)
        {
            try
            {
                using var db = new MyStoreContext();
                return db.AccountMember.FirstOrDefault(a => a.MemberId == accountId);
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving account: " + e.Message);
            }
        }
    }
}