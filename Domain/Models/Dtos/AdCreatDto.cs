namespace Domain.Dtos;

public class AdCreatDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int Status { get; set; }
}