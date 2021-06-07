using Microsoft.EntityFrameworkCore;
using MONQTest.Models;

namespace MONQTest.Infrastructure
{
    public class AppDBContext : DbContext
    {
        ///<summary>
        ///Конструктор производного контекста БД
        ///</summary>>
        public AppDBContext(DbContextOptions<AppDBContext> options) : base (options)
        {
            Database.EnsureCreated();
        }

        ///<summary>
        ///Сущности Mails
        ///</summary>>
        public DbSet<Mail> Mails { get; set; }
        ///<summary>
        ///Сущности Recipient
        ///</summary>>
        public DbSet<Recipient> Recipients { get; set; }


        protected override void OnModelCreating (ModelBuilder modelBuilder)
        {
        }
    }
}
