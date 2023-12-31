﻿using Bdaya.Abp.TemporalEntities.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.MongoDB;

namespace Bdaya.Abp.TemporalEntities.MongoDB;

[DependsOn(typeof(TemporalEntitiesDomainModule), typeof(AbpMongoDbModule))]
public class TemporalEntitiesMongoDbModule : AbpModule { }
