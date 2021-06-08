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

        [Test]
        public void IsRoomAvailable_Returns_False_if_a_booking_has_been_made_for_room_date_pair()
        {
            IBookingManager bm = new BookingManager();
            var date = new DateTime(2021,6,7);
            bm.AddBooking("Little Richard", 101, date);
            bm.IsRoomAvailable(101, date).Should().BeFalse();
        }

        [Test]
        public void AddBooking_Throws_if_a_booking_has_been_made_for_the_room_already()
        {
            IBookingManager bm = new BookingManager();
            var date = new DateTime(2021,6,7);
            bm.AddBooking("Little Richard", 101, date);
            Action act = () => bm.AddBooking("Shania Twain",101, date);
            act.Should().Throw<RoomUnavailableException>();
        }
    }
}