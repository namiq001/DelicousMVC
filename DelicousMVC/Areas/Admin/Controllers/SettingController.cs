using DelicousMVC.DelicousDataContexts;
using DelicousMVC.Models;
using DelicousMVC.ViewModels.SettingVM;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DelicousMVC.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingController : Controller
{
    private readonly DelicousDbContext _context;

    public SettingController(DelicousDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        List<Setting> settings = await _context.Settings.ToListAsync();
        return View(settings);
    }
    [HttpGet]
    public async Task<IActionResult> Edit(int Id)
    {
        Setting? setting = await _context.Settings.FindAsync(Id);
        if(setting is null)
        {
            return NotFound();
        }
        EditSettingVM editSetting = new EditSettingVM()
        {
            Value = setting.Value,
        };
        return View(editSetting);
    }
    [HttpPost]
    public async Task<IActionResult> Edit(int Id, EditSettingVM editSetting)
    {
        Setting? setting = await _context.Settings.FindAsync(Id);
        if(setting is null)
        {
            return NotFound();
        }
        if(!ModelState.IsValid)
        {
            return View(editSetting);
        }
        setting.Value = editSetting.Value;

        _context.Settings.Update(setting);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
