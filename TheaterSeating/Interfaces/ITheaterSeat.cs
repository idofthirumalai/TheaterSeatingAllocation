using System.Collections.Generic;

namespace TheaterSeating.Interfaces
{
    public interface ITheaterSeat
    {
        List<string> GetTheaterSeats(List<string> input);
    }
}
