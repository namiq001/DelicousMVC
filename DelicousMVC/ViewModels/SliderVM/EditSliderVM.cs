namespace DelicousMVC.ViewModels.SliderVM;

public class EditSliderVM
{
    public string? Title { get; set; }
    public string? Description { get; set; } 
    public string? ImageName { get; set; } 
    public IFormFile? Image { get; set ; }
}
