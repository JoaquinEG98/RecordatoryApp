using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models
{
    public partial class RecordatorioAppContext : DbContext
    {
        public RecordatorioAppContext(DbContextOptions<RecordatorioAppContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        // Tablas de la BD: RecordatorioApp
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Note> Note { get; set; } = null!;
        public DbSet<List> List { get; set; } = null!;
    }
}
