

namespace DeskBooker.Core.Domain
{
    public class DeskBookingResult : DeskBoookingBase
    {
        public DeskBookingResultCode Code { get; set; }
        public int? DeskBookingId { get; set; }
    }
}