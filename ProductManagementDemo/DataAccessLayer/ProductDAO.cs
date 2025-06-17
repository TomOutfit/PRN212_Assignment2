using BusinessObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccessLayer
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            try
            {
                using var db = new MyStoreContext();
                return db.Products.Include(p => p.Category).ToList();
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving products: " + e.Message);
            }
        }

        public static Product? GetProductById(int id)
        {
            try
            {
                using var db = new MyStoreContext();
                return db.Products.Include(p => p.Category).FirstOrDefault(p => p.ProductId == id);
            }
            catch (Exception e)
            {
                throw new Exception("Error retrieving product by ID: " + e.Message);
            }
        }

        public static void SaveProduct(Product p)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Add(p);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error saving product: " + e.Message);
            }
        }

        public static void UpdateProduct(Product product)
        {
            try
            {
                using var context = new MyStoreContext();
                context.Products.Update(product);
                context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception("Error updating product: " + e.Message);
            }
        }

        public static void DeleteProduct(Product product)
        {
            try
            {
                using var context = new MyStoreContext();
                var p1 = context.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (p1 != null)
                {
                    context.Products.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception("Error deleting product: " + e.Message);
            }
        }
    }
}