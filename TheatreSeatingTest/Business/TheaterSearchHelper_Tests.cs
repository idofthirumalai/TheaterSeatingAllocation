using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheaterSearch.Business.Helpers;
using TheaterSearch.Business.Models;

namespace TheatreSeatingTest.Business
{
    [TestClass]
    public class TheaterSearchHelper_Tests
    {
        [TestMethod]
        public void BuildResponseMessage_Test()
        {
            //Arrange
            var theaterRequests = new TheaterRequest()
            {
                IsOk = false,
                PersonName = "Smith",
                RequestedSeats = 100,
                RowNumber = 1,
                SectionNumber = 1
            };
            string responseText = "Smith Sorry, we can't handle your party.";

            //Act
            TheaterSearchHelper theaterSearchHelper = new TheaterSearchHelper();
            var result = theaterSearchHelper.BuildResponseMessage(theaterRequests);

            //Assert
            Assert.AreEqual(result, responseText);
        }
    }
}
