using Bdaya.Abp.TemporalEntities.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Bdaya.Abp.TemporalEntities.Permissions;

public class TemporalEntitiesPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(TemporalEntitiesPermissions.GroupName, L("Permission:TemporalEntities"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<TemporalEntitiesResource>(name);
    }
}
