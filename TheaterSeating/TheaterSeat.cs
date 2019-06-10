using System;
using System.Collections.Generic;
using System.Text;
using TheaterSearch.Business.Interfaces;
using TheaterSearch.Business.Models;
using TheaterSeating.Interfaces;

namespace TheaterSeating
{
    public class TheaterSeat : ITheaterSeat
    {
        private readonly ITheaterSearch _theaterSearch;

        public TheaterSeat(ITheaterSearch theaterSearch)
        {
            _theaterSearch = theaterSearch;
        }

        public List<string> GetTheaterSeats(List<string> inputs)
        {
            StringBuilder layout = new StringBuilder();
            StringBuilder ticketRequests = new StringBuilder();

            //Parse the Theater Layout and Ticket Requests
            GetLayoutAndTicketRequests(inputs, layout, ticketRequests);

            try
            {
                //Get Theater Layout
                TheaterLayout theaterLayout = _theaterSearch.GetTheaterLayout(layout.ToString());

                //Get Ticket Requests
                List<TheaterRequest> requests = _theaterSearch.GetTicketRequests(ticketRequests.ToString());

                //Process Ticket Requests
                var listTheaterRequest = _theaterSearch.ProcessTicketRequests(theaterLayout, requests);

                //Build Response
               return _theaterSearch.GetSeatInformation(listTheaterRequest);

            }
            catch (Exception e)
            {
                Console.WriteLine("Internal Error Occured : " + e.Message);
                Console.ReadLine();
            }

            return null;
        }

        //Parse the input and put layout and ticket requests into the string variable
        private void GetLayoutAndTicketRequests(List<string> input, StringBuilder layout, StringBuilder ticketRequests)
        {
            bool layoutStatus = false;

            foreach (var line in input)
            {
                if (line.Length == 0)
                {
                    layoutStatus = true;
                    continue;
                }

                if (layoutStatus)
                {
                    ticketRequests.Append(line + Environment.NewLine);
                }
                else
                {
                    layout.Append(line + Environment.NewLine);
                }
            }
        }
    }
}
