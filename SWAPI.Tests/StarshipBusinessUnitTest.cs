using NUnit.Framework;
using SWAPI.Business;
using SWAPI.Models;
using System.Collections.Generic;

namespace SWAPI.Tests
{
    public class StarshipBusinessUnitTest
    {
        private List<StarshipModel> _starshipValidList;
        private List<StarshipModel> _starshipInvalidList;

        [SetUp]
        public void Setup()
        {
            _starshipValidList = new List<StarshipModel>
            {
                new StarshipModel { name = "Y-wing", MGLT = "80", consumables = "1 week", },
                new StarshipModel { name = "Millennium Falcon", MGLT = "75", consumables = "2 months", },
                new StarshipModel { name = "Rebel transport", MGLT = "20", consumables = "6 months", }
            };

            _starshipInvalidList = new List<StarshipModel>
            {
                new StarshipModel { name = "invalid", MGLT = "invalid", consumables = "invalid"  },
                new StarshipModel { name = "negative", MGLT = "-1", consumables = "-1" }
            };
        }

        #region StarshipBusiness.GetSpentPerHour Tests
        [Test]
        public void Should_GetSpentPerHour_Return_Same_Value_For_Different_Time_Unit()
        {
            var hours = StarshipBusiness.GetSpentPerHour("8760 hours");
            var day = StarshipBusiness.GetSpentPerHour("365 days");
            var year = StarshipBusiness.GetSpentPerHour("1 year");

            Assert.IsTrue(hours == day && hours == year && day == year);

            Assert.Pass("OK GetSpentPerHour for return same value for different time unit test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Value_If_Consumables_Per_Year_IsPuzzledCase_and_Plural()
        {
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("2 hoUrs") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("2 DayS") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("2 weeKs") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("5 mOnths") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("8 YeArs") > 0);
            Assert.Pass("OK GetSpentPerHour for PascalCase and Plural test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Value_If_Consumables_Is_Pascal_Case_And_Singular()
        {
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("1 Hour") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("1 Day") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("1 Week") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("1 Month") > 0);
            Assert.IsTrue(StarshipBusiness.GetSpentPerHour("1 Year") > 0);
            Assert.Pass("OK for PascalCase and Singular test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Default_Value_If_Consumables_Is_Negative()
        {
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 hours"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 days"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 weeks"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 months"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 years"), 0);

            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 hour"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 day"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 week"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 month"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("-1 year"), 0);
            Assert.Pass("OK for Negative Values test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Default_Value_If_Consumables_Is_Null()
        {
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour(null), 0);
            Assert.Pass("OK for Null Values test");
        }

        [Test]
        public void Should_GetSpentPerHour_ReturnDefault_Value_If_Consumables_Is_Empty()
        {
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour(string.Empty), 0);
            Assert.Pass("OK for Empty Values test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Default_Value_If_Consumables_Has_Invalid_Values()
        {
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("invalid invalid"), 0);
            Assert.Pass("OK for Invalid Values test");
        }

        [Test]
        public void Should_GetSpentPerHour_Return_Default_Value_If_Consumables_Has_Zero_Values()
        {
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("0 days"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("0 weeks"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("0 months"), 0);
            Assert.AreEqual(StarshipBusiness.GetSpentPerHour("0 years"), 0);
            Assert.Pass("OK for Zero Values test");
        }
        #endregion

        #region StarshipBusiness.CalculateStarshipStops Tests
        [Test]
        public void Should_CalculateStarshipStops_Not_Throw_Exception_If_Input_Is_Null_Or_Zero()
        {
            List<StarshipModel> list = null;

            StarshipBusiness.CalculateStarshipStops(ref list, 0);
            Assert.Pass("OK for CalculateStarshipStops Null And Zero Values test");
        }

        [Test]
        public void Should_CalculateStarshipStops_Not_Throw_Exception_If_Input_Is_Invalid()
        {
            StarshipBusiness.CalculateStarshipStops(ref _starshipInvalidList, 0);

            _starshipInvalidList.ForEach(x => Assert.AreEqual(x.stops, 0));

            Assert.Pass("OK for CalculateStarshipStops Invalid Starshiplist test");
        }

        [Test]
        public void Should_CalculateStarshipStops_Greather_Than_Zero_If_Input_Is_Valid()
        {
            StarshipBusiness.CalculateStarshipStops(ref _starshipValidList, 1000000);

            _starshipValidList.ForEach(x => Assert.IsTrue(x.stops > 0));

            Assert.Pass("OK for CalculateStarshipStops valid Starshiplist test");
        }

        [Test]
        public void Should_CalculateStarshipStops_Greather_Than_Zero_If_Input_Is_Valid2()
        {
            StarshipBusiness.CalculateStarshipStops(ref _starshipValidList, 1000000);

            _starshipValidList.ForEach(x => Assert.IsTrue(x.stops > 0));

            Assert.Pass("OK for CalculateStarshipStops valid Starshiplist test");
        }
        #endregion
    }
}