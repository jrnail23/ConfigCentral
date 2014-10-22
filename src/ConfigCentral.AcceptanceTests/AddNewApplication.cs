using System;
using NUnit.Framework;
using TestStack.BDDfy;

namespace ConfigCentral.AcceptanceTests
{
    [TestFixture]
    [Story(AsA = "As an application developer", IWant = "I want to add a new application to ConfigCentral",
        SoThat = "So that I can begin managing its configuration data")]
    public class AddNewApplication : ApiAcceptanceTestBase
    {
        [Test]
        public void NewApplication()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void ApplicationHasAlreadyBeenAdded()
        {
            throw new NotImplementedException();
        }
    }

    [TestFixture]
    [Story(AsA = "As a team lead", IWant = "I want to add my organization to ConfigCentral",
        SoThat = "So that I can begin managing my applications' configuration data")]
    public class AddNewOrganization : ApiAcceptanceTestBase
    {
        [Test]
        public void NewAOrganization()
        {
            throw new NotImplementedException();
        }

        [Test]
        public void OrganizationHasAlreadyBeenAdded()
        {
            throw new NotImplementedException();
        }
    }
}