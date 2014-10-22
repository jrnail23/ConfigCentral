namespace ConfigCentral.DataAccess
{
    public interface IConfigurationRepository
    {
        [NotNull]
        Configuration GetByEnvironment(string environment);
    }
}