using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Kaizen.EntityFrameworkCore
{
    public static class KaizenDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<KaizenDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<KaizenDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
