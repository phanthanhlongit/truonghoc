using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace KidsSchool.Models.DB
{
    public partial class Entities : DbContext
    {
        public Entities()
            : base("name=Entities")
        {
        }

        public virtual DbSet<C__MigrationHistory> C__MigrationHistory { get; set; }
        public virtual DbSet<AspNetRole> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; }
        public virtual DbSet<BannerPosition> BannerPositions { get; set; }
        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Config> Configs { get; set; }
        public virtual DbSet<ContactGHelp> ContactGHelps { get; set; }
        public virtual DbSet<ContactGHelpGroup> ContactGHelpGroups { get; set; }
        public virtual DbSet<District> Districts { get; set; }
        public virtual DbSet<HitCounter> HitCounters { get; set; }
        public virtual DbSet<MenuLocation> MenuLocations { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<Noti_Send> Noti_Send { get; set; }
        public virtual DbSet<Noti_Setting> Noti_Setting { get; set; }
        public virtual DbSet<Noti_User> Noti_User { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<Province> Provinces { get; set; }
        public virtual DbSet<SeoUrlRecord> SeoUrlRecords { get; set; }
        public virtual DbSet<Slider> Sliders { get; set; }
        public virtual DbSet<Testimonial> Testimonials { get; set; }
        public virtual DbSet<RecursiveMenuView> RecursiveMenuViews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRole>()
                .HasMany(e => e.AspNetUsers)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("AspNetUserRoles").MapLeftKey("RoleId").MapRightKey("UserId"));

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserClaims)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<AspNetUser>()
                .HasMany(e => e.AspNetUserLogins)
                .WithRequired(e => e.AspNetUser)
                .HasForeignKey(e => e.UserId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Categories1)
                .WithOptional(e => e.Category1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Category>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.Category)
                .HasForeignKey(e => e.CatId);

            modelBuilder.Entity<ContactGHelpGroup>()
                .HasMany(e => e.ContactGHelps)
                .WithOptional(e => e.ContactGHelpGroup)
                .HasForeignKey(e => e.GroupId);

            modelBuilder.Entity<HitCounter>()
                .Property(e => e.IPAddress)
                .IsFixedLength();

            modelBuilder.Entity<HitCounter>()
                .Property(e => e.Device)
                .IsFixedLength();

            modelBuilder.Entity<MenuLocation>()
                .HasMany(e => e.Menus)
                .WithOptional(e => e.MenuLocation)
                .HasForeignKey(e => e.LocationId);

            modelBuilder.Entity<Menu>()
                .HasMany(e => e.Menus1)
                .WithOptional(e => e.Menu1)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<Noti_Send>()
                .Property(e => e.ToListId)
                .IsUnicode(false);

            modelBuilder.Entity<Noti_User>()
                .Property(e => e.Tag)
                .IsFixedLength();
        }
    }
}
