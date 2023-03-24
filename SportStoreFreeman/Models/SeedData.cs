using Microsoft.EntityFrameworkCore;
using SportStoreFreeman.Data;

namespace SportStoreFreeman.Models
{
    public class SeedData
    {
        public static void EnsurePopulated(IApplicationBuilder  applicationBuilder)
        {
            SportStoreDbContext context = applicationBuilder.ApplicationServices
                .CreateScope().ServiceProvider.GetRequiredService<SportStoreDbContext>();
            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Products.Any())
            {
                context.AddRange(
                new Product
                {
                    Name = "Каяк",
                    Description = "Лодка для одного человека",
                    Category = "Водный спорт",
                    Price = 275M
                },
                new Product
                {
                    Name = "Спасательный жилет",
                    Description = "Защитный и модный",
                    Category = "Водный спорт",
                    Price = 48.95M
                },
                new Product
                {
                    Name = "Футбольный мяч",
                    Description = "Одобрен ФИФА",
                    Category = "Футбол",
                    Price = 19.50M
                },
                new Product
                {
                    Name = "Угловой флажок",
                    Description = "Для настоящих профессионалов",
                    Category = "Футбол",
                    Price = 34.95M
                },
                new Product
                {
                    Name = "Стадион",
                    Description = "Раздвижной стадион",
                    Category = "Футбол",
                    Price = 79500M
                },
                new Product
                {
                    Name = "Кепка мыслителя",
                    Description = "Скилл + 500",
                    Category = "Шахматы",
                    Price = 16M
                },
                new Product
                {
                    Name = "Стул шахматиста",
                    Description = "Скилл + 1500",
                    Category = "Шахматы",
                    Price = 29.95M
                },
                new Product
                {
                    Name = "Шахматы",
                    Description = "Для настоящих ценителей игры",
                    Category = "Шахматы",
                    Price = 75M
                },
                new Product
                {
                    Name = "Золотой король",
                    Description = "Лучший подарок шахматисту",
                    Category = "Шахматы",
                    Price = 1200M
                }
                );
            }
            context.SaveChanges();
        }
    }
}
