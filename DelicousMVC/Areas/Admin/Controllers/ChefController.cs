using DelicousMVC.DelicousDataContexts;
using DelicousMVC.Models;
using DelicousMVC.ViewModels.ChefVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DelicousMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class ChefController : Controller
{
    private readonly DelicousDbContext _context;
    private readonly IWebHostEnvironment _environment;

    public ChefController(DelicousDbContext context, IWebHostEnvironment environment)
    {
        _environment = environment;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Chef> chefs = await _context.Chefs.Include(x=>x.work).ToListAsync();
        return View(chefs);
    }
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        CreateChefVM createChef = new CreateChefVM()
        {
            Works = await _context.Works.ToListAsync(),
        };
        return View(createChef);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateChefVM createChef)
    {
        createChef.Works = await _context.Works.ToListAsync();
        if(!ModelState.IsValid) { return View(createChef); }
        string newFileName = Guid.NewGuid().ToString() + createChef.Image.FileName;
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "chefs", newFileName);
        using(FileStream stream = new FileStream (path, FileMode.CreateNew))
        {
            await createChef.Image.CopyToAsync(stream);
        }
        Chef chef = new Chef()
        {
            Name = createChef.Name,
            WorkId = createChef.WorkId,
        };
        chef.ImageName = newFileName;

        _context.Chefs.Add(chef);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int Id)
    {
        Chef? chef = await _context.Chefs.FindAsync(Id);
        if(chef is null) { return NotFound(); }
        string path = Path.Combine(_environment.WebRootPath, "assets", "img", "chefs", chef.ImageName);
        if(System.IO.File.Exists(path))
        {
            System.IO.File.Delete(path);
        }
        _context.Chefs.Remove(chef);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Chef? chef = await _context.Chefs.FindAsync(Id);
        if(chef is null)
        {
            return NotFound();
        }
        EditChefVM editChef = new EditChefVM()
        {
            Name = chef.Name,
            WorkId = chef.WorkId,
            Works = await _context.Works.ToListAsync(),
            ImageName = chef.ImageName,
        };
        return View(editChef);
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int Id, EditChefVM editChef)
    {
        Chef? chef = await _context.Chefs.FindAsync(Id);
        if (chef is null)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            editChef.Works = await _context.Works.ToListAsync();
            return View(editChef);
        }
        if(editChef.ImageName is not null)
        {
            string path = Path.Combine(_environment.WebRootPath, "assets", "img", "chefs", chef.ImageName);
            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            string newFileName = Guid.NewGuid().ToString() + editChef.Image.FileName;
            string newPath = Path.Combine(_environment.WebRootPath, "assets", "img", "chefs", newFileName);
            using (FileStream stream = new FileStream(newPath, FileMode.CreateNew))
            {
                await editChef.Image.CopyToAsync(stream);
            }
            chef.ImageName = newFileName;
        }
        chef.Name = editChef.Name;
        chef.WorkId = editChef.WorkId;
        _context.Chefs.Update(chef);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
