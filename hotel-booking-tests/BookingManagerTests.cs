using NUnit.Framework;
using System;
using FluentAssertions;

namespace hotel_booking_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void IsRoomAvailable_Returns_True_if_no_bookings_have_been_made_on_date_given()
        {
            IBookingManager bm = new BookingManager();
            var date = new DateTime(2021,6,7);
            bm.IsRoomAvailable(101, date).Should().BeTrue();
        }
    }
}