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
    : TemporalMongoDbRepository<TestMongoDbContext, ProductModel, ProductModelHistory, Guid>,
        IProductRepository
{
    public ProductRepository(
        IMongoDbContextProvider<TestMongoDbContext> dbContextProvider,
        IRepository<ProductModelHistory, Guid> historyRepository
    )
        : base(dbContextProvider, historyRepository) { }

    protected override ProductModelHistory CreateHistoryEntity(
        ProductModel entity,
        DateTime ValidFrom,
        DateTime ValidTo
    )
    {
        return new ProductModelHistory(entity, ValidFrom, ValidTo);
    }
}
