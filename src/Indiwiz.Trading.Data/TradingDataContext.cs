using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Indiwiz.Trading.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Indiwiz.Trading.Data
{
    public class TradingDataContext : DbContext
    {
        public DbSet<Instrument> Instruments { get; set; } = null!;
        public string DbPath { get; set; } = null!;

        public TradingDataContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = System.IO.Path.Join(path, "trading.db");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}