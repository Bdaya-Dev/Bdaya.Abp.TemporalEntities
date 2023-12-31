﻿using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace Bdaya.Abp.TemporalEntities;

[DependsOn(
    typeof(AbpVirtualFileSystemModule)
    )]
public class TemporalEntitiesInstallerModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<TemporalEntitiesInstallerModule>();
        });
    }
}
