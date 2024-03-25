using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer
{
    public class DatabaseContext : DbContext
    {
        string connectionString;
        public DatabaseContext(string connectionString) : base()
        {
            this.connectionString = connectionString;
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionCategory> TransactionCategories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(this.connectionString);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Name = "Фридрих", Password = "34234" },
                    new User { Id = 2, Name = "Alice", Password = "34234" },
                    new User { Id = 3, Name = "Sam", Password = "34234" }
            );

            modelBuilder.Entity<Account>().HasData(
                    new Account { Id = 1, Name = "Наличные", Balance = 50000, UserId = 1},
                    new Account { Id = 2, Name = "Дебетовая карта", Balance = 150000, Number = "6509", UserId = 1},
                    new Account { Id = 3, Name = "Кредитная карта", Balance = 300000, Limit = 100000, Number = "1337" , LimitRenewalDate = new DateTime(2023, 12, 28), LimitRenewalFrequency = 30, UserId = 1}
            );

            modelBuilder.Entity<TransactionCategory>().HasData(
                    new TransactionCategory { Id = 1, Name = "Сбережения", UserId = null},
                    new TransactionCategory { Id = 2, Name = "Перевод", UserId = null },
                    new TransactionCategory { Id = 3, Name = "Коррекция", UserId = null },
                    new TransactionCategory { Id = 4, Name = "Продукты", UserId = null },
                    new TransactionCategory { Id = 5, Name = "ЖКХ", UserId = null},
                    new TransactionCategory { Id = 6, Name = "Здоровье", UserId = null },
                    new TransactionCategory { Id = 7, Name = "Одежда", UserId = null},
                    new TransactionCategory { Id = 8, Name = "Образование", UserId = null },
                    new TransactionCategory { Id = 9, Name = "Коммуникации", UserId = null },
                    new TransactionCategory { Id = 10, Name = "Транспорт", UserId = null },
                    new TransactionCategory { Id = 11, Name = "Зарплата", UserId = null },
                    new TransactionCategory { Id = 12, Name = "Подарки", UserId = null},
                    new TransactionCategory { Id = 13, Name = "Фридрих Один", UserId = 1},
                    new TransactionCategory { Id = 14, Name = "Фридрих Два", UserId = 1 },
                    new TransactionCategory { Id = 15, Name = "Фридрих Три", UserId = 1 }
            );

            modelBuilder.Entity<Transaction>().HasData(
                new Transaction { Id = 1, Date = DateTime.Now, TransactionCategoryId = 4, AccountId = 1, UserId = 1, TransactionType = 1, Amount = 1500 },
                new Transaction { Id = 2, Date = DateTime.Now, TransactionCategoryId = 5, AccountId = 2, UserId = 1, TransactionType = 1, Amount = 2500 },
                new Transaction { Id = 3, Date = DateTime.Now, TransactionCategoryId = 6, AccountId = 3, UserId = 1, TransactionType = 1, Amount = 15000 },
                new Transaction { Id = 4, Date = DateTime.Now, TransactionCategoryId = 7, AccountId = 1, UserId = 1, TransactionType = 1, Amount = 1000 },

                new Transaction { Id = 5, Date = DateTime.Now, TransactionCategoryId = 8, AccountId = 1, UserId = 1, TransactionType = 2, Amount = -1300 },
                new Transaction { Id = 6, Date = DateTime.Now, TransactionCategoryId = 13, AccountId = 2, UserId = 1, TransactionType = 2, Amount = -725 },
                new Transaction { Id = 7, Date = DateTime.Now, TransactionCategoryId = 14, AccountId = 3, UserId = 1, TransactionType = 2, Amount = -2300 },
                new Transaction { Id = 8, Date = DateTime.Now, TransactionCategoryId = 15, AccountId = 1, UserId = 1, TransactionType = 2, Amount = -20000 }
                );

            modelBuilder.Entity<Goal>().HasData(
                new Goal { Id = 1, UserId = 1, Amount = 150000, Name = "А-а-а-автомобиль", Balance = 1000, DateOfCreation = DateTime.Parse("11-11-2023"), DateToSaveUp = DateTime.Parse("04-04-2024")}, // обычная цель
                new Goal { Id = 2, UserId = 1, Amount = 25000, Name = "Дорогой енот", Balance = 30000, DateOfCreation = DateTime.Parse("03-09-2023"), DateToSaveUp = DateTime.Parse("03-09-2024")}, // накопленная цель
                new Goal { Id = 3, UserId = 1, Amount = 120000, Name = "Накопить за лето", Balance = 10000, DateOfCreation = DateTime.Parse("06-01-2023"), DateToSaveUp = DateTime.Parse("09-09-2023") } // просроченная цель
                );

            //modelBuilder.Entity<Goal>().HasData(
            //        new TransactionCategory { Id = 1, Name = "Сбережения", UserId = null },
            //);
        }
    }
}
 