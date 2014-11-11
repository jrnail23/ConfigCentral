using ConfigCentral.DomainModel;
using NHibernate.Mapping.ByCode;
using NHibernate.Mapping.ByCode.Conformist;

namespace ConfigCentral.Infrastructure
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
                    m.Generator(new AssignedGeneratorDef());
                    m.Access(Accessor.NoSetter);
                });
            Version(a => a.Version,
                v =>
                {
                    v.Access(Accessor.NoSetter);
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