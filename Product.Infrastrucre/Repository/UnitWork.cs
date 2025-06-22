using AutoMapper;
using Microsoft.Extensions.FileProviders;
using Product.Core.Interface;
using Product.Infrastrucre.Data;
using StackExchange.Redis;
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
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
        private readonly IConnectionMultiplexer _redis;
        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public IBasketRepository BasketRepository { get; }

        public UnitWork(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper, IConnectionMultiplexer redis)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
            _redis = redis;

            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context, _fileProvider, _mapper);
     
        }
    }
}
