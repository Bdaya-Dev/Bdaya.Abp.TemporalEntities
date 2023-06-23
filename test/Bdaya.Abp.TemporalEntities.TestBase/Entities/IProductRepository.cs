using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Bdaya.Abp.TemporalEntities.Entities
{
    public interface IProductRepository : IRepository<ProductModel, Guid>
    {
    }
}
