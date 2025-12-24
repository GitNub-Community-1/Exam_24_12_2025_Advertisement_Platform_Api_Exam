namespace Domain.Models.Entity;

public class Advertisement
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public DateOnly CreatedDate { get; set; }
    public int Status { get; set; }
    
    public int CategoryId { get; set; }
    public Category? Category { get; set; }
    
    public int UserId { get; set; }
    public User? User { get; set; }
}