using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TheaterSearch.Business.Handlers;
using TheaterSearch.Business.Interfaces;
using TheaterSearch.Business.Models;

namespace TheaterSeatingTest
{
    [TestClass]
    public class TheaterSeatingSearchUnitTest
    {
        [TestMethod]
        public void GetTheaterLayout_Test()
        {
            //Arrange
            string layout = "5 5\r\n7 8\r\n";

            Mock<ITheaterSearchHelper> mockTheaterSearchHelper = new Mock<ITheaterSearchHelper>();
            TheaterSeatingSearch theaterSeatingSearch = new TheaterSeatingSearch(mockTheaterSearchHelper.Object);

            //Act
            var result = theaterSeatingSearch.GetTheaterLayout(layout);

            //Assert
            Assert.AreEqual(result.TotalSeats, 25);
        }

        [TestMethod]
        public void GetTicketRequest_Test()
        {
            //Arrange
            string ticketRequests = "Smith 5\r\nThiru 8\r\n";

            Mock<ITheaterSearchHelper> mockTheaterSearchHelper = new Mock<ITheaterSearchHelper>();
            TheaterSeatingSearch theaterSeatingSearch = new TheaterSeatingSearch(mockTheaterSearchHelper.Object);

            //Act
            var result = theaterSeatingSearch.GetTicketRequests(ticketRequests);

            //Assert
            Assert.AreEqual(result.Count, 2);
        }


        [TestMethod]
        public void ProcessTicketRequests_Test()
        {
            //Arrange
            var theaterLayout= new TheaterLayout()
            {
                UsableSeats = 10,
                TotalSeats = 10,
                Sections = new List<TheaterSection>()
                {
                    new TheaterSection()
                    {
                        AvailableSeats = 5,
                        RowNumber = 1,
                        SectionNumber = 1
                    },
                    new TheaterSection()
                    {
                        AvailableSeats = 5,
                        RowNumber = 2,
                        SectionNumber = 2}
                }
            };

            var theaterRequest = new List<TheaterRequest>()
            {
                new TheaterRequest()
                {
                    IsOk = false,
                    PersonName = "Smith",
                    RequestedSeats = 5,
                    RowNumber = 1,
                    SectionNumber = 1
            } };

            Mock<ITheaterSearchHelper> mockTheaterSearchHelper = new Mock<ITheaterSearchHelper>();
            TheaterSeatingSearch theaterSeatingSearch = new TheaterSeatingSearch(mockTheaterSearchHelper.Object);

            //Act
            var result = theaterSeatingSearch.ProcessTicketRequests(theaterLayout, theaterRequest);

            //Assert
            Assert.AreEqual(result.Count, 1);
        }

        [TestMethod]
        public void GetSeatInformation_Test()
        {
            //Arrange
            var responses = new List<TheaterRequest>()
            {
                new TheaterRequest()
                {
                    IsOk = false,
                    PersonName = "Smith",
                    RequestedSeats = 100,
                    RowNumber = 1,
                    SectionNumber = 1
                }
            };
            string responseText = "Smith Sorry, we can't handle your party.";

            Mock<ITheaterSearchHelper> mockTheaterSearchHelper = new Mock<ITheaterSearchHelper>();
            mockTheaterSearchHelper.Setup(x => x.BuildResponseMessage(It.IsAny<TheaterRequest>())).Returns(responseText);
            TheaterSeatingSearch theaterSeatingSearch = new TheaterSeatingSearch(mockTheaterSearchHelper.Object);

            //Act
            var result = theaterSeatingSearch.GetSeatInformation(responses);

            //Assert
            Assert.AreEqual(result.Count, 1);
            Assert.AreEqual(result.FirstOrDefault(), responseText);
        }
    }
}
