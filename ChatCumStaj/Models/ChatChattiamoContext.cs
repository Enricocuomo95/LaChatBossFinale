using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ChatCumStaj.Models;

public partial class ChatChattiamoContext : DbContext
{
    public ChatChattiamoContext()
    {
    }

    public ChatChattiamoContext(DbContextOptions<ChatChattiamoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Utente> Utentes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=ACADEMY2024-16\\SQLEXPRESS;Database=ChatChattiamo;User Id=academy;Password=academy!;MultipleActiveResultSets=true;Encrypt=false;TrustServerCertificate=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Utente>(entity =>
        {
            entity.HasKey(e => e.Username).HasName("PK__Utente__F3DBC573E668D8D0");

            entity.ToTable("Utente");

            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("username");
            entity.Property(e => e.Passward)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("passward");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
