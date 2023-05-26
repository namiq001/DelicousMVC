namespace DelicousMVC.Models;

public class Work
{
    public int Id { get; set; }
    public string WorkName { get; set; } = null!;
    public List<Chef> chefs { get; set; } 
}
