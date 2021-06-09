using NUnit.Framework;
using System;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;
using AutoFixture.NUnit3;

namespace hotel_booking_tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, AutoData]
        public void IsRoomAvailable_Returns_True_if_no_bookings_have_been_made_on_date_given(
            int roomNumber,
            DateTime date
        )
        {
            var rooms = new List<int>() { roomNumber };
            IBookingManager bm = new BookingManager(rooms);
            bm.IsRoomAvailable(roomNumber, date).Should().BeTrue();
        }

        [Test, AutoData]
        public void IsRoomAvailable_Returns_False_if_a_booking_has_been_made_for_room_date_pair(
            int roomNumber,
            DateTime date
        )
        {
            var rooms = new List<int>() { roomNumber };
            IBookingManager bm = new BookingManager(rooms);
            bm.AddBooking("Richard", 101, date);
            bm.IsRoomAvailable(101, date).Should().BeFalse();
        }

        [Test, AutoData]
        public void AddBooking_Throws_if_a_booking_has_been_made_for_the_room_already(
            int roomNumber,
            DateTime date
        )
        {
            var rooms = new List<int>() { roomNumber };
            IBookingManager bm = new BookingManager(rooms);
            bm.AddBooking("Richard", roomNumber, date);
            Action act = () => bm.AddBooking("Twain",roomNumber, date);
            act.Should().Throw<RoomUnavailableException>();
        }

        [Test, AutoData]
        public void GetAvailableRooms_Returns_all_rooms_when_no_bookings_have_been_made(
            int room1,
            int room2,
            int room3        
        )
        {
            var rooms = new List<int>() {room1, room2, room3};
            IBookingManager bm = new BookingManager(rooms);
            var availableRooms = bm.GetAvailableRooms(new DateTime()).ToList();

            foreach(var room in rooms)
            {
                availableRooms.Contains(room).Should().BeTrue();
            }
        }

        [Test, AutoData]
        public void GetAvailableRooms_Does_Not_Return_Rooms_That_Are_Booked(
            int room1,
            int room2,
            int room3,
            int bookedRoom,
            DateTime date
        )
        {
            var allRooms = new List<int>() {room1, room2, room3, bookedRoom};

            IBookingManager bm = new BookingManager(allRooms);

            bm.AddBooking("Cyrus", bookedRoom, date);
            var availableRooms = bm.GetAvailableRooms(date).ToList();
            
            availableRooms.Contains(room1).Should().BeTrue();
            availableRooms.Contains(room2).Should().BeTrue();
            availableRooms.Contains(room3).Should().BeTrue();
            availableRooms.Contains(bookedRoom).Should().BeFalse();
        }
    }
}