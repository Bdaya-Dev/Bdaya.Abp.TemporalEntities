using Volo.Abp.Reflection;

namespace Bdaya.Abp.TemporalEntities.Permissions;

public class TemporalEntitiesPermissions
{
    public const string GroupName = "TemporalEntities";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(TemporalEntitiesPermissions));
    }
}
