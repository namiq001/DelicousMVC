using DelicousMVC.DelicousDataContexts;
using DelicousMVC.Models;
using DelicousMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DelicousMVC.Controllers;

public class HomeController : Controller
{
    private readonly DelicousDbContext _context;

    public HomeController(DelicousDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Chef> chefList = await _context.Chefs.Include(x => x.work).ToListAsync();
        List<Slider> sliders = await _context.Sliders.ToListAsync();
        HomeVM homeVM = new HomeVM()
        {
            Chefs = chefList,
            Sliders = sliders
        };
        return View(homeVM);
    }
}