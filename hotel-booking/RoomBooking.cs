using System;

public class RoomBooking
{
    public RoomBooking(string guest, int room, DateTime date)
    {
        Guest = guest;
        Room = room;
        Date = date;
    }
    public string Guest { get; set; }
    public int Room {get; set; }
    public DateTime Date { get; set; }
}
