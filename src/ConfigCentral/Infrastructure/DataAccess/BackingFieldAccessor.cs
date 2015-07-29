using NHibernate.Properties;

namespace ConfigCentral.Infrastructure.DataAccess
{
    public class BackingFieldAccessor : FieldAccessor
    {
        public BackingFieldAccessor():base(new BackFieldStrategy())
        {
            
        }
    }
}