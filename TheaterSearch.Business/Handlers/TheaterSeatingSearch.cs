using System;
using System.Collections.Generic;
using TheaterSearch.Business.Interfaces;
using TheaterSearch.Business.Models;

namespace TheaterSearch.Business.Handlers
{
    public class TheaterSeatingSearch : ITheaterSearch
    {
        private readonly ITheaterSearchHelper _theaterSearchHelper;
        public TheaterSeatingSearch(ITheaterSearchHelper theaterSearchHelper)
        {
            _theaterSearchHelper = theaterSearchHelper;
        }
        
        //Process the layout string and map the theater section
        public TheaterLayout GetTheaterLayout(string rawLayout)
        {
            TheaterLayout theaterLayout = new TheaterLayout();
            List<TheaterSection> sectionsList = new List<TheaterSection>();
            int totalCapacity = 0;
            var rows = rawLayout.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            for (int i = 0; i < rows.Length - 1; i++)
            {
                var sections = rows[i].Split(null);

                for (int j = 0; j < sections.Length; j++)
                {
                    int value;

                    try
                    {
                        value = int.Parse(sections[j]);
                    }
                    catch (Exception)
                    {
                        throw new Exception(
                            "'" + sections[j] + "'" + " is invalid section capacity. Please correct it.");
                    }

                    totalCapacity = totalCapacity + value;

                    var section = new TheaterSection
                    {
                        RowNumber = i + 1,
                        SectionNumber = j + 1,
                        AvailableSeats = value
                    };

                    sectionsList.Add(section);
                }
            }

            theaterLayout.TotalSeats = totalCapacity;
            theaterLayout.UsableSeats = totalCapacity;
            theaterLayout.Sections = sectionsList;

            return theaterLayout;
        }

        //Parse the ticket request and create the request lists
        public List<TheaterRequest> GetTicketRequests(string ticketRequests)
        {
            var requestsList = new List<TheaterRequest>();

            var requests = ticketRequests.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

            foreach (var request in requests)
            {
                if (string.IsNullOrEmpty(request))
                    break;

                var splitPersonAndCount = request.Split(null);
                var theaterRequest = new TheaterRequest();
                theaterRequest.PersonName = splitPersonAndCount[0];

                try
                {
                    theaterRequest.RequestedSeats = int.Parse(splitPersonAndCount[1]);
                }
                catch (Exception)
                {
                    throw new Exception("'" + splitPersonAndCount[1] + "'" + " is invalid ticket request. Please correct it.");
                }

                requestsList.Add(theaterRequest);
            }

            return requestsList;
        }

        //Process the Ticket Requests and Layout
        public List<TheaterRequest> ProcessTicketRequests(TheaterLayout layout, List<TheaterRequest> requests)
        {
            for (int i = 0; i < requests.Count; i++)
            {
                TheaterRequest request = requests[i];
                if (request.IsOk) continue;

                if (request.RequestedSeats > layout.UsableSeats)
                {
                    request.RowNumber = -2;
                    request.SectionNumber = -2;
                    continue;
                }

                var sections = layout.Sections;

                foreach (var section in sections)
                {
                    if (request.RequestedSeats == section.AvailableSeats)
                    {
                        _theaterSearchHelper.MapTheaterDetails(layout, request, section);
                        break;

                    }

                    if (request.RequestedSeats < section.AvailableSeats)
                    {

                        int requestNo = _theaterSearchHelper.FindComplementRequest(requests,
                            section.AvailableSeats - request.RequestedSeats, i);

                        if (requestNo == -1)
                        {
                            int sectionNo = _theaterSearchHelper.FindSectionByAvailableSeats(sections, request.RequestedSeats);

                            if (sectionNo >= 0)
                            {
                                TheaterSection perfectSection = sections[sectionNo];

                                _theaterSearchHelper.MapTheaterDetails(layout, request, perfectSection);
                                break;
                            }

                            _theaterSearchHelper.MapTheaterDetails(layout, request, section);
                            break;
                        }

                        _theaterSearchHelper.MapTheaterDetails(layout, request, section);

                        TheaterRequest complementRequest = requests[requestNo];

                        _theaterSearchHelper.MapTheaterDetails(layout, complementRequest, section);

                        break;
                    }
                }

                if (!request.IsOk)
                {
                    request.RowNumber = -1;
                    request.SectionNumber = -1;
                }
            }

            return requests;
        }

        public List<string> GetSeatInformation(List<TheaterRequest> listTheaterRequest)
        {
            var responses = new List<string>();
            foreach (var request in listTheaterRequest)
            {
                responses.Add(_theaterSearchHelper.BuildResponseMessage(request));
            }

            return responses;
        }
    }
}