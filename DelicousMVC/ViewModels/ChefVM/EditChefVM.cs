using DelicousMVC.Models;

namespace DelicousMVC.ViewModels.ChefVM;

public class EditChefVM
{
    public string? Name { get; set; }
    public int WorkId { get; set; }
    public string? ImageName { get; set; }
    public IFormFile? Image { get; set; }
    public List<Work>? Works { get; set; } 
}
