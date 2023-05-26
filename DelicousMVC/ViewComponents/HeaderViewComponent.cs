using DelicousMVC.DelicousDataContexts;
using DelicousMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;

namespace DelicousMVC.ViewComponents;

public class HeaderViewComponent : ViewComponent
{
    private readonly DelicousDbContext _context;

    public HeaderViewComponent(DelicousDbContext context)
    {
        _context = context;
    }
    public async Task<IViewComponentResult> InvokeAsync()
    {
        Dictionary<string, Setting> setting = await _context.Settings.ToDictionaryAsync(x => x.Key);
        return View(setting);
    }
}
