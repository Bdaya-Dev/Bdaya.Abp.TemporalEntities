using Bdaya.Abp.TemporalEntities.Entities;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.MongoDB;
using Volo.Abp.MongoDB;
using Volo.Abp.Timing;

namespace Bdaya.Abp.TemporalEntities.MongoDB.Entities;

public class ProductRepository
    : TemporalMongoDbRepository<TestMongoDbContext, Product, ProductHistory, Guid>,
        IProductRepository
{
    public ProductRepository(
        IMongoDbContextProvider<TestMongoDbContext> dbContextProvider,
        IRepository<ProductHistory, Guid> historyRepository,
        IClock clock
    )
        : base(dbContextProvider, historyRepository, clock) { }

    protected override ProductHistory CreateHistoryEntity(
        Product entity,
        DateTime ValidFrom,
        DateTime ValidTo
    )
    {
        return new ProductHistory(entity, ValidFrom, ValidTo);
    }
}
