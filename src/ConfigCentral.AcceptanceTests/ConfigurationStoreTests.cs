using System.Collections.Generic;
using ConfigCentral.DataAccess;
using FluentAssertions;
using NUnit.Framework;

namespace ConfigCentral.AcceptanceTests
{
    public class ConfigurationStoreTests
    {
        [Test]
        [TestCase("qa")]
        [TestCase("production")]
        public void CanRetrieveParameterValuesByEnvironment(string environment)
        {
            var sut = new XmlFileBasedConfigurationStore("App_Data");

            var result = sut.GetConfigPairs(environment);

            result.Should()
                .BeEquivalentTo(new Dictionary<string, string>
                {
                    {"parameter1", environment + ".value1"},
                    {"parameter2", environment + ".value2"},
                });
        }
    }
}