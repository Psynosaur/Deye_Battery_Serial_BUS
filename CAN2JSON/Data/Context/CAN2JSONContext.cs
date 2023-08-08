using CAN2JSON.BMS;
using CAN2JSON.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Context;

public partial class Can2JsonContext : DbContext
{
    public Can2JsonContext()
    {
    }

    public Can2JsonContext(DbContextOptions<Can2JsonContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BmsReading> BmsReadings { get; set; } = null!;
    public virtual DbSet<BatteryReading> BatteryReadings { get; set; } = null!;
    public virtual DbSet<BatteryCellReading> BatteryCellReadings { get; set; } = null!;

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}