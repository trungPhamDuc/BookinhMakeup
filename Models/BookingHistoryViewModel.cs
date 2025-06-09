namespace www_Blush_Brush.Models
{
    public class BookingHistoryViewModel
    {
        public Guid BookingId { get; set; }
        public string ArtistName { get; set; }

        public string CustomerName { get; set; }
        public string ServiceName { get; set; }
        public DateTime BookingTime { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public decimal AmountPaid { get; set; }
        public string PaymentStatus { get; set; }
        public int Rating { get; set; }
        public string ReviewComment { get; set; }
    }

}
