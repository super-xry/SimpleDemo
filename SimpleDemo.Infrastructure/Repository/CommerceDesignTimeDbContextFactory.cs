using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SimpleDemo.Infrastructure.Repository
{
    public class CommerceDesignTimeDbContextFactory : IDesignTimeDbContextFactory<CommerceDbContext>
    {
        public CommerceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<CommerceDbContext>();

            builder.UseSqlite("Data Source=(local);Database = TransactionAssistant; Trusted_Connection = True; MultipleActiveResultSets = true;Connect Timeout=30");
            return new CommerceDbContext(builder.Options);
        }
    }
}