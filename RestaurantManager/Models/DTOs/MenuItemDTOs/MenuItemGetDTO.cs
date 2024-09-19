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
        public bool IsAvaliable { get; set; }
        public int AmountAvaliable { get; set; }
    }
}
