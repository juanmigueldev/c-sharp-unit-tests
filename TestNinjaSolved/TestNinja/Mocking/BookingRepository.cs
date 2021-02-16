using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestNinja.Mocking
{
    public class BookingRepository : IBookingRepository
    {
        public IQueryable<Booking> GetActiveBookings(int? excludedBookingId = null)
        {
            var unitOfWork = new UnitOfWork();
            var activeBookings = unitOfWork.Query<Booking>()
                    .Where(b => b.Status != "Cancelled");

            if (excludedBookingId.HasValue)
                activeBookings = activeBookings.Where(b => b.Id != excludedBookingId.Value);

            return activeBookings;
        }
    }
}
