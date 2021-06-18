using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace proj_zal
{
    public class GamesDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=DESKTOP-JR5ILTA;Database=Games;Trusted_Connection=True;");
            }
        }
        public virtual DbSet<Games> tSteamChart { get; set; }
    }
}