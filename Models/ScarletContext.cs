using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models
{
    public partial class ScarletContext : DbContext
    {
        public ScarletContext(DbContextOptions<ScarletContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        // Tablas de la BD: RecordatorioApp
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Note> Note { get; set; } = null!;
        public DbSet<List> List { get; set; } = null!;
    }
}
