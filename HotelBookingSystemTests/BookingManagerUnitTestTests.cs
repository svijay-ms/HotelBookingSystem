using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelBookingSystem.Tests
{
    [TestClass()]
    public class BookingManagerUnitTestTests
    {
        private IBookingManager _bookingManager;
        private DateTime _today;

        [TestInitialize]
        public void Setup()
        {
            _bookingManager = new BookingManager();
            _today = new DateTime(2020, 11, 02);
        }

        [TestMethod]
        public void Check_Given_Room_IsAvailable_And_Return_True_Then_Add_Booking()
        {
            var isAvailable = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Patel", 101, _today);

            Assert.IsTrue(isAvailable);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Room 101 is already booked")]
        public void Check_GivenRoom_IsAvailable_And_Add_Booking_Then_Should_Return_True_And_False_And_ThrowException()
        {
            var result = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Patel", 101, _today);

            var result1 = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Li", 101, _today);

            Assert.IsTrue(result);
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void Check_GivenRooms_IsAvailable_And_Add_Booking_Then_Should_Return_True()
        {
            var result = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Patel", 101, _today);

            var result1 = _bookingManager.IsRoomAvailable(201, _today);
            _bookingManager.AddBooking("Li", 201, _today);

            Assert.IsTrue(result);
            Assert.IsTrue(result1);
        }

        [TestMethod]
        public void Check_GivenRoom_IsAvailable_And_Add_Booking_Then_Should_Return_True_And_Three_AvailbleRooms_For_GivenDate()
        {
            var result = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Patel", 101, _today);

            var availableRooms = _bookingManager.getAvailableRooms(_today);

            Assert.IsTrue(result);
            Assert.AreEqual<int>(3, availableRooms.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Room 1011 is not valid!")]
        public void Check_Given_InvalidRoom_IsAvailable_And_Add_Booking_Then_Should_Return_False_And_ThrowException()
        {
            var result = _bookingManager.IsRoomAvailable(1011, _today);
            _bookingManager.AddBooking("Patel", 1011, _today);

            var availableRooms = _bookingManager.getAvailableRooms(_today);

            Assert.IsFalse(result);
            Assert.AreEqual<int>(4, availableRooms.Count());
        }

        [TestMethod]
        public void Check_GivenRooms_IsAvailable_And_Add_Booking_Then_Should_Return_AllTrue_And_Two_AvailbleRooms_For_GivenDate()
        {
            var result = _bookingManager.IsRoomAvailable(203, _today);
            _bookingManager.AddBooking("Patel", 203, _today);

            var result1 = _bookingManager.IsRoomAvailable(201, _today);
            _bookingManager.AddBooking("Li", 201, _today);

            var availableRooms = _bookingManager.getAvailableRooms(_today);

            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.AreEqual<int>(2, availableRooms.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException), "Room 203 is already booked")]
        public void Check_GivenRoom_IsAvailable_And_Add_Booking_And_Get_AvailableRooms_Then_Should_Return_True_And_False_And_ThrowException()
        {
            var result = _bookingManager.IsRoomAvailable(203, _today);
            _bookingManager.AddBooking("Patel", 203, _today);

            var result1 = _bookingManager.IsRoomAvailable(203, _today);
            _bookingManager.AddBooking("Li", 203, _today);

            var availableRooms = _bookingManager.getAvailableRooms(_today);

            Assert.IsTrue(result);
            Assert.IsFalse(result1);
        }

        [TestMethod]
        public void Check_GivenRooms_IsAvailable_And_Add_Booking_Then_Should_Return_AllTrue_And_No_AvailbleRooms_ForGivenDate()
        {
            var result = _bookingManager.IsRoomAvailable(201, _today);
            _bookingManager.AddBooking("Patel", 201, _today);

            var result1 = _bookingManager.IsRoomAvailable(203, _today);
            _bookingManager.AddBooking("Li", 203, _today);

            var result2 = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Mark", 101, _today);

            var result3 = _bookingManager.IsRoomAvailable(102, _today);
            _bookingManager.AddBooking("Vijay", 102, _today);

            var availableRooms = _bookingManager.getAvailableRooms(_today);

            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.AreEqual<int>(0, availableRooms.Count());
        }

        [TestMethod]
        public void Check_GivenRooms_IsAvailable_For_Today_And_Add_Booking_Then_Should_Return_AllTrue_And_Four_AvailbleRooms_For_NextDate()
        {
            var result = _bookingManager.IsRoomAvailable(201, _today);
            _bookingManager.AddBooking("Patel", 201, _today);

            var result1 = _bookingManager.IsRoomAvailable(203, _today);
            _bookingManager.AddBooking("Li", 203, _today);

            var result2 = _bookingManager.IsRoomAvailable(101, _today);
            _bookingManager.AddBooking("Mark", 101, _today);

            var result3 = _bookingManager.IsRoomAvailable(102, _today);
            _bookingManager.AddBooking("Vijay", 102, _today);

            var nextDay = _today.AddDays(1);
            var availableRooms = _bookingManager.getAvailableRooms(nextDay);

            Assert.IsTrue(result);
            Assert.IsTrue(result1);
            Assert.IsTrue(result2);
            Assert.IsTrue(result3);
            Assert.AreEqual<int>(4, availableRooms.Count());
        }
    }
}