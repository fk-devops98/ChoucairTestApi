using ChoucairTest.Domain.Ports;
using ChoucairTest.Infrastructure.Adapters;
using ChoucairTest.Infrastructure.Middlewares;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Data;

namespace ChoucairTest.Infrastructure.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddPersistence(this IServiceCollection svc, IConfiguration config)
    {
        svc.AddTransient(typeof(ILoginMiddleware), typeof(LoginMiddleware));

        svc.AddTransient(typeof(ITareaRepository), typeof(TareaRepository));
        svc.AddTransient(typeof(IEstadoTareaRepository), typeof(EstadoTareaRepository));

        svc.AddTransient<IDbConnection>((_) => new SqlConnection(config.GetConnectionString("database")));

        return svc;
    }
}
