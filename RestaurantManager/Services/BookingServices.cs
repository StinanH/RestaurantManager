using Azure.Core;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;

        public BookingServices(IBookingRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        public async Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync()
        {
            var allBookings = await _bookingRepository.GetAllBookingsAsync();

            var bookingsList = allBookings.Select(b => new BookingGetDTO
            {
                Id = b.Id,
                NrOfPeople = b.NrOfPeople,
                Requests = b.Requests,
                ReservationDateTimeStart = b.ReservationDateTimeStart,
                ReservationDateTimeEnd = b.ReservationDateTimeEnd,
                RestaurantId = b.FK_RestaurantId,
                //Restaurant = b.Restaurant, <-------- Need to be RestaurantGetDTO?
                UserId = b.FK_UserID,
                //User = b.User,<-------- Need to be UserGetDTO?
                TableId = b.FK_TableId
            }).ToList();

            return bookingsList;
        }
        public async Task<IEnumerable<BookingGetDTO>> GetAllBookingsByRestaurantIdAsync(int restaurantId)
        {
            var allBookings = await _bookingRepository.GetAllBookingsByRestaurantIdAsync(restaurantId);

            var bookingsList = allBookings.Select(b => new BookingGetDTO
            {
                Id = b.Id,
                NrOfPeople = b.NrOfPeople,
                Requests = b.Requests,
                ReservationDateTimeStart = b.ReservationDateTimeStart,
                ReservationDateTimeEnd = b.ReservationDateTimeEnd,
                RestaurantId = b.FK_RestaurantId,
                //Restaurant = b.Restaurant, < --------Need to be RestaurantGetDTO?
                UserId = b.FK_UserID,
                //User = b.User, < --------Need to be UserGetDTO?
                TableId = b.FK_TableId
            }).ToList();

            return bookingsList;

        }
        public async Task<BookingGetDTO> GetBookingAsync(int bookingID)
        {
            var bookingById = await _bookingRepository.GetBookingAsync(bookingID);

            var booking = new BookingGetDTO
            {
                Id = bookingById.Id,
                NrOfPeople = bookingById.NrOfPeople,
                Requests = bookingById.Requests,
                ReservationDateTimeStart = bookingById.ReservationDateTimeStart,
                ReservationDateTimeEnd = bookingById.ReservationDateTimeEnd,
                RestaurantId = bookingById.FK_RestaurantId,
                //Restaurant = bookingById.Restaurant, < --------Need to be RestaurantGetDTO?
                UserId = bookingById.FK_UserID,
                //User = bookingById.User, < --------Need to be UserGetDTO?
                TableId = bookingById.FK_TableId
            };

            return booking;

        }

        //return bool on these 3 to report success?
        public async Task AddBookingAsync(BookingCreateDTO bookingDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            Booking bookingToAdd = new Booking
            {
                NrOfPeople = bookingDTO.NrOfPeople,
                Requests = bookingDTO.Requests,
                ReservationDateTimeStart = bookingDTO.ReservationDateTimeStart,
                ReservationDateTimeEnd = bookingDTO.ReservationDateTimeEnd,
                FK_RestaurantId = bookingDTO.RestaurantId,
                //Restaurant = Get restaurant 
                FK_UserID = bookingDTO.UserId,
                //User = bookingDTO.User,
                FK_TableId = bookingDTO.TableId
            };

            await _bookingRepository.AddBookingAsync(bookingToAdd);
        }
        public async Task UpdateBookingAsync(BookingUpdateDTO bookingDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            var bookingToUpdate = await _bookingRepository.GetBookingAsync(bookingDTO.Id);

            bookingToUpdate.NrOfPeople = bookingDTO.NrOfPeople;
            bookingToUpdate.Requests = bookingDTO.Requests;
            bookingToUpdate.ReservationDateTimeStart = bookingDTO.ReservationDateTimeStart;
            bookingToUpdate.ReservationDateTimeEnd = bookingDTO.ReservationDateTimeEnd;
            bookingToUpdate.FK_RestaurantId = bookingDTO.RestaurantId;
            //Restaurant = Get restaurant 
            bookingToUpdate.FK_UserID = bookingDTO.UserId;
            //User = bookingDTO.User,
            bookingToUpdate.FK_TableId = bookingDTO.TableId;

            await _bookingRepository.UpdateBookingAsync(bookingToUpdate);

        }
        public async Task DeleteBookingAsync(int bookingId)
        {
            var bookingToDelete = await _bookingRepository.GetBookingAsync(bookingId);

            //check if toDelete == nulll, if so return false

            await _bookingRepository.DeleteBookingAsync(bookingToDelete);

            //return true;
        }
    }
}
