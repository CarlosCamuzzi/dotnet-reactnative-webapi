using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroPessoa.Models;
using Microsoft.EntityFrameworkCore;

namespace CadastroPessoa.Data
{
  public class AppDbContext : DbContext
  {
    public DbSet<Pessoa> Pessoas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
    }
  }
}