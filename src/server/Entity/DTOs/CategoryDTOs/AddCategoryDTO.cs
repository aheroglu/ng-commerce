namespace Entity.DTOs.CategoryDTOs
{
    public class AddCategoryDTO
    {
        public string Name { get; set; }
        public string UrlHandle { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
