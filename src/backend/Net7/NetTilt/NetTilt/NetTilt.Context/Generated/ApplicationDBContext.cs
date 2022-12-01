using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Generated
{
    public partial class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext()
        {
        }

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TILT_Note> TILT_Note { get; set; } = null!;
        public virtual DbSet<TILT_Tag> TILT_Tag { get; set; } = null!;
        public virtual DbSet<TILT_Tag_Note> TILT_Tag_Note { get; set; } = null!;
        public virtual DbSet<TILT_URL> TILT_URL { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=tilt;UId=sa;pwd=<YourStrong@Passw0rd>");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TILT_Note>(entity =>
            {
                entity.HasOne(d => d.IDURLNavigation)
                    .WithMany(p => p.TILT_Note)
                    .HasForeignKey(d => d.IDURL)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TILT_Note_TILT_URL");
            });

            modelBuilder.Entity<TILT_Tag_Note>(entity =>
            {
                entity.HasOne(d => d.IDNoteNavigation)
                    .WithMany(p => p.TILT_Tag_Note)
                    .HasForeignKey(d => d.IDNote)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TILT_Tag_Note_TILT_Note");

                entity.HasOne(d => d.IDTagNavigation)
                    .WithMany(p => p.TILT_Tag_Note)
                    .HasForeignKey(d => d.IDTag)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_TILT_Tag_Note_TILT_Tag");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
