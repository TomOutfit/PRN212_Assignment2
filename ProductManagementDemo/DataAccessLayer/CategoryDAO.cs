using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            try
            {
                using var context = new MyStoreContext();
                return context.Categories.ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving categories: " + e.Message);
            }
        }
    }
}