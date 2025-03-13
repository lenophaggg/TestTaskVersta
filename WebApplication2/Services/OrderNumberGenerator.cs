namespace WebApplication2.Services
{
    public class OrderNumberGenerator : IOrderNumberGenerator
    {
        public string GenerateOrderNumber()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
        }
    }
}
