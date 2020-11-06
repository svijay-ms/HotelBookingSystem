using System;
using System.Linq;
using System.Collections.Generic;

namespace HotelBookingSystem
{
    public class BookingManager : IBookingManager
    {
        private Dictionary<string, string> roomsBooked = new Dictionary<string, string>();

        private List<int> roomList = new List<int>() { 101, 102, 201, 203 };

        private Object threadLock = new object();

        /**
        * Return true if there is no booking for the given room on the date,
        * otherwise false
        */
        public bool IsRoomAvailable(int room, DateTime date)
        {
            try
            {
                lock (threadLock)
                {
                    var isValidRoomId = roomList.Any(rm => rm == room);
                    if (!isValidRoomId)
                    {
                        return false;
                    }

                    string key = string.Format("{0}_{1}", room, date.ToString("ddMMyyyy"));
                    return roomsBooked != null && !roomsBooked.ContainsKey(key);
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /**
        * Add a booking for the given guest in the given room on the given
        * date. If the room is not available, throw a suitable Exception.
        */
        public void AddBooking(string guest, int room, DateTime date)
        {
            try
            {
                lock (threadLock)
                {
                    if (roomsBooked == null)
                    {
                        roomsBooked = new Dictionary<string, string>();
                    }

                    var isValidRoomId = roomList.Any(rm => rm == room);
                    var FormattedDate = date.ToString("ddMMyyyy");
                    var key = string.Format("{0}_{1}", room, FormattedDate);

                    if (!isValidRoomId)
                    {
                        throw new InvalidOperationException("Room " + room + " is not valid!");
                    }

                    if (roomsBooked.ContainsKey(key))
                    {
                        throw new InvalidOperationException("Room " + room + " is already booked");
                    }

                    roomsBooked.Add(key, guest);
                }
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Room " + room + " is already booked");
            }
        }

        /**
         * Return a list of all the available room numbers for the given date
         */
        public IEnumerable<int> getAvailableRooms(DateTime date)
        {
            try
            {
                List<int> availableRooms = new List<int>();

                lock (threadLock)
                {
                    foreach (var room in roomList)
                    {
                        string key = string.Format("{0}_{1}", room, date.ToString("ddMMyyyy"));
                        if (!roomsBooked.ContainsKey(key))
                        {
                            availableRooms.Add(room);
                        }
                    }
                }

                return availableRooms;
            }
            catch (Exception)
            {
                throw new NullReferenceException("No rooms available");
            }
        } 
    }
}
