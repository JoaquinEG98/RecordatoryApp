using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Models
{
    public partial class RecordatorioAppContext : DbContext
    {
        public RecordatorioAppContext(DbContextOptions<RecordatorioAppContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Userid=postgres;Password=1234;Database=RecordatorioApp");
            }
        }

        // Tablas de la BD: RecordatorioApp
        public DbSet<User> User { get; set; } = null!;
        public DbSet<Note> Notificacion { get; set; } = null!;
        public DbSet<List> List { get; set; } = null!;
    }
}
