using Microsoft.EntityFrameworkCore;
using ReportBack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReportBack.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Paragraph> Paragraphs { get; set; }
        public DbSet<Sign> Signs { get; set; }
        public DbSet<House> Houses { get; set; }
        public DbSet<Planet> Planets { get; set; }
        public DbSet<Report> Reports { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            base.OnModelCreating(builder);
        }
    }
}
