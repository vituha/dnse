using NUnit.Framework;

namespace DataContractAdapter.Test
{
    [TestFixture]
    public class ProviderContactAdapterTest
    {
        private ProviderContact _providerContact;

        [TestFixtureSetUp]
        void SetUp()
        {
            _providerContact = new ProviderContact()
                                   {
                                       FirstName = "Vasya",
                                       LastName = "Petrov"
                                   };
        }

        [TestFixtureTearDown]
        void TearDown()
        {
            _providerContact = null;
        }

        [Test]
        public void Convert()
        {
            var dataExchangeContact = _providerContact.ToDataExchange();
            Assert.IsNotNull(dataExchangeContact);
            Assert.AreEqual(_providerContact.FirstName, dataExchangeContact.Name1);
            Assert.AreEqual(string.Empty, dataExchangeContact.Name2);
            Assert.AreEqual(_providerContact.LastName, dataExchangeContact.Name3);
        }
    }
}
