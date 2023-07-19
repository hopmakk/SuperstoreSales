using Microsoft.EntityFrameworkCore;
using PractProj_ASP.Models.Entities;
using PractProj_ASP.Root;

public class ApplicationContext : DbContext
{
    public DbSet<Returns> Returns { get; set; } = null!;
    public DbSet<Orders> Orders {get; set; } = null!;
    public DbSet<Users> Users {get; set; } = null!;
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
        //Database.EnsureDeleted();
        //Database.EnsureCreated();   // создаем базу данных при первом обращении
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Users>().HasKey(u => new { u.Region, u.Manager });

        List<Orders> ordlst = new List<Orders>();
        List<Returns> retlst = new List<Returns>();
        List<Users> userlst = new List<Users>();

        //ExcelImport.DoImportTo(ordlst, retlst, userlst);

        builder.Entity<Orders>().HasData(ordlst.ToArray());
        builder.Entity<Returns>().HasData(retlst.ToArray());
        builder.Entity<Users>().HasData(userlst.ToArray());
    }
}