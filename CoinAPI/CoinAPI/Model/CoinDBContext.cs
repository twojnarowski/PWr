using Microsoft.EntityFrameworkCore;

namespace CoinAPI.Model
{
    public class CoinDBContext : DbContext
    {
        public CoinDBContext(DbContextOptions<CoinDBContext> options) : base(options)
        {
        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Value> Values { get; set; }
    }
}