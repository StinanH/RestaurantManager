using Azure.Core;
using RestaurantManager.Data.Repos;
using RestaurantManager.Data.Repos.IRepos;
using RestaurantManager.Models;
using RestaurantManager.Models.DTOs.BookingDTOs;
using RestaurantManager.Models.DTOs.RestaurantDTOs;
using RestaurantManager.Models.DTOs.TableDTOs;
using RestaurantManager.Models.DTOs.UserDTOs;
using RestaurantManager.Models.DTOs.TimeSlotDTOs;
using RestaurantManager.Services.IServices;

namespace RestaurantManager.Services
{
    public class BookingServices : IBookingServices
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITableRepository _tableRepository;
        private readonly ITimeslotRepository _timeslotRepository;

        public BookingServices(IBookingRepository bookingRepository, IRestaurantRepository restaurantRepository, IUserRepository userRepository, ITableRepository tableRepository, ITimeslotRepository timeslotRepository)
        {
            _bookingRepository = bookingRepository;
            _restaurantRepository = restaurantRepository;
            _userRepository = userRepository;
            _tableRepository = tableRepository;
            _timeslotRepository = timeslotRepository;
        }

        public async Task<IEnumerable<BookingCreateDTO>> GetAvaliableBookingsOnDayAsync(int restaurantId, int numberOfPeople, DateTime date)
        {

            var listOfTimeslots = await _bookingRepository.GetAvaliableBookingsOnDayAsync(restaurantId, numberOfPeople, date);

            return listOfTimeslots;
        }

        public async Task<IEnumerable<BookingGetDTO>> GetAllBookingsAsync()
        {
            var allBookings = await _bookingRepository.GetAllBookingsAsync();

            var bookingsList = allBookings.Select(b => new BookingGetDTO
            {
                Id = b.Id,
                NrOfPeople = b.NrOfPeople,
                Requests = b.Requests,
                
                RestaurantId = b.FK_RestaurantId,
                Restaurant = new RestaurantGetDTO
                {
                    Name = b.Restaurant.Name,
                    Address = b.Restaurant.Address,
                    Email = b.Restaurant.Email,
                    PhoneNumber = b.Restaurant.PhoneNumber,
                    Description = b.Restaurant.Description
                },
                UserId = b.FK_UserID,
                User = new UserGetDTO
                {
                    Name = b.User.Name,
                    Email = b.User.Email,
                    PhoneNumber= b.User.PhoneNumber
                },
                TableId = b.FK_TableId,
                Timeslot = new TimeslotGetDTO
                {
                    Id = b.Timeslot.Id,
                    StartTime = b.requestedTime,
                    EndTime = b.requestedTime.AddHours(2),
                    isAvaliable = b.Timeslot.isAvaliable
                }
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
                RestaurantId = b.FK_RestaurantId,
                Restaurant = new RestaurantGetDTO
                {
                    Name = b.Restaurant.Name,
                    Address = b.Restaurant.Address,
                    Email = b.Restaurant.Email,
                    PhoneNumber = b.Restaurant.PhoneNumber,
                    Description = b.Restaurant.Description
                },
                UserId = b.FK_UserID,
                User = new UserGetDTO
                {
                    Name = b.User.Name,
                    Email = b.User.Email,
                    PhoneNumber = b.User.PhoneNumber
                },
                TableId = b.FK_TableId,
                Timeslot = new TimeslotGetDTO
                {
                    Id = b.Timeslot.Id,
                    StartTime = b.requestedTime,
                    EndTime = b.requestedTime.AddHours(2),
                    isAvaliable = b.Timeslot.isAvaliable
                }
            }).ToList();

            return bookingsList;

        }
        public async Task<BookingGetDTO> GetBookingAsync(int bookingID)
        {
            //check that booking is users booking.

            var bookingById = await _bookingRepository.GetBookingAsync(bookingID);

            var booking = new BookingGetDTO
            {
                Id = bookingById.Id,
                NrOfPeople = bookingById.NrOfPeople,
                Requests = bookingById.Requests,
                RestaurantId = bookingById.FK_RestaurantId,
                Restaurant = new RestaurantGetDTO
                {
                    Id = bookingById.Restaurant.Id,
                    Name = bookingById.Restaurant.Name,
                    Address = bookingById.Restaurant.Address,
                    Email = bookingById.Restaurant.Email,
                    PhoneNumber = bookingById.Restaurant.PhoneNumber,
                    Description = bookingById.Restaurant.Description
                },
                UserId = bookingById.FK_UserID,
                User = new UserGetDTO
                {
                    Id = bookingById.User.Id,
                    Name = bookingById.User.Name,
                    Email = bookingById.User.Email,
                    PhoneNumber = bookingById.User.PhoneNumber
                },
                TableId = bookingById.FK_TableId,
                Timeslot = new TimeslotGetDTO
                {
                    Id = bookingById.Timeslot.Id,
                    StartTime = bookingById.requestedTime,
                    EndTime = bookingById.requestedTime.AddHours(2),
                    isAvaliable = bookingById.Timeslot.isAvaliable
                }                
            };

            return booking;

        }

        public async Task<TableGetDTO> IsBookingAvaliable(BookingCreateDTO bookingDTO)
        {
            DateTime startTime = bookingDTO.requestedTime;
            DateTime endtime = bookingDTO.requestedTime.AddHours(2);
            TimeSlot newTimeslot = new TimeSlot { StartTime = startTime, EndTime = endtime };

            var bookingToCheckIfAvaliable = new Booking
            {
                NrOfPeople = bookingDTO.NrOfPeople,
                requestedTime = bookingDTO.requestedTime,
                requestedEndTime = endtime,
                Requests = bookingDTO.Requests,
                FK_UserID = bookingDTO.UserId,
                FK_RestaurantId = bookingDTO.RestaurantId,
                Timeslot = newTimeslot
            };

            var AvaliableTable = await _bookingRepository.IsBookingAvaliable(bookingToCheckIfAvaliable);

            var tableToGetDto = new TableGetDTO
            {
                Id = AvaliableTable.Id,
                RestaurantId=AvaliableTable.FK_RestaurantId,
                NrOfSeats = AvaliableTable.NrOfSeats
            };


            //returns the first avaliable table with least nr of seats to meet requirement
            return tableToGetDto;
        }

        //return bool on these 3 to report success?
        public async Task<bool> AddBookingAsync(BookingCreateDTO bookingDTO)
        {   
            var avaliabletable = await IsBookingAvaliable(bookingDTO);

            if (avaliabletable.Id >= 0)
            {
                var restaurant = await _restaurantRepository.GetRestaurantAsync(bookingDTO.RestaurantId);

                // check if restaurant is null.

                var user = await _userRepository.GetUserAsync(bookingDTO.UserId);

                // check if user is null.

                //check if any of the required fields are empty (DTO.name), if so return false.

                DateTime startTime = bookingDTO.requestedTime;
                DateTime endtime = bookingDTO.requestedTime.AddHours(2);
                TimeSlot newTimeslot = new TimeSlot {
                    StartTime = startTime,
                    EndTime = endtime };

                await _timeslotRepository.AddTimeSlotAsync(newTimeslot);

                Booking bookingToAdd = new Booking
                {
                    NrOfPeople = bookingDTO.NrOfPeople,
                    Requests = bookingDTO.Requests,
                    FK_RestaurantId = bookingDTO.RestaurantId,
                    FK_UserID = bookingDTO.UserId,
                    FK_TableId = avaliabletable.Id,
                    FK_TimeslotId = newTimeslot.Id
                };

                //create timeslot
                //later timeslots can be used to set avaliable slots during open hours. atm i am not generating timeslots.

                
                //Add booking
                await _bookingRepository.AddBookingAsync(bookingToAdd);

                await _bookingRepository.UpdateBookingAsync(bookingToAdd);

                return true;
            }

            else
            {
                Console.WriteLine("booking failed, no avaliable tables");
                return false;
            }
        }
        public async Task UpdateBookingAsync(BookingUpdateDTO bookingDTO)
        {
            //check if any of the required fields are empty (DTO.name), if so return false.
            var bookingToUpdate = await _bookingRepository.GetBookingAsync(bookingDTO.Id);

            bookingToUpdate.NrOfPeople = bookingDTO.NrOfPeople;
            bookingToUpdate.Requests = bookingDTO.Requests;
            bookingToUpdate.FK_RestaurantId = bookingDTO.RestaurantId;
            bookingToUpdate.FK_UserID = bookingDTO.UserId;
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
