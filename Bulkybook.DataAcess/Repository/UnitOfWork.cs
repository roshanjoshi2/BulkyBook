using Bulkybook.DataAcess.Repository.IRepository;
using BulkyBook.DataAcess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkybook.DataAcess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Product = new ProductRepository(_db);
            Register = new RegisterRepository(_db);

        }
        public Icategoryrepository Category {  get; private set; }
        public ICoverTypeRepository CoverType {  get; private set; }

        public IProductRepository Product { get; private set; }
        public IRegisterRepository Register { get; private set; }

        public void Save()
        {
           _db.SaveChanges();
        }
    }
}
