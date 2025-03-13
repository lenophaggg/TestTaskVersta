using WebApplication2.Models;

namespace WebApplication2.Services
{
    public class DatabaseSeeder
    {
        private readonly AppDbContext _context;
        private readonly IOrderNumberGenerator _orderNumberGenerator;

        public DatabaseSeeder(AppDbContext context, IOrderNumberGenerator orderNumberGenerator)
        {
            _context = context;
            _orderNumberGenerator = orderNumberGenerator;
        }

        public async Task SeedOrdersAsync(int count = 1000)
        {
            
                var random = new Random();
                var cities = new[] { "Москва", "Санкт-Петербург", "Новосибирск", "Екатеринбург", "Казань" };
                var streets = new[] { "Ленина", "Мира", "Советская", "Пушкина", "Гагарина" };

                var orders = new List<Order>();

                for (int i = 0; i < count; i++)
                {
                    var senderCity = cities[random.Next(cities.Length)];
                    var recipientCity = cities[random.Next(cities.Length)];

                    var order = new Order
                    {
                        Ordernumber = _orderNumberGenerator.GenerateOrderNumber(),
                        Sendercity = senderCity,
                        Senderaddress = $"ул. {streets[random.Next(streets.Length)]}, д. {random.Next(1, 100)}",
                        Recipientcity = recipientCity,
                        Recipientaddress = $"ул. {streets[random.Next(streets.Length)]}, д. {random.Next(1, 100)}",
                        Cargoweight = Math.Round((decimal)((random.NextDouble()+1) * 100), 2), // Вес от 0 до 100 кг
                        Pickupdate = DateOnly.FromDateTime(DateTime.Now.AddDays(random.Next(-30, 30))), // За последние/будущие 30 дней
                        Createdat = DateTime.Now.AddDays(-random.Next(0, 60)) // Создан в последние 60 дней
                    };

                    orders.Add(order);
                }

                _context.Orders.AddRange(orders);
                await _context.SaveChangesAsync();
           
        }
    }
}
