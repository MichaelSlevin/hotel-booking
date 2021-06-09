using System;
using System.Collections.Generic;
using System.Linq;

public class BookingManager : IBookingManager
{    
    private readonly object _bookingLock = new object();
    private List<RoomBooking> _bookings;
    private readonly IEnumerable<int> _rooms;

    public BookingManager(IEnumerable<int> rooms)
    {
        _bookings = new List<RoomBooking>();
        _rooms = rooms;
    }
    public void AddBooking(string guest, int room, DateTime date)
    {
        lock(_bookingLock)
        {
            if(!IsRoomAvailable(room,date))
            {
                throw new RoomUnavailableException();
            }
            var booking = new RoomBooking(guest, room, date);
            _bookings.Add(booking);
        }
    }

    public bool IsRoomAvailable(int room, DateTime date)
    {
        return !_bookings.Any(x => x.Room == room && x.Date == date);
    }

    public IEnumerable<int> GetAvailableRooms(DateTime date)
    {
        return _rooms.Where(x=> IsRoomAvailable(x, date));
    }

    
}
