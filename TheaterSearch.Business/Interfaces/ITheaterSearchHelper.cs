using System.Collections.Generic;
using TheaterSearch.Business.Models;

namespace TheaterSearch.Business.Interfaces
{
    public interface ITheaterSearchHelper
    {
        void MapTheaterDetails(TheaterLayout layout, TheaterRequest request, TheaterSection section);
        int FindSectionByAvailableSeats(List<TheaterSection> sections, int availableSeats);
        int FindComplementRequest(List<TheaterRequest> requests, int complementSeats, int currentRequestIndex);
        string BuildResponseMessage(TheaterRequest request);
    }
}