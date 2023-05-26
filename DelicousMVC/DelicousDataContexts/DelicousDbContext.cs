using DelicousMVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DelicousMVC.DelicousDataContexts;

public class DelicousDbContext : IdentityDbContext<AppUser>
{
	public DelicousDbContext(DbContextOptions<DelicousDbContext> options) : base(options)
	{

	}
	public DbSet<Chef> Chefs { get; set; }
	public DbSet<Work> Works { get; set; }
	public DbSet<Slider> Sliders { get; set; }
	public DbSet<Setting> Settings { get; set; }
}
