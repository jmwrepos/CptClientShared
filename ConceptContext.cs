using CptClientShared;
using CptClientShared.Entities.Accounting;
using CptClientShared.Entities.Structure;
using CptClientShared.Entities.Vals;
using CptClientShared.QueryForms;
using Microsoft.EntityFrameworkCore;

namespace CptClientShared
{
    public class ConceptContext : DbContext
    {
        public DbSet<CptLibrary> Libraries => Set<CptLibrary>();
        public DbSet<CptObject> Objects => Set<CptObject>();
        public DbSet<CptProperty> Properties => Set<CptProperty>();
        public DbSet<CptObjectType> ObjectTypes => Set<CptObjectType>();
        public DbSet<CptObjectProperty> ObjectProperties => Set<CptObjectProperty>();
        public DbSet<CptObjectNameValue> ObjNameValues => Set<CptObjectNameValue>();
        public DbSet<CptStringValue> StringValues => Set<CptStringValue>();
        public DbSet<CptNumberValue> NumberValues => Set<CptNumberValue>();
        public DbSet<CptBytesValue> BytesValues => Set<CptBytesValue>();
        public DbSet<CptBoolValues> BoolValues => Set<CptBoolValues>();
        public DbSet<CptAccount> Accounts => Set<CptAccount>();
        public DbSet<CptAcctUser> AccountUsers => Set<CptAcctUser>();
        //REMAINING CODE BELOW
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseSqlServer(Environment.GetEnvironmentVariable("cpConnString")!).EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<CptLibrary>()
                .HasMany(e => e.Objects)
                .WithOne(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CptObject>()
                .HasOne(e => e.Parent)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CptLibrary>()
                .HasMany(e => e.ObjectTypes)
                .WithOne(e => e.ParentLibrary)
                .HasForeignKey(e => e.ParentLibraryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CptObjectType>()
                .HasOne(e => e.ParentType)
                .WithMany(e => e.Children)
                .HasForeignKey(e => e.ParentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            modelBuilder.Entity<CptObjectType>()
                .HasMany(e => e.Properties)
                .WithMany(e => e.ObjectTypes);

            modelBuilder.Entity<CptObjectType>()
                .HasMany(e => e.Objects)
                .WithMany(e => e.ObjectTypes);

            modelBuilder.Entity<CptLibrary>()
                .HasMany(e => e.Properties)
                .WithOne(e => e.Library)
                .HasForeignKey(e => e.LibraryId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CptObject>()
                .HasMany(e => e.ObjectProperties)
                .WithOne(e => e.Object)
                .HasForeignKey(e => e.ObjectId);

            modelBuilder.Entity<CptProperty>()
                .HasMany(e => e.ObjectProperties)
                .WithOne(e => e.Property)
                .HasForeignKey(e => e.PropertyId);

            modelBuilder.Entity<CptObjectProperty>()
                .HasMany(e => e.NumberValues)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<CptObjectProperty>()
                .HasMany(e => e.StringValues)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<CptObjectProperty>()
                .HasMany(e => e.ObjNameValues)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<CptObjectProperty>()
                .HasMany(e => e.BytesValues)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            modelBuilder.Entity<CptObjectProperty>()
                .HasMany(e => e.BoolValues)
                .WithOne(e => e.Owner)
                .HasForeignKey(e => e.OwnerId);

            //ACCOUNTING

            modelBuilder.Entity<CptAccount>()
                .HasMany(e => e.Users)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<CptAccount>()
                .HasMany(e => e.Libraries)
                .WithOne(e => e.Account)
                .HasForeignKey(e => e.AccountId)
                .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}