using DelicousMVC.DelicousDataContexts;
using DelicousMVC.Models;
using DelicousMVC.ViewModels.SliderVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DelicousMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SliderController : Controller
{
    private readonly DelicousDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public SliderController(DelicousDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }

    public async Task<IActionResult> Index()
    {
        List<Slider> sliderList = await _context.Sliders.ToListAsync();
        return View(sliderList);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Slider? slider = await _context.Sliders.FindAsync(Id);
        if(slider is null)
        {
            return NotFound();
        }
        EditSliderVM editSlider = new EditSliderVM()
        {
            Title = slider.Title,
            Description = slider.Description,
            ImageName = slider.ImageName,
        };
        return View(editSlider);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditSliderVM editSlider)
    {
        Slider? slider = await _context.Sliders.FindAsync(Id);
        if (slider is null)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            return View(editSlider);
        }
        if (editSlider.ImageName is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "slide", editSlider.ImageName);
            if(System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editSlider.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "slide", newFileName);
            using(FileStream stream = new FileStream(path, FileMode.CreateNew))
            {
                await editSlider.Image.CopyToAsync(stream);
            }
            slider.ImageName = newFileName;
        }
        slider.Title = editSlider.Title;
        slider.Description = editSlider.Description;

        _context.Sliders.Update(slider);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
