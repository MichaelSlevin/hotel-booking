using System;
using System.Collections.Generic;
using System.Linq;

public class BookingManager : IBookingManager
{    
    private readonly object bookingLock = new object();
    public BookingManager()
    {
        Bookings = new List<RoomBooking>();
    }
    public void AddBooking(string guest, int room, DateTime date)
    {
        lock(bookingLock)
        {
            if(!IsRoomAvailable(room,date))
            {
                throw new RoomUnavailableException();
            }
            var booking = new RoomBooking(guest, room, date);
            Bookings.Add(booking);
        }
    }

    public bool IsRoomAvailable(int room, DateTime date)
    {
        return !Bookings.Any(x => x.Room == room && x.Date == date);
    }
    private List<RoomBooking> Bookings;
}
