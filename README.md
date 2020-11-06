# HotelBookingSystem

Hotel Bookings Problem

A simple hotel booking system keeps track of the rooms in a hotel. A guest can book a room for
individual nights and the booking system maintains the state of these bookings.
• Guests are identified by their surname which, for the purposes of this exercise, can be
considered unique.

• Rooms are identified by a unique number taken from an arbitrary, potentially nonsequential set
of numbers. For example, a hotel might have four rooms {101, 102, 201, 203}.
• The booking system may be used by a number of hotel staff at once, so your implementation
must be thread-safe.

• There is no need to write an interactive harness or command-line interface. Simply call your
code from a main method or unit test to demonstrate it works.

• There is no need to implement a persistent store.

Part 1

Implement the following interface to provide the functionality for the booking manager. Try to keep
your code simple and handle errors in a sensible fashion.

 public interface IBookingManager
 {
 /**
 * Return true if there is no booking for the given room on the date,
 * otherwise false
 */
 bool IsRoomAvailable(int room, DateTime date);

 /**
 * Add a booking for the given guest in the given room on the given
 * date. If the room is not available, throw a suitable Exception.
 */
 void AddBooking(string guest, int room, DateTime date);
 }
Example usage
Assuming a hotel with four rooms, {101, 102, 201, 203}:
IBookingManager bm = ;// create your manager here;
var today = new DateTime(2012,3,28);
Console.WriteLine(bm.IsRoomAvailable(101, today)); // outputs true
bm.AddBooking("Patel", 101, today);
Console.WriteLine(bm.IsRoomAvailable(101, today)); // outputs false
bm.AddBooking("Li", 101, today); // throws an exception

Part 2

Good news! The hotel staff love your system. However, they find it rather tedious to check the
availability of each room separately. Add the method below to your IBookingManager interface and
implement:

 /**
 * Return a list of all the available room numbers for the given date
 */
 IEnumerable<int> getAvailableRooms(DateTime date);
