using NUnit.Framework;
using SWAPI.Business;
using SWAPI.Helpers;
using System.Linq;

namespace SWAPI.Tests
{
    public class APICallIntegrationtTest
    {
        #region StarshipBusiness.GetStarshipStops Tests
        [Test]
        public void Should_GetStarshipStops_Bring_Results()
        {
            System.Collections.Generic.List<Models.StarshipModel> returnValue = StarshipBusiness.GetStarshipStops(200);

            Assert.IsNotNull(returnValue);
            Assert.Pass("OK for GetStarshipStops Should Bring Results test");
        }

        [Test]
        public void Should_GetStarshipStops_Ywing_Stops_74_Times()
        {
            System.Collections.Generic.List<Models.StarshipModel> returnValue = StarshipBusiness.GetStarshipStops(1000000);
            Assert.IsNotNull(returnValue);

            Models.StarshipModel ywing = returnValue.FirstOrDefault(x => x.name.ToLower() == "y-wing");

            Assert.IsNotNull(ywing);
            Assert.AreEqual(ywing.stops, 74);
            Assert.Pass("OK for GetStarshipStops Y-wing stops 74 times test");
        }

        [Test]
        public void Should_GetStarshipStops_Millennium_Falcon_Stops_9_Times()
        {
            System.Collections.Generic.List<Models.StarshipModel> returnValue = StarshipBusiness.GetStarshipStops(1000000);
            Assert.IsNotNull(returnValue);

            Models.StarshipModel ywing = returnValue.FirstOrDefault(x => x.name.ToLower() == "millennium falcon");

            Assert.IsNotNull(ywing);
            Assert.AreEqual(ywing.stops, 9);
            Assert.Pass("OK for GetStarshipStops Millennium Falcon stops 9 times test");
        }

        [Test]
        public void Should_GetStarshipStop_Rebel_Transport_Stops_11_Times()
        {
            System.Collections.Generic.List<Models.StarshipModel> returnValue = StarshipBusiness.GetStarshipStops(1000000);
            Assert.IsNotNull(returnValue);

            Models.StarshipModel ywing = returnValue.FirstOrDefault(x => x.name.ToLower() == "rebel transport");

            Assert.IsNotNull(ywing);
            Assert.AreEqual(ywing.stops, 11);
            Assert.Pass("OK for GetStarshipStops Rebel Transport stops 11 times test");
        }

        #endregion

        #region APICall.GetStarshipResults Tests
        [Test]
        public void Should_GetStarshipResults_Not_Brings_Null()
        {
            Models.StarshipResultModel returnValue = APICall.GetStarshipResults();

            Assert.IsNotNull(returnValue);
            Assert.Pass("OK for GetStarshipResults not be null test");
        }

        [Test]
        public void Should_GetStarshipResults_Brings_Results()
        {
            Models.StarshipResultModel returnValue = APICall.GetStarshipResults();

            Assert.IsTrue(returnValue.results.Any());
            Assert.Pass("OK for GetStarshipResults Contain Results test");
        }

        [Test]
        public void Should_GetStarshipResults_CountProperty_And_CountResults_Are_Equal()
        {
            Models.StarshipResultModel returnValue = APICall.GetStarshipResults();

            Assert.AreEqual(returnValue.results.Count, returnValue.count);
            Assert.Pass("OK for GetStarshipResults CountProperty and CountResults are equal test");
        }

        [Test]
        public void Should_GetStarshipResults_NextValue_Be_Empty()
        {
            Models.StarshipResultModel returnValue = APICall.GetStarshipResults();

            Assert.IsTrue(string.IsNullOrWhiteSpace(returnValue.next));
            Assert.Pass("OK for GetStarshipResults NextValue is empty test");
        }
        #endregion
    }
}