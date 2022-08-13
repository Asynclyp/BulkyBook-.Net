using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) :base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDd = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDd != null)
            {
            
                objFromDd.Title = obj.Title;
                objFromDd.ISBN = obj.ISBN;
                objFromDd.Price = obj.Price;
                objFromDd.Price50 = obj.Price50;
                objFromDd.ListPrice = obj.ListPrice;
                objFromDd.Price100 = obj.Price100;
                objFromDd.Description = obj.Description;
                objFromDd.CategoryId = obj.CategoryId;
                objFromDd.Author = obj.Author;
                objFromDd.CoverTypeId = obj.CoverTypeId;
                if(obj.ImageUrl != null)
                {
                    objFromDd.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
