using Data_Access.Repositories.Impl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data_Access.Entities;

namespace Data_Access.Repositories
{
    public interface IProductRepository : IGenericRepository<ProductEntity>
    {
        // Add specific methods for Product entity if needed
    }
}
