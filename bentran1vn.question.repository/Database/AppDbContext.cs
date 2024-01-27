using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace bentran1vn.question.repository.Database
{
    public partial class AppDbContext : IdentityDbContext<Users>
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            //Console.WriteLine(connectionString);
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tableName = entity.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entity.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<RefreshTokens>()
                .HasOne<Users>(reToken => reToken.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(reToken => reToken.UserId);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
