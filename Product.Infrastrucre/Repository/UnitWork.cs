using Product.Core.Interface;
using Product.Infrastrucre.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastrucre.Repository
{
    public class UnitWork : IUnitWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitWork(ApplicationDbContext context)
        {
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
     
        }
    }
}
