using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class BookingHelper_OverlappingBookingsExistTests
    {
        private Mock<IBookingRepository> _bookingRepository;
        private BookingHelper _bookingHelper;
        private List<Booking> _existingBookings;

        [SetUp]
        public void SetUp()
        {
            _bookingRepository = new Mock<IBookingRepository>();
            _bookingHelper = new BookingHelper(_bookingRepository.Object);

            _existingBookings = new List<Booking>
            {
                new Booking
                {
                    Id = 2,
                    ArrivalDate = ArriveOn(2021, 9, 14),
                    DepartureDate = DepartOn(2021, 9, 19),
                    Reference = "2-A"
                }
            };

            _bookingRepository.Setup(br => br.GetActiveBookings(1)).Returns(_existingBookings.AsQueryable());
        }

        [Test]
        public void NewBookingStartsAndFinishedBeforeAnExistingBookings_ReturnEmptyString()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBookings.FirstOrDefault().ArrivalDate, 2),
                DepartureDate = Before(_existingBookings.FirstOrDefault().ArrivalDate),
                Reference = "1-A"
            });

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void NewBookingStartsBeforeAndFinishedInTheMiddleOfAnExistingBookings_ReturnExistingBookingReferences()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBookings.FirstOrDefault().ArrivalDate, 2),
                DepartureDate = After(_existingBookings.FirstOrDefault().ArrivalDate),
                Reference = "1-A"
            });

            Assert.That(result, Is.EqualTo(_existingBookings.FirstOrDefault().Reference));
        }

        [Test]
        public void NewBookingStartsBeforeAndFinishedAfterAnExistingBookings_ReturnExistingBookingReferences()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = Before(_existingBookings.FirstOrDefault().ArrivalDate, 2),
                DepartureDate = After(_existingBookings.FirstOrDefault().DepartureDate),
                Reference = "1-A"
            });

            Assert.That(result, Is.EqualTo(_existingBookings.FirstOrDefault().Reference));
        }

        [Test]
        public void NewBookingStartsAndFinishedInTheMiddleOfAnExistingBookings_ReturnExistingBookingReferences()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBookings.FirstOrDefault().ArrivalDate),
                DepartureDate = Before(_existingBookings.FirstOrDefault().DepartureDate),
                Reference = "1-A"
            });

            Assert.That(result, Is.EqualTo(_existingBookings.FirstOrDefault().Reference));
        }

        [Test]
        public void NewBookingStartsInTheMiddleAndFinishedAfterAnExistingBookings_ReturnExistingBookingReferences()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBookings.FirstOrDefault().ArrivalDate),
                DepartureDate = After(_existingBookings.FirstOrDefault().DepartureDate),
                Reference = "1-A"
            });

            Assert.That(result, Is.EqualTo(_existingBookings.FirstOrDefault().Reference));
        }

        [Test]
        public void NewBookingStartsAndFinishedAfterAnExistingBookings_ReturnEmptyString()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                ArrivalDate = After(_existingBookings.FirstOrDefault().DepartureDate),
                DepartureDate = After(_existingBookings.FirstOrDefault().DepartureDate, days: 2),
                Reference = "1-A"
            });

            Assert.That(result, Is.Empty);
        }

        [Test]
        public void BookingOverlapButNewBookingIsCancelled_ReturnEmptyString()
        {
            var result = _bookingHelper.OverlappingBookingsExist(new Booking()
            {
                Id = 1,
                Status = "Cancelled",
                ArrivalDate = After(_existingBookings.FirstOrDefault().ArrivalDate),
                DepartureDate = After(_existingBookings.FirstOrDefault().DepartureDate, days: 2),
                Reference = "1-A"
            });

            Assert.That(result, Is.Empty);
        }


        private DateTime Before(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(-days);
        }

        private DateTime After(DateTime dateTime, int days = 1)
        {
            return dateTime.AddDays(days);
        }

        private DateTime ArriveOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 14, 0, 0);
        }

        private DateTime DepartOn(int year, int month, int day)
        {
            return new DateTime(year, month, day, 10, 0, 0);
        }
    }
}
