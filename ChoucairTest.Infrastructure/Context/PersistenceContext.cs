using ChoucairTest.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ChoucairTest.Infrastructure.Context;

public class PersistenceContext : IdentityDbContext<Usuario>
{
    private readonly IConfiguration _config;
    public PersistenceContext(DbContextOptions<PersistenceContext> options, IConfiguration config) 
        : base(options)
    {
        _config = config;
    }

    public async Task CommitAsync()
    {
        await SaveChangesAsync().ConfigureAwait(false);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) 
    {
        if (modelBuilder == null)
            return;
        
        modelBuilder.HasDefaultSchema(_config.GetValue<string>("SchemaName"));

        modelBuilder.Entity<Tarea>(
            entity => {
                entity
                    .HasOne(d => d.EstadoTarea)
                    .WithMany(p => p.Tareas)
                    .HasForeignKey(d => d.EstadoTareaId)
                    .HasConstraintName("FK_EstadoTarea_Tarea");
            }
        );
        modelBuilder.Entity<EstadoTarea>();

        base.OnModelCreating(modelBuilder);
    }

}
