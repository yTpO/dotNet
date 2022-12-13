using Microsoft.EntityFrameworkCore;

namespace Words;

public partial class WordsDbContext : DbContext
{
    public WordsDbContext()
    {
    }

    public WordsDbContext(DbContextOptions<WordsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Lvl> Lvls { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=DESKTOP-LOSC5IU;Initial Catalog=wordsDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lvl>(entity =>
        {
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.Name).HasMaxLength(10);
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.LvlId).HasColumnName("Lvl_Id");
            entity.Property(e => e.Value).HasMaxLength(100);

            entity.HasOne(d => d.Lvl).WithMany(p => p.Words)
                .HasForeignKey(d => d.LvlId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK_Words_Lvls");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
