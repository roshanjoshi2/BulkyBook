using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bulkybook.DataAcess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        Icategoryrepository Category { get; }
        ICoverTypeRepository CoverType { get; }
        IProductRepository Product { get; }
        IRegisterRepository Register { get; }


        void Save();
    }
}
