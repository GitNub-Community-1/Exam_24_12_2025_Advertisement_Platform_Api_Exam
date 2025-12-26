namespace Domain.Dtos;

public class AdDto
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedDate { get; set; }
    public int Status { get; set; }
    public int CategoryId { get; set; }
}