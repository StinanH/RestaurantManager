﻿namespace RestaurantManager.Models.DTOs.UserDTOs
{
    public class UserUpdateDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}
