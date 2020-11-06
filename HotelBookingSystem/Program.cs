using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace HotelBookingSystem
{
    class Program
    {
        static IBookingManager _bookingManager = new BookingManager();
        static DateTime _today = new DateTime(2020, 11, 06);

        static void Main(string[] args)
        {
            try
            {
                CheckIsRoomAvailableAndBook();

                CheckIsRoomAvailableAndBookAsync();

                Console.ReadLine();
            }
            catch (Exception)
            {
                Console.ReadLine();
            }
        }

        /// <summary>
        /// Check is rooms available and book for the given date
        /// </summary>
        private static void CheckIsRoomAvailableAndBook()
        {
            try
            {
                Console.WriteLine("Sync Call");
                Console.WriteLine("*********");

                // Display available rooms for the given date
                DisplayAvailableRooms(_today);

                // Display available rooms for the next date
                Console.WriteLine("");
                var nextDay = _today;
                DisplayAvailableRooms(nextDay.AddDays(1));

                Console.WriteLine("");
                Console.WriteLine(_bookingManager.IsRoomAvailable(101, _today));
                _bookingManager.AddBooking("Patel", 101, _today);

                Console.WriteLine(_bookingManager.IsRoomAvailable(102, _today));
                _bookingManager.AddBooking("Li", 102, _today);

                Console.WriteLine(_bookingManager.IsRoomAvailable(102, _today));
                _bookingManager.AddBooking("Vijay", 102, _today);

                Console.WriteLine(_bookingManager.IsRoomAvailable(201, _today));
                _bookingManager.AddBooking("Subu", 201, _today);

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Check is rooms available and book for the given date with thread safe call
        /// </summary>
        private static void CheckIsRoomAvailableAndBookAsync()
        {
            try 
            {
                List<Task> tasks = new List<Task>();

                Console.WriteLine("\nAsync Call");
                Console.WriteLine("**********");

                // Display available rooms for the given date
                DisplayAvailableRooms(_today);
                Console.WriteLine("");

                // Assuming User 1 calling IsRoomAvailable and AddBooking for room 101 with the given date
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine(_bookingManager.IsRoomAvailable(101, _today));
                    _bookingManager.AddBooking("Patel", 101, _today);

                    Console.WriteLine(_bookingManager.IsRoomAvailable(101, _today));
                    _bookingManager.AddBooking("Li", 101, _today);
                }));

                // Assuming User 2 calling IsRoomAvailable and AddBooking for room 101 with the given date
                tasks.Add(Task.Run(() =>
                {
                    Console.WriteLine(_bookingManager.IsRoomAvailable(101, _today));
                    _bookingManager.AddBooking("Subu", 101, _today);

                    Console.WriteLine(_bookingManager.IsRoomAvailable(101, _today));
                    _bookingManager.AddBooking("Vijay", 101, _today);
                }));

                Task.WaitAll(tasks.ToArray());
            }
            catch(AggregateException ex)
            {
                Console.WriteLine(ex.InnerException.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// Display available rooms for the given date
        /// </summary>
        private static void DisplayAvailableRooms(DateTime date)
        {
            var formattedDate = date.ToString("dd/MM/yyyy");
            var availableRooms = _bookingManager.getAvailableRooms(date);

            if (availableRooms != null && availableRooms.Any())
            {
                Console.WriteLine("Rooms available for the date {0} :", formattedDate);
                foreach (var room in availableRooms)
                {
                    Console.WriteLine(room);
                }
            }
            else
            {
                Console.WriteLine("No rooms available for the date {0}", formattedDate);
            }
        }
    }
}
