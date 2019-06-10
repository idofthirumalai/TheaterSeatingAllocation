using System.Collections.Generic;
using TheaterSearch.Business.Models;

namespace TheaterSearch.Business.Interfaces
{
    public interface ITheaterSearch
    {
        TheaterLayout GetTheaterLayout(string rawLayout);
        List<TheaterRequest> GetTicketRequests(string ticketRequests);
        List<TheaterRequest> ProcessTicketRequests(TheaterLayout layout, List<TheaterRequest> requests);
        List<string> GetSeatInformation(List<TheaterRequest> listTheaterRequest);
    }
}
