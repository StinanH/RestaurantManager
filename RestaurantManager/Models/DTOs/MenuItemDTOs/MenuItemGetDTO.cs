namespace RestaurantManager.Models.DTOs.MenuItemDTOs
{
    public class MenuItemGetDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public int FK_RestaurantId { get; set; }
        public int FK_MenuId { get; set; }
        public bool IsAvailable { get; set; }
        public int Price { get; set; }
        public int AmountSold { get; set; }
    }
}
