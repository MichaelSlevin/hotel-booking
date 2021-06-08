using System;

public class BookingManager : IBookingManager
{    
    public void AddBooking(string guest, int room, DateTime date)
    {
        throw new NotImplementedException();
    }

    public bool IsRoomAvailable(int room, DateTime date)
    {
        return true;
    }
}
