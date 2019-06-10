namespace TheaterSearch.Business.Models
{
    public class TheaterRequest
    {
        public string PersonName { get; set; }
        public int RequestedSeats { get; set; }
        public bool IsOk { get; set; }
        public int RowNumber { get; set; }
        public int SectionNumber { get; set; }
    }
}
