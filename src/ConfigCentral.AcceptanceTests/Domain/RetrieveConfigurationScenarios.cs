using System.Collections.Generic;
using System.Linq;
using ConfigCentral.DataAccess;
using ConfigCentral.Domain;
using FluentAssertions;
using NUnit.Framework;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using TestStack.BDDfy;

namespace ConfigCentral.AcceptanceTests.Domain
{
    public class InMemoryConfigurationStore : IConfigurationStore
    {
        public IDictionary<string, IDictionary<string, string>> Data;

        public void AddData(string environment, object anonymous)
        {
            var values = new AnonymousDictionary(anonymous);
            Data.Add(environment,values);
        }

        public InMemoryConfigurationStore() : this(new Dictionary<string, IDictionary<string, string>>()) {}

        public InMemoryConfigurationStore(IDictionary<string, IDictionary<string, string>> data)
        {
            Data = data;
        }

        public IEnumerable<KeyValuePair<string, string>> GetConfigPairs(string environmentName)
        {
            return Data[environmentName];
        }

        public void Persist(string environmentName, IEnumerable<KeyValuePair<string, string>> valuePairs)
        {
            Data[environmentName] = valuePairs.ToDictionary(x => x.Key, x => x.Value);
        }
    }

    public class RetrieveConfigurationScenarios
    {
        private ConfigurationRepository Repository { get; set; }
        private InMemoryConfigurationStore ConfigurationStore { get; set; }
        private IFixture Fixture { get; set; }
        private Configuration Configuration { get; set; }

        [SetUp]
        public void Setup()
        {
            Fixture = new Fixture().Customize(new AutoNSubstituteCustomization());
            ConfigurationStore = new InMemoryConfigurationStore();
            Fixture.Inject<IConfigurationStore>(ConfigurationStore);
            Repository = Fixture.Create<ConfigurationRepository>();
        }

        public void GivenThatTheseConfigurationPairsExistForQaEnvironment()
        {
            ConfigurationStore.AddData("qa", new
            {
                parameter1 = "qa.value1",
                parameter2 = "qa.value2"
            });
        }

        public void WhenIUpdateAQaConfigurationValue()
        {
            Configuration = Repository.GetByEnvironment("qa");
            Configuration["parameter1"] = "qa.value1.updated";
            Repository.Save(Configuration);
        }

        public void ThenTheUpdatedValueShouldBeReturnedWhenRetrievedFromRepository()
        {
            var result = Repository.GetByEnvironment("qa");
            result.ShouldBeEquivalentTo(new
            {
                parameter1 = "qa.value1.updated",
                parameter2 = "qa.value2"
            }.ToAnonymousDictionary());
        }

        [Test]
        public void Execute()
        {
            this.BDDfy();
        }
    }

    public static class AnonymousDictionaryCreator
    {
        public static IDictionary<string, string> ToAnonymousDictionary(this object anonymousObject)
        {
            return new AnonymousDictionary(anonymousObject);
        } 
    }
}