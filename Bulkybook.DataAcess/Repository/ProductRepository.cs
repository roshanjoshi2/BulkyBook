using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkybook.DataAcess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        void IProductRepository.Update(Product obj)
        {
            var objFromDB = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDB != null)
            {
                objFromDB.Name = obj.Name;
                objFromDB.ISBN = obj.ISBN;
                objFromDB.Author = obj.Author;
                objFromDB.Price100 = obj.Price100;
                objFromDB.Price50 = obj.Price50;
                objFromDB.CategoryId = obj.CategoryId;
                objFromDB.CoverTypeId = obj.CoverTypeId;
                objFromDB.Price = obj.Price;
                if (obj.ImageUrl != null)
                {
                    objFromDB.ImageUrl = obj.ImageUrl;
                }

            }
        }
    }
}
