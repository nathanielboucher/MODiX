using System.Transactions;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

using Modix.Data.Permissions;
using Modix.Data.Users;

namespace Modix.Data
{
    public static class Setup
    {
        public static IServiceCollection AddModixData(this IServiceCollection services, IConfiguration configuration)
            => services
                .Add(services => services.AddOptions<DataConfiguration>()
                    .Bind(configuration.GetSection("MODiX:Data"))
                    .ValidateDataAnnotations()
                    .ValidateOnStartup())
                .AddDbContext<ModixDbContext>()
                .AddSingleton<ITransactionScopeFactory, TransactionScopeFactory>()
                .AddPermissions()
                .AddUsers();

        public static IHealthChecksBuilder AddModixData(this IHealthChecksBuilder builder)
            => builder.AddDbContextCheck<ModixDbContext>();
    }
}
