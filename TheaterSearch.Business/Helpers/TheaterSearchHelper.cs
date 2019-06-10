using System.Collections.Generic;
using TheaterSearch.Business.Interfaces;
using TheaterSearch.Business.Models;

namespace TheaterSearch.Business.Helpers
{
    public class TheaterSearchHelper : ITheaterSearchHelper
    {
        //To map theater details to the request  object 
        public void MapTheaterDetails(TheaterLayout layout, TheaterRequest request, TheaterSection section)
        {
            request.RowNumber = section.RowNumber;
            request.SectionNumber = section.SectionNumber;
            section.AvailableSeats = section.AvailableSeats - request.RequestedSeats;
            layout.UsableSeats = layout.UsableSeats - request.RequestedSeats;
            request.IsOk = true;
        }

        //Find the number of available seats in the section list
        public int FindSectionByAvailableSeats(List<TheaterSection> sections, int availableSeats)
        {
            int i = 0;
            TheaterSection section = new TheaterSection();
            section.AvailableSeats = availableSeats;

            sections.Sort(new CompareAvailableSeats());
            CompareAvailableSeats compareAvailable = new CompareAvailableSeats();
            int sectionNo = sections.BinarySearch(section, compareAvailable);

            if (sectionNo > 0)
            {
                for (i = sectionNo - 1; i >= 0; i--)
                {
                    TheaterSection s = sections[i];

                    if (s.AvailableSeats != availableSeats) break;
                }

                sectionNo = i + 1;
            }

            return sectionNo;
        }

        //To find available seats in the partially filled section
        public int FindComplementRequest(List<TheaterRequest> requests, int complementSeats, int currentRequestIndex)
        {
            int requestNo = -1;

            for (int i = currentRequestIndex + 1; i < requests.Count; i++)
            {
                TheaterRequest request = requests[i];

                if (!request.IsOk && request.RequestedSeats == complementSeats)
                {
                    requestNo = i;
                    break;
                }
            }

            return requestNo;
        }

        //Build and format the output response
        public string BuildResponseMessage(TheaterRequest request)
        {
            string status;
            if (request.IsOk)
            {
                status = request.PersonName + " " + "Row " + request.RowNumber + " " + "Section " + request.SectionNumber;
            }
            else
            {
                if (request.RowNumber == -1 && request.SectionNumber == -1)
                {
                    status = request.PersonName + " " + "Call to split party.";
                }
                else
                {
                    status = request.PersonName + " " + "Sorry, we can't handle your party.";
                }
            }

            return status;
        }

        //Compare the two different sections
        private class CompareAvailableSeats : IComparer<TheaterSection>
        {
            public int Compare(TheaterSection o1, TheaterSection o2)
            {
                return o1.AvailableSeats - o2.AvailableSeats;
            }
        }
    }
}
