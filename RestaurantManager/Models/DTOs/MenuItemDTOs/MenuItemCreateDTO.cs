namespace RestaurantManager.Models.DTOs.MenuItemDTOs
{
    public class MenuItemCreateDTO
    {
        int MenuId { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }

        public int AmountAvaliable { get; set; }
    }
}
