namespace Domain.Models.Filters;

public class AdFilter
{
    public long? Id { get; set; }
    public string? Title { get; set; }
    public string? Description { get; set; }
    public decimal? Price { get; set; }
    public DateOnly? CreatedDate { get; set; }
    public int? Status { get; set; }
}