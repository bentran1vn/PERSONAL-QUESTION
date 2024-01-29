using bentran1vn.question.repository.Datas.Entities;
using bentran1vn.question.src.Datas.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Security.AccessControl;


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
        DbSet<RefreshTokens> RefreshTokens { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Development.json")
                .Build();
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var allEntity = modelBuilder.Model.GetEntityTypes();

            foreach (var entity in allEntity)
            {
                var tableName = entity.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entity.SetTableName(tableName.Substring(6));
                }
                entity.AddProperty("CreatedDate", typeof(DateTime));
                entity.AddProperty("UpdatedDate", typeof(DateTime));
            }

            modelBuilder.Entity<RefreshTokens>()
                .HasOne<Users>(reToken => reToken.User)
                .WithMany(u => u.RefreshTokens)
                .HasForeignKey(reToken => reToken.UserId);

            modelBuilder.Entity<UserQuestion>()
                .HasOne<Users>(ques => ques.User)
                .WithMany(u => u.UserQuestions)
                .HasForeignKey(ques => ques.UserId);

            modelBuilder.Entity<QuestionAnswers>()
                .HasOne<UserQuestion>(ans => ans.UserQuestion)
                .WithMany(ques => ques.QuestionAnswers)
                .HasForeignKey(ans => ans.UserQuestionId);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var insertedEntries = ChangeTracker.Entries()
                               .Where(x => x.State == EntityState.Added)
                               .Select(x => x.Entity);

            foreach (var insertedEntry in insertedEntries)
            {
                if (insertedEntry is UserQuestion questionEntity)
                {
                    Entry(questionEntity).Property("CreatedDate").CurrentValue = DateTime.Now;
                }
            }

            var modifiedEntries = this.ChangeTracker.Entries()
                   .Where(x => x.State == EntityState.Modified)
                   .Select(x => x.Entity);

            foreach (var modifiedEntry in modifiedEntries)
            {
                if (modifiedEntry is UserQuestion questionEntity)
                {
                    Entry(questionEntity).Property("UpdatedDate").CurrentValue = DateTimeOffset.UtcNow;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
