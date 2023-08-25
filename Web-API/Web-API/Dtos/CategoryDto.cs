namespace Web_API.Dtos
{
    public class CategoryDto
    {
        public int CategoryId { get; set; }

        public string CategoryName { get; set; } = null!;

        public string? Description { get; set; }

        public string? Picture { get; set; }
    }
}
