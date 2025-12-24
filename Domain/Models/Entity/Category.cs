namespace Domain.Models.Entity;

public class Category
{
    public long Id { get; set; }
    public string Name { get; set; }
    
    private List<Advertisement?>? Advertisements { get; set; } = new();
}