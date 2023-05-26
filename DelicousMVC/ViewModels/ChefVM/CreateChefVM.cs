using DelicousMVC.Models;

namespace DelicousMVC.ViewModels.ChefVM;

public class CreateChefVM
{
    public string Name { get; set; } = null!;
    public int WorkId { get; set; }
    public IFormFile Image { get; set; } = null!;
    public List<Work>? Works { get; set; }
}
