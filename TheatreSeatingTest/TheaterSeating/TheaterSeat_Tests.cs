using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TheaterSeating;
using Moq;
using TheaterSearch.Business.Interfaces;
using TheaterSeating.Interfaces;
using TheaterSearch.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TheatreSeatingTest.TheaterSeating
{
    [TestClass]
    public class TheaterSeat_Tests
    {
        [TestMethod]
        public void GetTheaterSeats_Test()
        {
            //Arrange
            string responseText = "Smith Sorry, we can't handle your party.";
            var inputs = new List<string>() {"5 5" };
            var theaterSeats = new List<string>() { responseText };

            var theaterLayout = new TheaterLayout()
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
                        SectionNumber = 2
                    }
                }
            };

            var theaterRequest = new List<TheaterRequest>()
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

            Mock<ITheaterSearch> mockITheaterSearch = new Mock<ITheaterSearch>();
           
            //Act
            mockITheaterSearch.Setup(x => x.GetTheaterLayout(It.IsAny<string>())).Returns(theaterLayout);
            mockITheaterSearch.Setup(x => x.GetTicketRequests(It.IsAny<string>())).Returns(theaterRequest);
            mockITheaterSearch
                .Setup(x => x.ProcessTicketRequests(It.IsAny<TheaterLayout>(), It.IsAny<List<TheaterRequest>>()))
                .Returns(theaterRequest);
            mockITheaterSearch.Setup((x => x.GetSeatInformation(It.IsAny<List<TheaterRequest>>()))).Returns(theaterSeats);

            TheaterSeat theaterSeat = new TheaterSeat(mockITheaterSearch.Object);
            var result = theaterSeat.GetTheaterSeats(inputs);

            //Assert
            Assert.AreEqual(result.FirstOrDefault(), responseText);
        }
    }
}
