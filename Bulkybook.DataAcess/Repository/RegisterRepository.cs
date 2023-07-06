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
    public class RegisterRepository: Repository<Register>,IRegisterRepository
    {
        private ApplicationDbContext _db;

        public RegisterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        void IRegisterRepository.Update(Register obj)
        {
            _db.Registers.Update(obj);
        }

    }
}
