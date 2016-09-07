namespace Powerfront.Backend.EntityFramework
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Powerfront : DbContext
    {
        public Powerfront()
            : base("name=Powerfront")
        {
        }

        public virtual DbSet<ObjectDataTable> ObjectDataTables { get; set; }
        public virtual DbSet<ObjectPropertyTable> ObjectPropertyTables { get; set; }
        public virtual DbSet<ObjectTypeTable> ObjectTypeTables { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ObjectDataTable>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectDataTable>()
                .Property(e => e.PropertiesId)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectPropertyTable>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectPropertyTable>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectPropertyTable>()
                .Property(e => e.Age)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectPropertyTable>()
                .Property(e => e.TypeId)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectPropertyTable>()
                .HasOptional(e => e.ObjectDataTable)
                .WithRequired(e => e.ObjectPropertyTable);

            modelBuilder.Entity<ObjectTypeTable>()
                .Property(e => e.id)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectTypeTable>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<ObjectTypeTable>()
                .HasMany(e => e.ObjectPropertyTables)
                .WithRequired(e => e.ObjectTypeTable)
                .HasForeignKey(e => e.TypeId)
                .WillCascadeOnDelete(false);
        }
    }
}
