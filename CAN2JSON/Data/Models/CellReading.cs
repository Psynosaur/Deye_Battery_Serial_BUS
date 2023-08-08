using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace CAN2JSON.Data.Models;

public class BatteryCellReading
{
    [Key] public int Id { get; set; }
    public int Date { get; set; }
    public int SlaveNumber { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell01 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell02 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell03 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell04 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell05 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell06 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell07 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell08 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell09 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell10 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell11 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell12 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell13 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell14 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell15 { get; set; }
    [Column(TypeName = "decimal(4, 3)")]  public decimal Cell16 { get; set; }
    [Column(TypeName = "decimal(0, 0)")]  public decimal MinPos { get; set; }
}