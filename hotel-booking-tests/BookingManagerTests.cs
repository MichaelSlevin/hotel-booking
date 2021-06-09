using NUnit.Framework;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

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
            var rooms = new List<int>() { 101 };
            IBookingManager bm = new BookingManager(rooms);
            var date = new DateTime(2021,6,7);
            bm.IsRoomAvailable(101, date).Should().BeTrue();
        }

        [Test]
        public void IsRoomAvailable_Returns_False_if_a_booking_has_been_made_for_room_date_pair()
        {
            var rooms = new List<int>() { 101 };
            IBookingManager bm = new BookingManager(rooms);
            var date = new DateTime(2021,6,7);
            bm.AddBooking("Richard", 101, date);
            bm.IsRoomAvailable(101, date).Should().BeFalse();
        }

        [Test]
        public void AddBooking_Throws_if_a_booking_has_been_made_for_the_room_already()
        {
            var rooms = new List<int>() { 101 };
            IBookingManager bm = new BookingManager(rooms);
            var date = new DateTime(2021,6,7);
            bm.AddBooking("Richard", 101, date);
            Action act = () => bm.AddBooking("Twain",101, date);
            act.Should().Throw<RoomUnavailableException>();
        }

        [Test]
        public void GetAvailableRooms_Returns_all_rooms_when_no_bookings_have_been_made()
        {
            var rooms = new List<int>()
            {
                1,2,2007
            };
            IBookingManager bm = new BookingManager(rooms);
            var availableRooms = bm.GetAvailableRooms(new DateTime()).ToList();
            availableRooms.Contains(1).Should().BeTrue();
            availableRooms.Contains(2).Should().BeTrue();
            availableRooms.Contains(2007).Should().BeTrue();
        }

        [Test]
        public void GetAvailableRooms_Does_Not_Return_Rooms_That_Are_Booked()
        {
            var rooms = new List<int>()
            {
                1,2,2007
            };
            var date = new DateTime(2021,6,7);
            IBookingManager bm = new BookingManager(rooms);
            bm.AddBooking("Cyrus", 2007, date);
            var availableRooms = bm.GetAvailableRooms(date).ToList();
            availableRooms.Contains(1).Should().BeTrue();
            availableRooms.Contains(2).Should().BeTrue();
            availableRooms.Contains(2007).Should().BeFalse();
        }
    }
}