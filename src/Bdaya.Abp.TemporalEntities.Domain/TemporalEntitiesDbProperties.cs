namespace Bdaya.Abp.TemporalEntities;

public static class TemporalEntitiesDbProperties
{
    public static string DbTablePrefix { get; set; } = "TemporalEntities";

    public static string? DbSchema { get; set; } = null;

    public const string ConnectionStringName = "TemporalEntities";
}
