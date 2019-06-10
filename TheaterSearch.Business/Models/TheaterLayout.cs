using System.Collections.Generic;

namespace TheaterSearch.Business.Models
{
    public class TheaterLayout
    {
        public int TotalSeats { get; set; }
        public int UsableSeats { get; set; }
        public List<TheaterSection> Sections { get; set; }
    }
}
