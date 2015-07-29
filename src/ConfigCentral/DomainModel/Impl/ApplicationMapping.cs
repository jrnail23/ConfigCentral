using ConfigCentral.Infrastructure.DataAccess;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConfigCentral.DomainModel.Impl
{
    /// <summary>
    ///     TODO: hook up surrogate key, etc.
    /// </summary>
    public class ApplicationMapping : ClassMapping<Application>
    {
        public ApplicationMapping()
        {
            Table("Application");
            //NaturalId(a=>a.);
            Id(a => a.Id,
                m =>
                {
                    m.Column("Id");
                    m.Generator(new GuidCombGeneratorDef());
                    m.Access(typeof(BackingFieldAccessor));
                });
            Version(a => a.Version,
                v =>
                {
                    v.Access(typeof(BackingFieldAccessor));
                    v.Column("Version");
                    v.UnsavedValue(0);
                });
            Property(a => a.Name,
                p =>
                {
                    p.Access(Accessor.NoSetter);
                    p.Column("Name");
                    p.NotNullable(true);
                    p.Length(50);
                    p.Unique(true);
                });
        }
    }
}