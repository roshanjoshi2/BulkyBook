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

 
    public class CategoryRepository : Repository<Category>, Icategoryrepository
    {
        private ApplicationDbContext _db;

        public CategoryRepository( ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
       

        void Icategoryrepository.Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
